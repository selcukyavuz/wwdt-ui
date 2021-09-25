using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace wwdt_ui.Data
{
    public class EntryService
    {
        private IConfigurationRoot _config;
        private IWebHostEnvironment _env;

        public EntryService(IConfiguration config, IWebHostEnvironment env)
        {
            _config = (IConfigurationRoot)config;
            _env = env;
        }

        public Task<Entry[]> GetEntryAsync()
        {
            var apiUrl = _config.GetValue<string>("ApiUrl") + "Select?code=Pp4leohUDQIcLGSnswf6z2/iatVCHOdoAGPOez5K8r/7PEbyF3al2A==&who=joe";
            string json = string.Empty;

            if (_env.IsDevelopment())
            {
                string dataFolder = System.IO.Path.Combine(_env.ContentRootPath, "Data");
                string dummyFile = System.IO.Path.Combine(dataFolder, "DummyEntry.json");
                json = System.IO.File.ReadAllText(dummyFile);
            }
            else
            {
                json = new WebClient().DownloadString(apiUrl);
            }


            List<Entry> entryList = JsonConvert.DeserializeObject<List<Entry>>(json);

            return Task.FromResult(entryList.ToArray());
        }

        public Task<Entry> GetEntryByIdAsync(string id)
        {
            var apiUrl = _config.GetValue<string>("ApiUrl") + "SelectById?code=&id=" + id;
            string json = string.Empty;

            json = new WebClient().DownloadString(apiUrl);

            Entry entryList = JsonConvert.DeserializeObject<Entry>(json);

            return Task.FromResult(entryList);
        }
    }
}
