using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterAsia.Common.ObjectConverter
{
    public class ObjectConverter : IObjectConverter
    {
        /// <summary>
        /// Deserialize the json data
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="jsonData">Json data</param>
        /// <returns>Return object</returns>
        public T Deserialize<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        /// <summary>
        /// Serivalize the json data
        /// </summary>
        /// <typeparam name="T">Generic class type</typeparam>
        /// <param name="entity">Object entity</param>
        /// <returns>Return json</returns>
        public string Serialize<T>(T entity)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()

                // NullValueHandling = NullValueHandling.Ignore
            };

            var serialized = JsonConvert.SerializeObject(entity, settings);
            return serialized;
        }

        /// <summary>
        /// Deserialize the json data
        /// </summary>
        /// <param name="jsonData">JSON data</param>
        /// <param name="t">T type</param>
        /// <returns>Return the object</returns>
        public dynamic Deserialize(string jsonData, Type t)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.DeserializeObject(jsonData, t, settings);
        }
    }
}
