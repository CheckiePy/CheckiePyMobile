using System;
using CheckiePyMobile.Services;
using CheckiePyMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckiePyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Logout_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LogoutPage());
        }
    }
}