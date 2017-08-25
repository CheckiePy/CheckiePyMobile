using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckiePyMobile.Models;
using CheckiePyMobile.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CheckiePyMobile.ViewModels
{
    public class CreateCodeStylePopupViewModel : INotifyPropertyChanged
    {
        private Page _page;

        private INetworkService _networkService;

        private string _name;

        private string _repository;

        public ICommand CancelCommand { get; private set; }

        public ICommand OkCommand { get; private set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(EnableOk));
            }
        }

        public string Repository
        {
            get { return _repository; }
            set
            {
                _repository = value;
                OnPropertyChanged(nameof(EnableOk));
            }
        }

        public bool EnableOk => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Repository);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateCodeStylePopupViewModel(Page page, INetworkService networkService)
        {
            _page = page;
            _networkService = networkService;

            CancelCommand = new Command(Cancel);
            OkCommand = new Command(Ok);
        }

        private async void Ok()
        {
            await PopupNavigation.PopAsync();
            var codeStyle = await _networkService.CreateCodeStyleAsync(new CodeStyleRequestModel
            {
                Name = Name,
                Repository = Repository,
            });
            if (codeStyle == null)
            {
                await _page.DisplayAlert("Error", "An error occurred during request execution", "Close");
            }
            else if (codeStyle.Detail == null)
            {
                while (codeStyle.Result.CalculationStatus == "S")
                {
                    await Task.Delay(100);
                    codeStyle = await _networkService.ReadCodeStyleAsync(codeStyle.Result.Id);
                }
                if (codeStyle.Result.CalculationStatus == "F")
                {
                    await _page.DisplayAlert("Error", "Can't create code style. Incorrect input is possible", "OK");
                }
                else
                {
                    MessagingCenter.Send(this, "CreatedCodeStyle", codeStyle.Result);
                }
            }
            else
            {
                Debug.WriteLine(codeStyle.Detail);
            }
        }

        private async void Cancel()
        {
            await PopupNavigation.PopAsync();
        }
    }
}
