using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckiePyMobile.Models;
using CheckiePyMobile.Services;
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
        }

        public async Task LoadCodeStylesAsync()
        {
            var codeStyles = await _networkService.GetCodeStylesAsync();
            if (codeStyles.Detail == null)
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
            await _page.DisplayAlert("test", "test", "test");
        }
    }
}
