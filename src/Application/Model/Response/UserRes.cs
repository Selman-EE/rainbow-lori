using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model.Response
{
    public class UserRes
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }
        [JsonProperty("user_name")]
        public string Username { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
