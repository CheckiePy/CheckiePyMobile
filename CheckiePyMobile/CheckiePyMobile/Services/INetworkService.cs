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
    }
}
