using System;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Http;
using Google.Apis.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otomatik.Library.Web.Core.Enums;

namespace Otomatik.Library.Web.Services
{
    public class BookFinder : IBookFinder
    {
        private readonly BooksService _service;

        public BookFinder()
        {
            _service = new BooksService(
                new BaseClientService.Initializer
                {
                    ApplicationName = Environment.GetEnvironmentVariable(EnvironmentParameters.GoogleAppName),
                    ApiKey = Environment.GetEnvironmentVariable(EnvironmentParameters.GoogleApiKey),
                    HttpClientInitializer = new GBookRequest()
                });
        }

        public async Task<IList<Volume>> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Volume>();
            }

            var result = await _service.Volumes.List(searchTerm).ExecuteAsync();

            return result?.Items ?? new List<Volume>();
        }

        public async Task<Volume> GetAsync(string id)
        {
            var result = await _service.Volumes.Get(id).ExecuteAsync();

            return result;
        }
    }

    public class GBookRequest : IConfigurableHttpClientInitializer
    {
        public void Initialize(ConfigurableHttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("country", "TR");
        }
    }
}
