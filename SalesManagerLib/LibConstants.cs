using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SalesManagerLib
{
    public class LibConstants
    {
        public static string CONNECTION_STRING = ConfigurationManager.AppSettings["CONNECTION_STRING"] ?? ConfigurationManager.AppSettings["CONNECTION_STRING"];
        public static string ROOT_PATH = ConfigurationManager.AppSettings["ROOT_PATH"] ?? ConfigurationManager.AppSettings["ROOT_PATH"];
        public static string NO_IMAGE_URL = ConfigurationManager.AppSettings["NO_IMAGE_URL"] ?? ConfigurationManager.AppSettings["NO_IMAGE_URL"];
        public static string WEBSITE_IMAGEDOMAIN = ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"] ?? ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"];
        public static string LOG_DIR = ConfigurationManager.AppSettings["LOG_DIR"] ?? ConfigurationManager.AppSettings["LOG_DIR"];
        public static string CULTURE_VN = "vi-VN";
        public static string ROLENAME_ADMIN = ConfigurationManager.AppSettings["ROLENAME_ADMIN"] ?? "";
        public static string LOG_FILE_PATH = ConfigurationManager.AppSettings["LOG_FILE_PATH"] ?? string.Concat(HttpContext.Current.Request.PhysicalApplicationPath, "\\Logs");
    }
}
