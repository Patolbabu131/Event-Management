using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static Events.Common.Enums;

namespace Events.Common
{
    public class APIResponse<T>
    {

        [JsonProperty(Order = 5, PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        [JsonProperty(Order = 2, PropertyName = "status")]
        public bool Success { get; set; }

        /// <summary>
        /// Represents Http Status code like 200,401
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Message will be filled incase of Error/Fail 
        /// </summary>
        [JsonProperty(Order = 3, PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(Order = 4, PropertyName = "more_info", NullValueHandling = NullValueHandling.Ignore)]
        public string MoreInfo { get; set; }
    }


    public class ApiResponseObj
    {
        public bool success = false;
        public int code = 0;
        public string message = "";
        public string data = "";
        public string more_info = "";
    }

}
