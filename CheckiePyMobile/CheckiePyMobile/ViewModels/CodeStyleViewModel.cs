using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckiePyMobile.Models;
using CheckiePyMobile.Services;
using CheckiePyMobile.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CheckiePyMobile.ViewModels
{
    public class CodeStyleViewModel : INotifyPropertyChanged
    {
        private Page _page;

        private INetworkService _networkService;

        private ObservableCollection<CodeStyleModel> _codeStyles;

        private bool _isLoaderVisible;

        public ICommand OpenCreateCodeStylePopupCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public bool IsLoaderVisible
        {
            get { return _isLoaderVisible; }
            set
            {
                _isLoaderVisible = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsListViewVisible));
            }
        }

        public bool IsListViewVisible => !IsLoaderVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<CodeStyleModel> CodeStyles
        {
            get { return _codeStyles; }
            set
            {
                _codeStyles = value;
                OnPropertyChanged();
            }
        }

        public CodeStyleViewModel(Page page, INetworkService networkService)
        {
            _page = page;
            _networkService = networkService;

            OpenCreateCodeStylePopupCommand = new Command(OpenCreateCodeStylePopup);
            DeleteCommand = new Command(Delete);

            MessagingCenter.Subscribe<CreateCodeStylePopupViewModel, CodeStyleModel>(this, "CodeStyleCreated", HandleCodeStyleCreation);
        }

        private async void Delete(object obj)
        {
            var codeStyle = obj as CodeStyleModel;
            if (codeStyle == null)
            {
                Debug.WriteLine("Wrong code style delete param type");
                return;
            }
            var id = await _networkService.DeleteCodeStyleAsync(new IdRequestModel { Id = codeStyle.Id });
            if (id == null)
            {
                await _page.DisplayAlert("Error", "An error occurred during request execution", "Close");
            }
            else if (id.Detail == null)
            {
                CodeStyles.Remove(codeStyle);
            }
            else
            {
                Debug.WriteLine(id.Detail);
            }
        }

        private void HandleCodeStyleCreation(CreateCodeStylePopupViewModel createCodeStylePopupViewModel, CodeStyleModel codeStyleModel)
        {
            CodeStyles.Add(codeStyleModel);
        }

        public async Task LoadCodeStylesAsync()
        {
            IsLoaderVisible = true;
            var codeStyles = await _networkService.GetCodeStylesAsync();
            if (codeStyles == null)
            {
                await _page.DisplayAlert("Error", "An error occurred during request execution", "Close");
            }
            else if (codeStyles.Detail == null)
            {
                CodeStyles = new ObservableCollection<CodeStyleModel>(codeStyles.Result);
            }
            else
            {
                Debug.WriteLine(codeStyles.Detail);
            }
            IsLoaderVisible = false;
        }

        private async void OpenCreateCodeStylePopup()
        {
            await PopupNavigation.PushAsync(new CreateCodeStylePopupPage());
        }
    }
}
