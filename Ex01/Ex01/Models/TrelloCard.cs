using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex01.Models
{
    public class TrelloCard
    {
        [JsonProperty(PropertyName = "id")]
        public string CardId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "closed")]
        public bool IsClosed { get; set; }

        //This property is added only after the basic UI stuff is finished!
        [JsonProperty(PropertyName = "badges")]
        public CardInformation CardInfo { get; set; }
    }

    public class CardInformation
    {
        [JsonProperty(PropertyName = "comments")]
        public int NumComments { get; set; }
        [JsonProperty(PropertyName = "attachments")]
        public int NumAttachments { get; set; }

    }
}
