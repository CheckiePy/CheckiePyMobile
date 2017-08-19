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
    public class RepositoryViewModel : INotifyPropertyChanged
    {
        private Page _page;

        private NetworkService _networkService;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RepositoryViewModel(Page page, NetworkService networkService)
        {
            _page = page;
            _networkService = networkService;
        }

        public async Task LoadRepositoriesAsync()
        {
            var repositories = await _networkService.GetRepositoriesAsync();
            if (repositories.Detail == null)
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
