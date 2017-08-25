using CheckiePyMobile.Services;
using CheckiePyMobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace CheckiePyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCodeStylePopupPage : PopupPage
    {
        public CreateCodeStylePopupPage()
        {
            InitializeComponent();
            this.BindingContext = new CreateCodeStylePopupViewModel(this, NetworkService.Instance);
        }
    }
}