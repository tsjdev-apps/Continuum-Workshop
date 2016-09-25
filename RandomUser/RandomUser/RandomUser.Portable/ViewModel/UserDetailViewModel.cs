using GalaSoft.MvvmLight.Command;
using RandomUser.Portable.Interfaces.Repository;
using RandomUser.Portable.Interfaces.Service;
using RandomUser.Portable.Model;

namespace RandomUser.Portable.ViewModel
{
    public class UserDetailViewModel : AsyncViewModelBase
    {
        private readonly IProjectionService _projectionService;

        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(); }
        }


        public RelayCommand<User> ProjectUserCommand { get; private set; }

        public UserDetailViewModel(IUserRepository userRepository, IProjectionService projectionService)
        {
            _projectionService = projectionService;
            userRepository.SelectedUserChanged += (__, user) => User = user;

            ProjectUserCommand = new RelayCommand<User>(ProjectUser);
        }

        private void ProjectUser(User user)
        {
            if (user == null)
                return;

            _projectionService.StartProjection(user);
        }
    }
}