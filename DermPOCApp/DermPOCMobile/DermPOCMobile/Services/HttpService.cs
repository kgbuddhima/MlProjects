using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DermPOCMobile.Services
{
    public class HttpService : IHttpService
    {
        HttpClient _client;
        public HttpService()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _client = new HttpClient(handler);
        }

        public async Task<string> PredictImageAsync(byte[] image,string filepath)
        {
            HttpResponseMessage response = null;
            try
            {
                var uri = new Uri(string.Format("https://10.0.2.2:5001/api/predict", string.Empty));

                /*var json = JsonConvert.SerializeObject(image);
                var content = new StringContent(json, Encoding.UTF8, "application/json");*/

                //string boundary = "---8d0f01e6b3b5dafaaadaada";
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                ByteArrayContent byteArrayContent = new ByteArrayContent(image);
                byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                byteArrayContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "imgFile.jpg",
                    Name = "imgFile"
                };
                multipartFormDataContent.Add(byteArrayContent);


                response = await _client.PostAsync(uri, multipartFormDataContent);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                    Debug.WriteLine(response.Content.ReadAsStringAsync());
                }

            }
            catch (HttpRequestException ex) 
            {
            
            }
            catch (Exception ex)
            {

            }
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
