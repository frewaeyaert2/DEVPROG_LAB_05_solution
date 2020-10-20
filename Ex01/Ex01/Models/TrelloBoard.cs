using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ex01.Models
{
    public class TrelloBoard
    {
        [JsonProperty(PropertyName = "id")]
        public string BoardId { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "starred")]
        public bool IsFavorite { get; set; }


        //This property is only added after the base UI stuff is done!
        public string ColorHex { get; set; }

        [JsonExtensionData]
        private Dictionary<string, JToken> _extraJsonData = new Dictionary<string, JToken>();

        [OnDeserialized]
        private void ProccessExtraJsonData(StreamingContext context)
        {
            //prefs.backgroundColor
            JToken prefsData = (JToken)_extraJsonData["prefs"];
            ColorHex = (string)prefsData.SelectToken("backgroundColor");
        }


        //[JsonProperty(PropertyName = "prefs")]
        //public BoardPreference Preferences { get; set; }
    }

    //public class BoardPreference
    //{
    //    public string BackgroundColor { get; set; }

    //}
}
