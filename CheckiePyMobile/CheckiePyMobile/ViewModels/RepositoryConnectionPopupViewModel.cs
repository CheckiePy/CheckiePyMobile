using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CheckiePyMobile.Models;
using CheckiePyMobile.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CheckiePyMobile.ViewModels
{
    public class RepositoryConnectionPopupViewModel : INotifyPropertyChanged
    {
        private Page _page;

        private INetworkService _networkService;

        private CodeStyleModel _codeStyle;

        public RepositoryModel Repository { get; set; }

        public CodeStyleModel CodeStyle
        {
            get { return _codeStyle; }
            set
            {
                _codeStyle = value;
                OnPropertyChanged(nameof(IsOkEnabled));
            }
        }

        public ICommand OkCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public bool IsOkEnabled => CodeStyle != null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
