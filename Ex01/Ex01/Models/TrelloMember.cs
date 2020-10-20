using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex01.Models
{

    public class TrelloMember
    {
        [JsonProperty(PropertyName = "avatarHash")]
        public string AvatarHash { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
    }

}
