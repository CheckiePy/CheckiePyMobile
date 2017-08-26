using CheckiePyMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckiePyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();
        }

        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url == "https://github.com/")
            {
                NetworkService.Instance.Token = string.Empty;
                Application.Current.MainPage = new LoginPage();
                Navigation.PopModalAsync();
            }
        }
    }
}