using System.ComponentModel;
using System.Reflection;
using System.Web;
using static Events.Common.Enums;

namespace Events.Common
{
    public static class CommonExtensions
    {
        #region Enum Extensions

        public static string GetDiscriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return source.ToString();
                }
            }
            else return "";
        }

        public static string GetDefaultValueAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            if (fi != null)
            {
                DefaultValueAttribute[] attributes = (DefaultValueAttribute[])fi.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Value.ToString();
                }
                else
                {
                    return source.ToString();
                }
            }
            else return "";

        }

        #endregion

        #region String Extensions

        public static string GetDomainEmail(this ServiceTypes serviceType)
        {
            string randomAplphaNumric = CommonMethods.RandomAlphaNumericString(8);
            return randomAplphaNumric + "_" + serviceType.GetDefaultValueAttr() + "_iacs@iacs.cloud";
        }

        public static string UrlEncode(this string text)
        {
            return HttpUtility.UrlEncode(text);
        }

        public static string UrlDecode(this string text)
        {
            return HttpUtility.UrlDecode(text);
        }

        public static string Base64Encode(this string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string text)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(text);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion

        #region Array Extensions
        public static int FindIndex<T>(this T[] array, T item)
        {
            try
            {
                return array
                    .Select((element, index) => new KeyValuePair<T, int>(element, index))
                    .First(x => x.Key.Equals(item)).Value;
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
        }
        #endregion


    }
}