using System.Collections.Generic;
using System.Threading.Tasks;
using CheckiePyMobile.Models;

namespace CheckiePyMobile.Services
{
    public class FakeNetworkService : INetworkService
    {
        public string Token { get; set; }
        public async Task<ResponseModel<List<RepositoryModel>>> GetRepositoriesAsync()
        {
            return new ResponseModel<List<RepositoryModel>>
            {
                Result = new List<RepositoryModel>
                {
                    new RepositoryModel
                    {
                        Id = 1,
                        IsConnected = false,
                        Name = "repository1",
                    },
                    new RepositoryModel
                    {
                        Id = 2,
                        IsConnected = true,
                        Name = "SecondRepository",
                    }
                },
            };
        }

        public async Task<ResponseModel<List<CodeStyleModel>>> GetCodeStylesAsync()
        {
            return new ResponseModel<List<CodeStyleModel>>
            {
                Result = new List<CodeStyleModel>
                {
                    new CodeStyleModel
                    {
                        Id = 1,
                        Name = "codeStyle1",
                    },
                    new CodeStyleModel
                    {
                        Id = 2,
                        Name = "SecondCodeStyle",
                    },
                },
            };
        }

        public Task<ResponseModel<CodeStyleModel>> CreateCodeStyleAsync(CodeStyleRequestModel request)
        {
            throw new System.NotImplementedException();
        }
    }
}
