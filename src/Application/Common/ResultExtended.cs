
namespace Application.Common
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    [JsonObject]    
    public class ResultExtended<T> : Result
    {
        internal ResultExtended(T response, bool succeeded)
        {
            Data = response;
            Succeeded = succeeded;
        }
        internal ResultExtended(T response, bool succeeded, IEnumerable<string> messages)
        {
            Data = response;
            Succeeded = succeeded;
            Messages = messages;
        }
        public ResultExtended() { }
        public ResultExtended(T response)
        {
            Data = response;
        }
        [JsonProperty("data")]
        public virtual T Data { get; set; }

        public static ResultExtended<T> Success(T response)
        {
            return new ResultExtended<T>(response, true);
        }
        public static ResultExtended<T> Success(T response, IEnumerable<string> messages)
        {
            return new ResultExtended<T>(response, true);
        }
        public static ResultExtended<T> Failure(T response)
        {
            return new ResultExtended<T>(response, false);
        }
        public static ResultExtended<T> Failure(T response, IEnumerable<string> messages)
        {
            return new ResultExtended<T>(response, false);
        }
    }
}
