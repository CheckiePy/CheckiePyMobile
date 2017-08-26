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
    public class RepositoryViewModel : INotifyPropertyChanged
    {
        private Page _page;

        private INetworkService _networkService;

        private ObservableCollection<RepositoryModel> _repositories;

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
                Debug.WriteLine(status.Result);
            }
        }

        public async Task LoadRepositoriesAsync()
        {
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
                Debug.WriteLine(repositories.Result);
            }
        }
    }
}
