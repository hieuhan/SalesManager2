using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SalesManager.AppCode
{
    public static class SalesManagerConstants
    {
        public static string ROOT_PATH = ConfigurationManager.AppSettings["ROOT_PATH"] ?? string.Empty;
        public static string WEBSITE_DOMAIN = ConfigurationManager.AppSettings["WEBSITE_DOMAIN"] ?? ConfigurationManager.AppSettings["WEBSITE_DOMAIN"];
        public static string WEBSITE_IMAGEDOMAIN = ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"] ?? ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"];
        public static string WEBSITE_MEDIADOMAIN = ConfigurationManager.AppSettings["WEBSITE_MEDIADOMAIN"] ?? ConfigurationManager.AppSettings["WEBSITE_MEDIADOMAIN"];
        public static string LOG_FILE_PATH = ConfigurationManager.AppSettings["LOG_FILE_PATH"] ?? string.Concat(HttpContext.Current.Request.PhysicalApplicationPath, "\\Logs");

        public static string MEDIA_PATH = (ConfigurationManager.AppSettings["MEDIA_PATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_PATH"];
        public static string MEDIA_ORIGINAL_PATH = (ConfigurationManager.AppSettings["MEDIA_ORIGINAL_PATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_ORIGINAL_PATH"];
        public static string MEDIA_THUMNAIL_PATH = (ConfigurationManager.AppSettings["MEDIA_THUMNAIL_PATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_THUMNAIL_PATH"];
        public static string MEDIA_ICON_PATH = (ConfigurationManager.AppSettings["MEDIA_ICON_PATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_ICON_PATH"];
        public static int MEDIA_WIDTH = (ConfigurationManager.AppSettings["MEDIA_WIDTH"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_WIDTH"].ToString());
        public static int MEDIA_HEIGHT = (ConfigurationManager.AppSettings["MEDIA_HEIGHT"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_HEIGHT"].ToString());
        public static int MEDIA_THUMNAIL_WIDTH = (ConfigurationManager.AppSettings["MEDIA_THUMNAIL_WIDTH"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_THUMNAIL_WIDTH"].ToString());
        public static int MEDIA_THUMNAIL_HEIGHT = (ConfigurationManager.AppSettings["MEDIA_THUMNAIL_HEIGHT"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_THUMNAIL_HEIGHT"].ToString());
        public static int MEDIA_ICON_WIDTH = (ConfigurationManager.AppSettings["MEDIA_ICON_WIDTH"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_ICON_WIDTH"].ToString());
        public static int MEDIA_ICON_HEIGHT = (ConfigurationManager.AppSettings["MEDIA_ICON_HEIGHT"] == null) ? 0 : Int32.Parse(ConfigurationManager.AppSettings["MEDIA_ICON_HEIGHT"].ToString());

        public static readonly byte SystemMessageIdSuccess = byte.Parse(ConfigurationManager.AppSettings["SystemMessageIdSuccess"] ?? "1");
        public static readonly byte SystemMessageIdError = byte.Parse(ConfigurationManager.AppSettings["SystemMessageIdError"] ?? "2");
        public static int RowAmount20 = int.Parse(ConfigurationManager.AppSettings["RowAmount20"] ?? "20");
    }
}