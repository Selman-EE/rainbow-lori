namespace Application.Common
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    [JsonObject]    
    public class Result
    {
        public Result() { }
        internal Result(bool succeeded, IEnumerable<string> messages)
        {
            Succeeded = succeeded;
            Messages = messages;
        }
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }
        [JsonProperty("messages")]
        public IEnumerable<string> Messages { get; set; } = new HashSet<string>();
        public static Result Success(IEnumerable<string> messages)
        {
            return new Result(true, messages);
        }
        public static Result Failure(IEnumerable<string> messages)
        {
            return new Result(false, messages);
        }

    }
}
