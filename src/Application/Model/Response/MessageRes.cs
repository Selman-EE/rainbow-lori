using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Model.Response
{
    [JsonObject]
    public class MessageRes
    {
        public string SenderUsername { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverUsername { get; set; }
        public string Text { get; set; }
        public string ChatId { get; set; }
    }
}
