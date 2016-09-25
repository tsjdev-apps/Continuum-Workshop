using System.Threading.Tasks;
using RandomUser.Portable.Model;

namespace RandomUser.Portable.Interfaces.Service
{
    public interface IProjectionService
    {
        bool IsProjecting { get; }

        void StartProjection(User selectedUser);
        Task StopProjectionAsync();
    }
}