using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CheckiePyMobile.Helpers;
using CheckiePyMobile.Models;
using Newtonsoft.Json;

namespace CheckiePyMobile.Services
{
    public class NetworkService : INetworkService
    {
        private HttpClient _client;

        private string _baseUrl = "https://checkiepy.com/api";

        private string _token;

        public static INetworkService Instance { get; } = new NetworkService();

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _token);
            }
        }

        protected NetworkService()
        {
            _client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
        }

        public async Task<ResponseModel<List<RepositoryModel>>> GetRepositoriesAsync()
        {
            return await GetAsync<ResponseModel<List<RepositoryModel>>>("/repository/list/");
        }

        public async Task<ResponseModel<List<CodeStyleModel>>> GetCodeStylesAsync()
        {
            return await GetAsync<ResponseModel<List<CodeStyleModel>>>("/code_style/list/");
        }

        public async Task<ResponseModel<CodeStyleModel>> CreateCodeStyleAsync(CodeStyleRequestModel request)
        {
            return await PostAsync<ResponseModel<CodeStyleModel>>("/code_style/create/", JsonConvert.SerializeObject(request));
        }

        public async Task<ResponseModel<CodeStyleModel>> ReadCodeStyleAsync(int id)
        {
            return await GetAsync<ResponseModel<CodeStyleModel>>($"/code_style/read/{id}/");
        }

        private async Task<T> GetAsync<T>(string url) where T : class 
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}{url}");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        private async Task<T> PostAsync<T>(string url, string content) where T : class
        {
            try
            {
                var response = await _client.PostAsync($"{_baseUrl}{url}", new StringContent(content, Encoding.UTF8, "application/json"));
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }
    }
}
