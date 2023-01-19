
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Events.Common
{
    public static class CommonMethods
    {

        public static decimal ConverToCurrencyValue(decimal AmountToConvert, decimal? ConversionRate)
        {
            if (!ConversionRate.HasValue)
                ConversionRate = 0;
            return Math.Round(AmountToConvert * ConversionRate.Value);
        }

        public static decimal ConvertDecimalToPlace(decimal AmountToConvert, int precision)
        {
            return Math.Round(AmountToConvert, precision, MidpointRounding.ToEven);
        }

        private static Random random = new Random();
        public static string RandomAlphaNumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomNumericString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string Base64Encode(string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string text)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(text);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #region Document Processing
        public static string GenerateFileName(DateTime dt,string fileName, string CustomerName)
        {
            try
            {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = dt.ToString("dd-MM-yyyy") + "/" + CustomerName + "/" +
                    dt.ToString("yyyyMMdd\\THHmmssfff") + "." +
                   strName[strName.Length - 1];
                return strFileName;
            }
            catch (Exception ex)
            {
                return fileName;
            }
        }

        public static string uploadImage(string filename, Stream filestream, string connectionstring, string containername)
        {
            try
            {
                var fileUrl = "";
                BlobContainerClient container = new BlobContainerClient(connectionstring, containername);
                try
                {
                    BlobClient blob = container.GetBlobClient(filename);                    
                    filestream.Position = 0;
                    blob.Upload(filestream);
                    fileUrl = blob.Uri.AbsoluteUri;
                }
                catch (Exception ex) { }

                return fileUrl;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetBase64PrefixString(string documentextension)
        {
            string finalstring = string.Empty;
            switch (documentextension)
            {
                case "pdf":
                    finalstring = "data:application/pdf;base64,";
                    break;

                case "tif":
                    finalstring = "data:image/tiff;base64,";
                    break;

            }

            return finalstring;
        }


        #endregion
    }

    public static class Ext
    {
        public static bool In<T>(this T val, params T[] values) where T : struct
        {
            return values.Contains(val);
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime ConvertToTimeZone(this DateTime dt, string timeZoneId)
        {           
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTime cstTime = TimeZoneInfo.ConvertTime(dt, timeZone);
            return cstTime;
        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt) =>
       dt.FirstDayOfWeek().AddDays(6);

    }

    public static class StringExtensions
    {
        public static string Mask(this string source, int start, int maskLength)
        {
            return source.Mask(start, maskLength, 'X');
        }

        public static string Mask(this string source, int start, int maskLength, char maskCharacter)
        {
            if (start > source.Length - 1)
            {
                throw new ArgumentException("Start position is greater than string length");
            }

            if (maskLength > source.Length)
            {
                throw new ArgumentException("Mask length is greater than string length");
            }

            if (start + maskLength > source.Length)
            {
                throw new ArgumentException("Start position and mask length imply more characters than are present");
            }

            string mask = new string(maskCharacter, maskLength);
            string unMaskStart = source.Substring(0, start);
            string unMaskEnd = source.Substring(start + maskLength, source.Length - maskLength);

            return unMaskStart + mask + unMaskEnd;
        }
    }


}
