using RandomUser.Portable.Interfaces.Repository;
using RandomUser.Portable.Model;

namespace RandomUser.Portable.ViewModel
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