using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SalesManager.AppCode
{
    public static class SalesManagerExtensions
    {
        /// <summary>
        /// Kiểm tra List có giá trị hay không
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool HasValue<T>(this List<T> items)
        {
            if (items != null)
            {
                if (items.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra IEnumerable có dữ liệu không
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        /// <summary>
        /// Trả về string với giá trị mặc định nếu không có giá trị
        /// </summary>
        /// <param name="str">value</param>
        /// <param name="strDefault">giá trị mặc định</param>
        /// <returns></returns>
        public static string TrimmedOrDefault(this string str, string strDefault)
        {
            return string.IsNullOrEmpty(str) ? strDefault : str.Trim();
        }

        /// <summary>
        /// DateTime to string format
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="textEmpty"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string toString(this DateTime dt, string textEmpty = "", string format = "dd/MM/yyyy")
        {
            return dt == DateTime.MinValue ? textEmpty : dt.ToString(format);
        }

        public static string CurrencyToString(this double currency, string textEmpty = "", string format = "#,###")
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return currency.ToString(format, cul.NumberFormat);
        }

        public static List<Actions> ListActionsByParentId(this List<Actions> list, short parentActionId)
        {
            List<Actions> retVal = new List<Actions>();
            foreach (Actions action in list)
            {
                if (action.ParentActionId == parentActionId && action.Display > 0)
                {
                    retVal.Add(action);
                }
            }
            return retVal;
        }

        public static string GetLinkPage(int page = 1)
        {
            string rawUrl = HttpContext.Current.Request.RawUrl;
            if (string.IsNullOrEmpty(rawUrl))
            {
                return rawUrl;
            }
            rawUrl = Regex.Replace(rawUrl, @"[?|&]page=[0-9]+", string.Empty);
            return rawUrl.Contains("?") ? rawUrl + "&page=" + page : rawUrl + "?page=" + page;
        }

        public static IEnumerable<SelectListItem> AddDefaultOption(this IEnumerable<SelectListItem> list, string dataTextField, string selectedValue)
        {
            var items = new List<SelectListItem> { new SelectListItem() { Text = dataTextField, Value = selectedValue } };
            items.AddRange(list);
            return items;
        }

        public static void Put<T>(this TempDataDictionary tempData, T value) where T : class
        {
            tempData[typeof(T).FullName] = value;
        }
        public static void Put<T>(this TempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[typeof(T).FullName + key] = value;
        }
        public static T Get<T>(this TempDataDictionary tempData) where T : class
        {
            object o;
            tempData.TryGetValue(typeof(T).FullName, out o);
            return o == null ? null : (T)o;
        }
        public static T Get<T>(this TempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(typeof(T).FullName + key, out o);
            return o == null ? null : (T)o;
        }
    }
}