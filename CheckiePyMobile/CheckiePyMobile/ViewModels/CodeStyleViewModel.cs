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

        public ICommand OpenCreateCodeStylePopupCommand { get; set; }

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

            MessagingCenter.Subscribe<CreateCodeStylePopupViewModel, CodeStyleModel>(this, "CreatedCodeStyle", HandleCodeStyleCreation);
        }

        private void HandleCodeStyleCreation(CreateCodeStylePopupViewModel createCodeStylePopupViewModel, CodeStyleModel codeStyleModel)
        {
            CodeStyles.Add(codeStyleModel);
        }

        public async Task LoadCodeStylesAsync()
        {
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
        }

        private async void OpenCreateCodeStylePopup()
        {
            await PopupNavigation.PushAsync(new CreateCodeStylePopupPage());
        }
    }
}
