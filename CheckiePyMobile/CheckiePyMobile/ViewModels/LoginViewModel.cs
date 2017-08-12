﻿using System.ComponentModel;
using System.Windows.Input;
using CheckiePyMobile.Services;
using Xamarin.Forms;

namespace CheckiePyMobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private enum LoginPageMode
        {
            Pending,
            Authentication,
        }

        private LoginPageMode _mode;

        private Page _page;

        private NetworkService _networkService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoginCommand { get; private set; }

        public bool IsLoginButtonVisible => _mode == LoginPageMode.Pending;

        public bool IsWebViewVisible => _mode == LoginPageMode.Authentication;

        public LoginViewModel(Page page, NetworkService networkService)
        {
            _page = page;
            _networkService = networkService;

            LoginCommand = new Command(Authenticate);
        }

        private void Authenticate()
        {
            _mode = LoginPageMode.Authentication;
            UpdateUi();
        }

        private void UpdateUi()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoginButtonVisible)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsWebViewVisible)));
        }

        public void FinishAuthentication(string token)
        {
            _networkService.Token = token;
            // TODO: Redirect to code styles / repositories
        }
    }
}