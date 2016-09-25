using Windows.ApplicationModel.Resources;
using RandomUser.Portable.Interfaces.Service;

namespace RandomUser.Universal.Service
{
    public class ResourceService : IResourceService
    {
        private readonly ResourceLoader _resLoader = new ResourceLoader();

        public string GetString(string key)
        {
            return _resLoader.GetString(key);
        }
    }
}