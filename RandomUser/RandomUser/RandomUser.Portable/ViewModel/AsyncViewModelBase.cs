using GalaSoft.MvvmLight;

namespace RandomUser.Portable.ViewModel
{
    public class AsyncViewModelBase : ViewModelBase
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; RaisePropertyChanged(); }
        }

        private string _loadingMessage;
        public string LoadingMessage
        {
            get { return _loadingMessage; }
            set { _loadingMessage = value; RaisePropertyChanged(); }
        }
    }
}