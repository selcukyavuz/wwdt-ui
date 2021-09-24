using System;
using Newtonsoft.Json;

namespace wwdt_ui.Data
{
    public class Entry
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "who")]
        public string Who { get; set; }

        [JsonProperty(PropertyName = "when")]
        public DateTime When { get; set; }

        [JsonProperty(PropertyName = "what")]
        public string What { get; set; }

        public Entity CurrentEntity { get; set; }

        public Entity OldEntity { get; set; }

        public string Ip { get; set; }

        public string ComputerName { get; set; }

        public string TicketNumber { get; set; }

        public string Application { get; set; }

        public string _ts { get; set; }

        public DateTime? TimeStamp { get; set; }
        
    }
}
