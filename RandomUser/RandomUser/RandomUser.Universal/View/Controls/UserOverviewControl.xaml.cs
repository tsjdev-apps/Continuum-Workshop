using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using RandomUser.Portable.Model;

namespace RandomUser.Universal.View.Controls
{
    public sealed partial class UserOverviewControl : UserControl
    {
        private bool _isPointerPressed = false;

        public UserOverviewControl()
        {
            this.InitializeComponent();
        }

        protected override void OnHolding(HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == HoldingState.Started)
            {
                var pointerPosition = e.GetPosition(null);

                var frameworkElement = e.OriginalSource as FrameworkElement;
                var myObject = frameworkElement?.DataContext as User;
                if (myObject == null)
                    return;

                ShowContextMenu(myObject, null, pointerPosition);
                e.Handled = true;

                _isPointerPressed = false;

                var itemsToCancel = VisualTreeHelper.FindElementsInHostCoordinates(pointerPosition, UserList);
                foreach (var uiElement in itemsToCancel)
                     uiElement.CancelDirectManipulations();
            }

            base.OnHolding(e);
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            _isPointerPressed = true;

            base.OnPointerPressed(e);
        }

        protected override void OnRightTapped(RightTappedRoutedEventArgs e)
        {
            if (_isPointerPressed)
            {
                var frameworkElement = e.OriginalSource as FrameworkElement;
                var myObject = frameworkElement?.DataContext as User;
                if (myObject == null)
                    return;

                ShowContextMenu(myObject, null, e.GetPosition(null));
                e.Handled = true;
            }

            base.OnRightTapped(e);
        }

        private void ShowContextMenu(User data, UIElement target, Point offset)
        {
            if (!ProjectionManager.ProjectionDisplayAvailable)
                return;

            var contextMenu = Resources["ContextMenu"] as MenuFlyout;
            if (contextMenu?.Items == null)
                return;

            foreach (var menuFlyoutItemBase in contextMenu.Items)
                menuFlyoutItemBase.DataContext = data;

            contextMenu.ShowAt(target, offset);
        }
    }
}
