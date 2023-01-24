using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Events.Common
{
    public class Serialization
    {
        public static string GenerateXML(Dictionary<string, string> filters)
        {
            XElement resultXML = new XElement("Parameters", from item in filters select new XElement(item.Key, item.Value));
            return resultXML.ToString();
        }                

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

            public static string ToXML(Object oObject)
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
                using (MemoryStream xmlStream = new MemoryStream())
                {
                    xmlSerializer.Serialize(xmlStream, oObject);
                    xmlStream.Position = 0;
                    xmlDoc.Load(xmlStream);                    
                    return xmlDoc.InnerXml;
                }
            }

            public static string ConvertJsonToXml(string json)
            {
                // Return an XML string representing the JSON data
                XNode document = JsonConvert.DeserializeXNode(json, "Root");
                return document.ToString();
            }

            public static T DeserializeXML<T>(string input) where T : class
            {
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

                using (StringReader sr = new StringReader(input))
                {
                    return (T)ser.Deserialize(sr);
                }
            }
        }
    }

}
