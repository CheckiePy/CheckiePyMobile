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

        private bool _isLoaded = false;

        public RepositoryPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new RepositoryViewModel(this, NetworkService.Instance);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!_isLoaded)
            {
                _isLoaded = true;
                await _viewModel.LoadRepositoriesAsync();
            }
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                listView.SelectedItem = null;
            }
        }
    }
}