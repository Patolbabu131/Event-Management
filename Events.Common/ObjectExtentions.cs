using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace Events.Common
{
    public static class ObjectExtentions
    {
        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <param name="errorMessageKey">The error message key.</param>
        /// <returns></returns>
        public static string GetValidationMessage(string errorMessageKey)
        {
            string errorMsg = string.Empty;
            var hasValue = false;
            hasValue = ValidationMessages.ValidationMsgs?.TryGetValue(errorMessageKey, out errorMsg) ?? false;
            //            hasValue = ValidationMessages.ValidationMsgs.TryGetValue(errorMessageKey, out errorMsg);
            return hasValue ? errorMsg : errorMessageKey;
        }

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="resultsOutput">The results output.</param>
        /// <returns></returns>
        public static string IsValid(this object obj, out Dictionary<string, string> resultsOutput)
        {
            string errMsgsResponse = null;
            resultsOutput = null;
            string errMsgList = "";
            var instance = obj.GetType().Assembly.CreateInstance((obj.GetType().Name + "Validator"));
            var validator = (IValidator)instance;
            if (validator != null)
            {
                var results = validator.Validate(obj);
                if (!results.IsValid)
                {
                    resultsOutput = new Dictionary<string, string>();
                    foreach (var item in results.Errors)
                    {
                        var i = 0;
                        var popName = item.PropertyName;
                        while (resultsOutput.Keys.Contains(popName))
                        {
                            popName = popName + i;
                            i++;
                        }
                        resultsOutput.Add(popName, item.ErrorMessage);
                        errMsgList += GetValidationMessage(item.ErrorMessage) + ";";
                    }
                }
            }
            if (errMsgList != null)
            {
                errMsgList = errMsgList.TrimEnd(';');
                errMsgsResponse = errMsgList;
            }
            return errMsgsResponse;
        }

    }

    public static class EnumHelper<T>
    where T : struct, Enum // This constraint requires C# 7.3 or later.
    {
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        private static string lookupResource(Type resourceManagerProvider, string resourceKey)
        {
            var resourceKeyProperty = resourceManagerProvider.GetProperty(resourceKey,
                BindingFlags.Static | BindingFlags.Public, null, typeof(string),
                new Type[0], null);
            if (resourceKeyProperty != null)
            {
                return (string)resourceKeyProperty.GetMethod.Invoke(null, null);
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
    }
    public static class ValidationMessages
    {
        public static Dictionary<string, string> ValidationMsgs;
        static readonly object Locker = new object();

        /// <summary>
        /// Initializes the <see cref="ValidationMessages"/> class.
        /// </summary>
        public static void Init(string path)
        {
            if (ValidationMsgs == null || ValidationMsgs.Keys.Count == 0)
            {
                if (Monitor.TryEnter(Locker))
                {
                    try
                    {
                        //var xmlFile = AppDomain.CurrentDomain.BaseDirectory + AppConfiguration.MESSAGEXMLFILEPATH;

                        var doc = XDocument.Load(path);

                        ValidationMsgs = doc.Descendants("messages")
                                                 .First()
                                                 .Elements("message")
                                                 .ToDictionary(x => (string)x.Attribute("key"),
                                                           x => (string)x.Attribute("value"));

                    }
                    finally
                    {
                        Monitor.Exit(Locker);
                    }
                }
            }
        }
    }

    public static class TimeSpanExtensions
    {
        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

    
    }
}
