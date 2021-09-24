using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace wwdt_ui.Data
{
    public class EntryService
    {
        private IConfigurationRoot _config;

        public EntryService(IConfiguration config)
        {
            _config = (IConfigurationRoot)config;
        }

        public Task<Entry[]> GetEntryAsync()
        {
            var apiUrl = _config.GetValue<string>("ApiUrl");
            var json = new WebClient().DownloadString(apiUrl);
            
            List<Entry> entryList = JsonConvert.DeserializeObject<List<Entry>>(json);

            return Task.FromResult(entryList.ToArray());
        }
    }
}
