using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Net.Http.Json;
using System;

namespace wwdt_ui.Data
{
    public class EntryService
    {
        private IConfigurationRoot _config;
        private IWebHostEnvironment _env;
        private readonly string apiUrl = string.Empty;
        private readonly string code = string.Empty;

        public EntryService(IConfiguration config, IWebHostEnvironment env)
        {
            _config = (IConfigurationRoot)config;
            _env = env;
            apiUrl = _config.GetValue<string>("ApiUrl");
            code = _config.GetValue<string>("code");
        }

        public Task<Entry[]> GetEntryAsync()
        {
            var address = $"{apiUrl}Select?code={code}&who=joe";

            string json = string.Empty;

            if (_env.IsDevelopment() && false)
            {
                string dataFolder = System.IO.Path.Combine(_env.ContentRootPath, "Data");
                string dummyFile = System.IO.Path.Combine(dataFolder, "DummyEntry.json");
                json = System.IO.File.ReadAllText(dummyFile);
            }
            else
            {
                json = new WebClient().DownloadString(address);
            }

            List<Entry> entryList = JsonConvert.DeserializeObject<List<Entry>>(json);
            return Task.FromResult(entryList.ToArray());
        }

        public Task<Entry> GetEntryByIdAsync(string id)
        {
            var address = $"{apiUrl}SelectById?code={code}&id={id}";
            string json = new WebClient().DownloadString(address);
            Entry entryList = JsonConvert.DeserializeObject<Entry>(json);
            return Task.FromResult(entryList);
        }

        public Task<bool> DeleteEntryById(string id)
        {
            var address = $"{apiUrl}Delete?code={code}&id={id}";
            Entry entry = new Entry() { Id = id };                 

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(entry),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(address, UriKind.Absolute)
            };

            HttpResponseMessage response = httpClient.Send(request);
            return Task.FromResult<bool>(response.IsSuccessStatusCode);
        }
    }
}
