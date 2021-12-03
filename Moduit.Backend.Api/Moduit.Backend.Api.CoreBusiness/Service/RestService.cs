using Moduit.Backend.Api.CoreBusiness.Helper;
using Moduit.Backend.Api.CoreBusiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Moduit.Backend.Api.CoreBusiness.Service
{
    public class RestService : IDisposable, IQuestionRepository
    {
        private readonly HttpClient httpClient;
        private readonly HttpClientHandler handler;
        private readonly string baseURL;
        public RestService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            baseURL = ApiHelper.BaseURL;
            handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Automatic,
            };
            httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseURL),
                DefaultRequestHeaders = { Accept = { MediaTypeWithQualityHeaderValue.Parse("application/json") } }
            };
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public async Task<BackendQuestionOneResponse> GetQuestionOneAsync()
        {
            SwaggerClient swaggerClient = new SwaggerClient(baseURL, httpClient);
            var response = await swaggerClient.OneAsync().ConfigureAwait(false);
            return response;
        }

        public async Task<IEnumerable<BackendQuestionOneResponse>> GetQuestionTwoAsync(string title, string tags, int pageSize)
        {
            SwaggerClient swaggerClient = new SwaggerClient(baseURL, httpClient);
            var response = await swaggerClient.TwoAsync().ConfigureAwait(false);
            //Filter Title Or Description
            response = response.Where(r => r.Title.ToLower().Contains(title.ToLower()) || r.Description.ToLower().Contains(title.ToLower()));
            //Filter tags;
            response = response.Where(r => r.Tags != null && r.Tags.Any(t => t.ToLower() == tags.ToLower()));
            //Order By Id Desc, Take PakeSize default 3;
            response = response.OrderByDescending(r => r.Id).Take(pageSize);
            return response;
        }

        public async Task<IEnumerable<BackendQuestionOneResponse>> GetQuestionThreeAsync(BackendQuestionThreeResponse entity)
        {
            return await GetBackendQuestionOneResponses(entity).ConfigureAwait(false);
        }

        protected Task<IEnumerable<BackendQuestionOneResponse>> GetBackendQuestionOneResponses(BackendQuestionThreeResponse entity)
        {
            List<BackendQuestionOneResponse> output = new List<BackendQuestionOneResponse>();
            foreach (var item in entity.Items)
            {
                output.Add(new BackendQuestionOneResponse()
                {
                    Id = entity.Id,
                    Category = entity.Category,
                    Title = item.Title,
                    Description = item.Description,
                    Footer = item.Footer,
                    CreatedAt = entity.CreatedAt,
                });
            }
            return Task.FromResult((IEnumerable<BackendQuestionOneResponse>)output);
        }
    }
}
