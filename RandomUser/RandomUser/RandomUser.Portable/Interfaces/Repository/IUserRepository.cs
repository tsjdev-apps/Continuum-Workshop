using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RandomUser.Portable.Model;

namespace RandomUser.Portable.Interfaces.Repository
{
    public interface IUserRepository
    {
        event EventHandler<User> SelectedUserChanged;

        Task<ObservableCollection<User>> GetUsersAsync();
        void SearchUsers(string query);
        void SetSelectedUser(User user);
    }
}