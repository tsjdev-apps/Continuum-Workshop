using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RandomUser.Portable.Interfaces.Service;
using RandomUser.Portable.Model;
using RandomUser.Universal.View.Pages;

namespace RandomUser.Universal.Service
{
    public class ProjectionService : IProjectionService
    {
        private int? _mainViewId;
        private int? _secondViewId;
        private bool _isProjecting;
        
        public bool IsProjecting => _isProjecting;

        public async void StartProjection(User selectedUser)
        {
            if (!ProjectionManager.ProjectionDisplayAvailable)
                return;

            _mainViewId = ApplicationView.GetForCurrentView().Id;

            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _secondViewId = ApplicationView.GetForCurrentView().Id;
                var rootFrame = new Frame();
                rootFrame.Navigate(typeof(ProjectionUserDetailPage), selectedUser);
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            });

            if (_secondViewId == null)
                return;

            _isProjecting = true;
            await ProjectionManager.StartProjectingAsync(_secondViewId.Value, _mainViewId.Value);
        }

        public async Task StopProjectionAsync()
        {
            if (_mainViewId == null || _secondViewId == null)
                return;

            await ProjectionManager.StopProjectingAsync(_secondViewId.Value, _mainViewId.Value);
            _isProjecting = false;
        }
    }
}