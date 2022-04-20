using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.CustomEntities;
using Core.Interfaces;
using Core.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly AIServiceOptions _options;

        public AIService(HttpClient httpClient, IOptions<AIServiceOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<List<Prediction> > SendRequest(string filename, string token)
        {

            _httpClient.DefaultRequestHeaders.Add(_options.AuthHeader, token);
            string filenameWithoutPath = Path.GetFileName(filename);

            using MultipartFormDataContent multipartFormContent = new();
            StreamContent fileStreamContent = new(File.OpenRead(filename));
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(_options.MediaType);

            multipartFormContent.Add(
                fileStreamContent,
                name: _options.FormContentVideoKey,
                fileName: filenameWithoutPath
            );

            HttpResponseMessage response = await _httpClient.PostAsync(
                _options.Route,
                multipartFormContent
            );
            string body = await response.Content.ReadAsStringAsync();
            List<Prediction> predictions = JsonConvert.DeserializeObject<List<Prediction> >(body);
            return predictions;
        }
    }
}
