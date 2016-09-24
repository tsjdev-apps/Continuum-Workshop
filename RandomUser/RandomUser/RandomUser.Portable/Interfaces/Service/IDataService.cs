using System.Collections.Generic;
using System.Threading.Tasks;
using RandomUser.Portable.Model;
using RandomUser.Portable.Utils;

namespace RandomUser.Portable.Interfaces.Service
{
    public interface IDataService
    {
        Task<DataServiceResponse<IEnumerable<User>>> GetUsersAsync(int count);
    }
}