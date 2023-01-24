using System.Xml.Linq;

namespace Events.Web.Services
{

    public class CommonService
    {       

        public static string GenerateXML(Dictionary<string, string> filters)
        {
            XElement resultXML = new XElement("Parameters", from item in filters select new XElement(item.Key, item.Value));
            return resultXML.ToString();
        }

        /// <summary>
        /// For Json Serialization and Deserialization
        /// </summary>
        public static class Json
        {
            /// <summary>
            /// Method to convert json string to custom object
            /// </summary>
            /// <typeparam name="T">Type of custom object to be returned</typeparam>
            /// <param name="jsonData">Json string to convert</param>
            /// <returns>Custom object</returns>
            public static T Deserialize<T>(string jsonData)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
            }

            /// <summary>
            /// Method to convert custom object to json string
            /// </summary>
            /// <typeparam name="T">Type of custom object to be converted</typeparam>
            /// <param name="entity">Custom object to convert</param>
            /// <returns>Json string</returns>
            public static string Serialize<T>(T entity)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
            }

            /// <summary>
            /// Method to convert json string to custom object
            /// </summary>
            /// <param name="jsonData">Json string to convert</param>
            /// <param name="t">Type of custom object to be returned</param>
            /// <returns>Custom object</returns>
            public static dynamic Deserialize(string jsonData, Type t)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData, t);
            }
        }

        /// <summary>
        /// For fetching the GenericResponse Of Any Model When returing the Partial View as String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class GenericJsonResponse<T>
        {
            public GenericJsonResponse()
            {
                IsSucceed = false;
                Message = string.Empty;
                Data = default(T);
            }
            public bool IsSucceed { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }
        }    
        
        public class Result
        {

            public Result()
            {
                IsSucceed = false;
                Message = string.Empty;
                Data = string.Empty; ;
            }

            public bool IsSucceed { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }


    }

}
