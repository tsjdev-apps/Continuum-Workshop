using RandomUser.Core.Interfaces.Repository;
using RandomUser.Core.Model;
using RandomUser.Portable.Interfaces.Repository;
using RandomUser.Portable.Model;
using RandomUser.Portable.ViewModel;

namespace RandomUser.Core.ViewModel
{
    public class UserDetailViewModel : AsyncViewModelBase
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(); }
        }
        
        public UserDetailViewModel(IUserRepository userRepository)
        {
            userRepository.SelectedUserChanged += (__, user) => User = user;
        }
    }
}