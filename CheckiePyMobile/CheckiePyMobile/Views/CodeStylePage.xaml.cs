using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckiePyMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckiePyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodeStylePage : ContentPage
    {
        public CodeStylePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var codeStyles = await NetworkService.Instance.GetCodeStylesAsync();
            var repositories = await NetworkService.Instance.GetRepositoriesAsync();
            int x = 0;
        }
    }
}