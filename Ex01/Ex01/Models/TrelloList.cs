using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex01.Models
{
    public class TrelloList
    {
        [JsonProperty(PropertyName ="id")] //name in Json
        public string ListId { get; set; } // name we want to use 
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
