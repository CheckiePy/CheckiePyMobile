using System.Collections.Generic;
using System.Threading.Tasks;
using CheckiePyMobile.Models;

namespace CheckiePyMobile.Services
{
    public interface INetworkService
    {
        string Token { get; set; }
        Task<ResponseModel<List<RepositoryModel>>> GetRepositoriesAsync();
        Task<ResponseModel<List<CodeStyleModel>>> GetCodeStylesAsync();
        Task<ResponseModel<CodeStyleModel>> CreateCodeStyleAsync(CodeStyleRequestModel request);
        Task<ResponseModel<CodeStyleModel>> ReadCodeStyleAsync(int id);
        Task<ResponseModel<int>> DeleteCodeStyleAsync(IdRequestModel request);
        Task<ResponseModel<string>> UpdateRepositoriesAsync();
        Task<ResponseModel<RepositoryUpdateModel>> GetLastRepositoriesUpdate();
        Task<ResponseModel<ConnectionModel>> ConnectRepositoryAsync(ConnectionModel request);
        Task<ResponseModel<int>> DisconnectRepositoryAsync(IdRequestModel request);
    }
}
