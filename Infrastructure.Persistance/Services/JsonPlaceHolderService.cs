using Core.Application.Helper;
using Core.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Services
{
    public class JsonPlaceHolderService : IJsonPlaceHolderService
    {
        private readonly HttpClient _httpClient;

        private readonly string _placeHolderBaseUrl;


        public JsonPlaceHolderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _placeHolderBaseUrl = configuration["jsonPlaceHolder:baseUrl"] ?? throw new ArgumentNullException(nameof(configuration), "Configuration for 'apiEndpoint:getAll' is missing.");
        }

        public async Task<List<JsonPlaceHolderModel>> GetData()
        {
            var response = await _httpClient.GetStringAsync(_placeHolderBaseUrl + "/posts");
            var data = JsonConvert.DeserializeObject<List<JsonPlaceHolderModel>>(response);
            return data;
        }

        public async Task<JsonPlaceHolderModel> Create(JsonPlaceHolderModel request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_placeHolderBaseUrl + "/posts", content);
            var responseData = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<JsonPlaceHolderModel>(responseData);
            return data;
        }

    }
}
