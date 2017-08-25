using System.ComponentModel;
using System.Windows.Input;
using CheckiePyMobile.Services;
using CheckiePyMobile.Views;
using Xamarin.Forms;

namespace CheckiePyMobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private enum LoginPageMode
        {
            Pending,
            Authentication,
        }

        private LoginPageMode _mode;

        private Page _page;

        private INetworkService _networkService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoginCommand { get; private set; }

        public bool IsLoginButtonVisible => _mode == LoginPageMode.Pending;

        public bool IsWebViewVisible => _mode == LoginPageMode.Authentication;

        public LoginViewModel(Page page, INetworkService networkService)
        {
            _page = page;
            _networkService = networkService;

            LoginCommand = new Command(Authenticate);
        }

        private void Authenticate()
        {
            _mode = LoginPageMode.Authentication;
            UpdateUi();
        }

        private void UpdateUi()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoginButtonVisible)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsWebViewVisible)));
        }

        public void FinishAuthentication(string token)
        {
            _networkService.Token = token;
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
