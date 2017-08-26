using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
    public class RepositoryViewModel : INotifyPropertyChanged
    {
        private Page _page;

        private INetworkService _networkService;

        private ObservableCollection<RepositoryModel> _repositories;

        private bool _isLoaderVisible;

        public ObservableCollection<RepositoryModel> Repositories
        {
            get { return _repositories; }
            set
            {
                _repositories = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateRepositoriesCommand { get; private set; }

        public ICommand ConnectCommand { get; private set; }

        public ICommand DisconnectCommand { get; private set; }

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

        public RepositoryViewModel(Page page, INetworkService networkService)
        {
            _page = page;
            _networkService = networkService;

            UpdateRepositoriesCommand = new Command(UpdateRepositories);
            ConnectCommand = new Command(Connect);
            DisconnectCommand = new Command(Disconnect);

            MessagingCenter.Subscribe<RepositoryConnectionPopupViewModel, Tuple<CodeStyleModel, RepositoryModel>>(this, "ConnectRepository", HandleRepositoryConnection);
        }

        private async void HandleRepositoryConnection(RepositoryConnectionPopupViewModel repositoryConnectionPopupViewModel, Tuple<CodeStyleModel, RepositoryModel> codeStyleRepositoryTuple)
        {
            var connection = await _networkService.ConnectRepositoryAsync(new ConnectionModel
            {
                CodeStyle = codeStyleRepositoryTuple.Item1.Id,
                Repository = codeStyleRepositoryTuple.Item2.Id,
            });
            if (connection == null)
            {
                await _page.DisplayAlert("Error", "An error occurred during request execution", "Close");
            }
            else if (connection.Detail == null)
            {
                RedrawRepository(codeStyleRepositoryTuple.Item2, true, codeStyleRepositoryTuple.Item1.Name);
            }
            else
            {
                Debug.WriteLine(connection.Detail);
            }
        }

        private async void Disconnect(object obj)
        {
            var repository = obj as RepositoryModel;
            if (repository == null)
            {
                Debug.WriteLine("Wrong connect repository method param");
                return;
            }
            var id = await _networkService.DisconnectRepositoryAsync(new IdRequestModel
            {
                Id = repository.Id,
            });
            if (id == null)
            {
                await _page.DisplayAlert("Error", "An error occurred during request execution", "Close");
            }
            else if (id.Detail == null)
            {
                RedrawRepository(repository, false, string.Empty);
            }
            else
            {
                Debug.WriteLine(id.Detail);
            }
        }

        private void RedrawRepository(RepositoryModel repository, bool isConnected, string codeStyleName)
        {
            repository.IsConnected = isConnected;
            repository.CodeStyleName = codeStyleName;
            int i = Repositories.IndexOf(repository);
            Repositories.Remove(repository);
            Repositories.Insert(i, repository);
        }

        private async void Connect(object obj)
        {
            var repository = obj as RepositoryModel;
            if (repository == null)
            {
                Debug.WriteLine("Wrong connect repository method param");
                return;
            }
            var repositoryConnectionPopupPage = new RepositoryConnectionPopupPage
            {
                Repository = repository,
            };
            await repositoryConnectionPopupPage.LoadCodeStylesAsync();
            await PopupNavigation.PushAsync(repositoryConnectionPopupPage);
        }

        private async void UpdateRepositories()
        {
            var status = await _networkService.UpdateRepositoriesAsync();
            if (status == null)
            {
                await _page.DisplayAlert("Error", "An error occurred during request execution", "Close");
            }
            else if (status.Detail == null)
            {
                var lastRepositoriesUpdate = new ResponseModel<RepositoryUpdateModel>
                {
                    Result = new RepositoryUpdateModel
                    {
                        Status = "S",
                    },
                };
                while (lastRepositoriesUpdate.Result.Status == "S")
                {
                    await Task.Delay(100);
                    lastRepositoriesUpdate = await _networkService.GetLastRepositoriesUpdate();
                    if (lastRepositoriesUpdate?.Result == null)
                    {
                        // In this case unknow if repositories successfully updated or not.
                        // Go away silently.
                        Debug.WriteLine("Failed repositories update status request");
                        return;
                    }
                }
                if (lastRepositoriesUpdate.Result.Status == "F")
                {
                    await _page.DisplayAlert("Error", "An error occurred during repositories updating", "OK");
                }
                else
                {
                    await LoadRepositoriesAsync();
                }
            }
            else
            {
                Debug.WriteLine(status.Detail);
            }
        }

        public async Task LoadRepositoriesAsync()
        {
            IsLoaderVisible = true;
            var repositories = await _networkService.GetRepositoriesAsync();
            if (repositories == null)
            {
                await _page.DisplayAlert("Error", "An error occurred during request execution", "Close");
            }
            else if (repositories.Detail == null)
            {
                Repositories = new ObservableCollection<RepositoryModel>(repositories.Result);
            }
            else
            {
                Debug.WriteLine(repositories.Detail);
            }
            IsLoaderVisible = false;
        }
    }
}
