using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DermPOCMobile.Services
{
    public class HttpService : IHttpService
    {
        HttpClient _client;
        public HttpService()
        {
            _client = new HttpClient();
        }

        public async Task<string> PredictImageAsync(byte[] image)
        {
            var uri = new Uri(string.Format("someUrl", string.Empty));

            var json = JsonConvert.SerializeObject(image);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
                Debug.WriteLine(response.Content.ReadAsStringAsync());
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
