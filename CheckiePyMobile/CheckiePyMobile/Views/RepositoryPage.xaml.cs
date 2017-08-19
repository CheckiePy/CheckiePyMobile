using CheckiePyMobile.Services;
using CheckiePyMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckiePyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RepositoryPage : ContentPage
    {
        private RepositoryViewModel _viewModel;

        public RepositoryPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new RepositoryViewModel(this, NetworkService.Instance);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadRepositoriesAsync();
        }
    }
}