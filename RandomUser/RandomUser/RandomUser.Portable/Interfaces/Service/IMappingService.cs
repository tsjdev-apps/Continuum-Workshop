using System.Collections.Generic;
using RandomUser.Portable.DTO;
using RandomUser.Portable.Model;

namespace RandomUser.Portable.Interfaces.Service
{
    public interface IMappingService
    {
        IEnumerable<User> MapUsers(IEnumerable<Result> dtos);
    }
}