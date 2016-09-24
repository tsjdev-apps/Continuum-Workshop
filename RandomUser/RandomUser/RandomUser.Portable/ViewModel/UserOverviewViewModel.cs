using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using RandomUser.Portable.Exceptions;
using RandomUser.Portable.Interfaces.Repository;
using RandomUser.Portable.Interfaces.Service;
using RandomUser.Portable.Model;

namespace RandomUser.Portable.ViewModel
{
    public class UserOverviewViewModel : AsyncViewModelBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IResourceService _resourceService;
        private readonly IDialogService _dialogService;

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; RaisePropertyChanged(); }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; RaisePropertyChanged(); SelectUser(value); }
        }

        private string _query;
        public string Query
        {
            get { return _query; }
            set { _query = value; RaisePropertyChanged(); SearchUsers(); }
        }

        public RelayCommand InitCommand { get; private set; }
        public RelayCommand<User> SelectUserCommand { get; private set; }

        public UserOverviewViewModel(IUserRepository userRepository, IResourceService resourceService, IDialogService dialogService)
        {
            _userRepository = userRepository;
            _resourceService = resourceService;
            _dialogService = dialogService;

            InitCommand = new RelayCommand(Init);
            SelectUserCommand = new RelayCommand<User>(SelectUser);
        }

        private async void Init()
        {
            IsLoading = true;
            LoadingMessage = _resourceService.GetString("UsersLoadingMessage");

            try
            {
                Users = await _userRepository.GetUsersAsync();
                IsLoading = false;
                LoadingMessage = null;
            }
            catch (DataNotAvailableException)
            {
                await ShowNetworkErrorAsync();
            }
        }

        private void SelectUser(User user)
        {
            _userRepository.SetSelectedUser(user);
        }

        private void SearchUsers()
        {
            _userRepository.SearchUsers(Query);
        }

        private async Task ShowNetworkErrorAsync()
        {
            if (await _dialogService.ShowMessage(_resourceService.GetString("NetworkErrorMessage"), _resourceService.GetString("NetworkErrorTitle"), _resourceService.GetString("NetworkErrorButtonReload"), _resourceService.GetString("NetworkErrorButtonCancel"), null))
            {
                Init();
            }
            else
            {
                IsLoading = false;
                LoadingMessage = null;
            }
        }
    }
}