using CheckiePyMobile.Services;
using CheckiePyMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckiePyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            webView.Navigating += WebView_Navigating;
            this.BindingContext = _viewModel = new LoginViewModel(this, NetworkService.Instance);
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains("token"))
            {
                var token = e.Url.Substring(e.Url.IndexOf("token") + 6);
                _viewModel.FinishAuthentication(token);
            }
        }
    }
}
