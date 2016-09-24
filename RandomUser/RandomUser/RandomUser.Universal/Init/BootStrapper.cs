using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using RandomUser.Portable.Interfaces.Repository;
using RandomUser.Portable.Interfaces.Service;
using RandomUser.Portable.Repository;
using RandomUser.Portable.Service;
using RandomUser.Portable.ViewModel;
using RandomUser.Universal.Service;

namespace RandomUser.Universal.Init
{
    public class BootStrapper
    {
        public BootStrapper()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register Services
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IHttpClientService, HttpClientService>();
            SimpleIoc.Default.Register<IMappingService, MappingService>();
            SimpleIoc.Default.Register<IProjectionService, ProjectionService>();
            SimpleIoc.Default.Register<IResourceService, ResourceService>();
            
            // Register Repositories
            SimpleIoc.Default.Register<IUserRepository, UserRepository>();

            // Register ViewModels
            SimpleIoc.Default.Register<UserOverviewViewModel>();
            SimpleIoc.Default.Register<UserDetailViewModel>();
        }

        public UserOverviewViewModel UserOverviewViewModel => SimpleIoc.Default.GetInstance<UserOverviewViewModel>();
        public UserDetailViewModel UserDetailViewModel => SimpleIoc.Default.GetInstance<UserDetailViewModel>();
    }
}