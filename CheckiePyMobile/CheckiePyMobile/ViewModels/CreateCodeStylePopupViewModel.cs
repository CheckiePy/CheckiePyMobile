using System.Diagnostics;
using System.Windows.Input;
using CheckiePyMobile.Models;
using CheckiePyMobile.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CheckiePyMobile.ViewModels
{
    public class CreateCodeStylePopupViewModel
    {
        private Page _page;

        private INetworkService _networkService;

        public ICommand CancelCommand { get; private set; }

        public ICommand OkCommand { get; private set; }

        public string Name { get; set; } = string.Empty;

        public string Repository { get; set; } = string.Empty;

        public CreateCodeStylePopupViewModel(Page page, INetworkService networkService)
        {
            _page = page;
            _networkService = networkService;

            CancelCommand = new Command(Cancel);
            OkCommand = new Command(Ok);
        }

        private async void Ok()
        {
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
                // TODO: delay and read request
                MessagingCenter.Send(this, "CreatedCodeStyle", codeStyle.Result);
            }
            else
            {
                Debug.WriteLine(codeStyle.Detail);
            }
            await PopupNavigation.PopAsync();
        }

        private async void Cancel()
        {
            await PopupNavigation.PopAsync();
        }
    }
}
