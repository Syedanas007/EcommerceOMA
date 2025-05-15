using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace UI.Services
{
    public class ApiClient
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _context;

        public ApiClient(HttpClient client, IHttpContextAccessor context)
        {
            _client = client;
            _context = context;
        }

        private void SetJwt()
        {
            var token = _context.HttpContext?.Session?.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {
            SetJwt();
            var res = await _client.GetAsync(url);
            res.EnsureSuccessStatusCode();
            var content = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T model)
        {
            SetJwt();
            var json = JsonConvert.SerializeObject(model);
            return await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T model)
        {
            SetJwt();
            var json = JsonConvert.SerializeObject(model);
            return await _client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            SetJwt();
            return await _client.DeleteAsync(url);
        }
    }
}
