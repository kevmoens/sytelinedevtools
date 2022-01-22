using System.Windows.Controls;
using System.Windows.Navigation;

namespace SyteLineDevTools.MVVM.Services
{

    public class WpfNavigationService : INavigationService
    {
        private NavigationService _navigationService;
        public WpfNavigationService(Frame frame)
        {
            _navigationService = frame.NavigationService;
        }
        public void GoBack()
        {
            _navigationService.GoBack();
        }

        public void GoForward()
        {
            _navigationService.GoForward();
        }

        public bool Navigate(IView view)
        {
            return _navigationService.Navigate(view);
        }

        public bool Navigate(string uri)
        {
            return _navigationService.Navigate(new Uri(uri));
        }

        public bool Navigate(Uri uri)
        {
            return _navigationService.Navigate(uri);
        }

        public void Refresh()
        {
            _navigationService.Refresh();
        }
    }
}
