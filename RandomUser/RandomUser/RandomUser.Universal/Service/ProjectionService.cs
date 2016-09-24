using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RandomUser.Portable.Interfaces.Service;
using RandomUser.Universal.View.Pages;

namespace RandomUser.Universal.Service
{
    public class ProjectionService : IProjectionService
    {
        private int? _mainViewId;
        private int? _secondViewId;
        

        public async void StartProjection()
        {
            StopProjection();

            if (!ProjectionManager.ProjectionDisplayAvailable)
                return;

            _mainViewId = ApplicationView.GetForCurrentView().Id;

            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _secondViewId = ApplicationView.GetForCurrentView().Id;
                var rootFrame = new Frame();
                rootFrame.Navigate(typeof(UserDetailPage));
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
            });

            if (_secondViewId != null)
                await ProjectionManager.StartProjectingAsync(_secondViewId.Value, _mainViewId.Value);
        }

        public async void StopProjection()
        {
            if (_mainViewId != null && _secondViewId != null)
                await ProjectionManager.StopProjectingAsync(_secondViewId.Value, _mainViewId.Value);
        }
    }
}