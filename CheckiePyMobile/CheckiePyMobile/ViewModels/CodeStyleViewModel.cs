using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CheckiePyMobile.Models;
using CheckiePyMobile.Services;
using Xamarin.Forms;

namespace CheckiePyMobile.ViewModels
{
    public class CodeStyleViewModel : INotifyPropertyChanged
    {
        private Page _page;

        private NetworkService _networkService;

        private ObservableCollection<CodeStyleModel> _codeStyles;

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

        public CodeStyleViewModel(Page page, NetworkService networkService)
        {
            _page = page;
            _networkService = networkService;
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
    }
}
