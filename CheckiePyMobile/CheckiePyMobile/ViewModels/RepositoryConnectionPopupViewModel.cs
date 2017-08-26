using System;
using System.Windows.Input;
using CheckiePyMobile.Models;
using CheckiePyMobile.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CheckiePyMobile.ViewModels
{
    public class RepositoryConnectionPopupViewModel
    {
        private Page _page;

        private INetworkService _networkService;

        public RepositoryModel Repository { get; set; }

        public CodeStyleModel CodeStyle { get; set; }

        public ICommand OkCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public RepositoryConnectionPopupViewModel(Page page, INetworkService networkService)
        {
            _page = page;
            _networkService = networkService;

            OkCommand = new Command(Connect);
            CancelCommand = new Command(Cancel);
        }

        private async void Connect()
        {
            await PopupNavigation.PopAsync();
            MessagingCenter.Send(this, "ConnectRepository", Tuple.Create(CodeStyle, Repository));
        }

        private async void Cancel()
        {
            await PopupNavigation.PopAsync();
        }
    }
}
