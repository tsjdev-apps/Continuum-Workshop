using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using RandomUser.Portable.Model;

namespace RandomUser.Universal.View.Pages
{
    public sealed partial class ProjectionUserDetailPage : Page
    {
        public ProjectionUserDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var selectedUser = e.Parameter as User;

            if (selectedUser == null)
                return;

            UserImage.ImageSource = GetUserImage(selectedUser.PictureUrl);
            UserFullName.Text = selectedUser.FullName;
            UserMail.Text = selectedUser.Email;
            UserPhone.Text = selectedUser.Phone;
            UserCell.Text = selectedUser.Cell;
        }

        private BitmapImage GetUserImage(string pictureUrl)
        {
            return new BitmapImage(new Uri(pictureUrl));
        }
    }
}
