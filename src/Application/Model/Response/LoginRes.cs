
namespace Application.Model.Response
{
    using Newtonsoft.Json;
    public class LoginRes
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
