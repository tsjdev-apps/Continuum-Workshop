using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RandomUser.Portable.Exceptions;
using RandomUser.Portable.ExtensionMethods;
using RandomUser.Portable.Interfaces.Repository;
using RandomUser.Portable.Interfaces.Service;
using RandomUser.Portable.Model;

namespace RandomUser.Portable.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataService _dataService;
        private IEnumerable<User> _allUsers;
        private ObservableCollection<User> _currentUsers;
        private User _selectedUser;

        public event EventHandler<User> SelectedUserChanged;

        public UserRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ObservableCollection<User>> GetUsersAsync()
        {
            if (_allUsers != null)
                return _currentUsers;

            var response = await _dataService.GetUsersAsync(250);

            if (!response.IsSuccess)
                throw new DataNotAvailableException();

            _allUsers = response.IsSuccess ? response.Response.OrderBy(user => user.FullName) : Enumerable.Empty<User>();
            _currentUsers = new ObservableCollection<User>(_allUsers);

            return _currentUsers;
        }

        public void SearchUsers(string query)
        {
            var searchResult = _allUsers.Where(e => (!string.IsNullOrEmpty(e.FirstName) && Regex.IsMatch(e.FirstName, query, RegexOptions.IgnoreCase)) || (!string.IsNullOrEmpty(e.LastName) && Regex.IsMatch(e.LastName, query, RegexOptions.IgnoreCase)));
            _currentUsers.Filter(searchResult);
        }

        public void SetSelectedUser(User user)
        {
            _selectedUser = user;
            SelectedUserChanged?.Invoke(this, user);
        }
    }
}