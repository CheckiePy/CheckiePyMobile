using CheckiePyMobile.Services;
using CheckiePyMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckiePyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodeStylePage : ContentPage
    {
        private CodeStyleViewModel _viewModel;

        public CodeStylePage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new CodeStyleViewModel(this, NetworkService.Instance);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadCodeStylesAsync();
        }
    }
}