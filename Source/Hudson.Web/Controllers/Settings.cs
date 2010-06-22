using System;
using System.Web;

namespace Hudson.Web.Controllers
{
    public static class Settings
    {
        private const string CookieName = "hudson";

        private static void SetValue(string name, string value)
        {
            var context = HttpContext.Current;

            if (context != null)
            {
                var response = context.Response;

                var cookie = response.Cookies.Get(CookieName);

                if (cookie == null) cookie = new HttpCookie(CookieName);

                cookie[name] = value;
                cookie.Expires = DateTime.MaxValue;

                response.Cookies.Add(cookie);
            }
        }

        private static string GetValue(string name)
        {
            var context = HttpContext.Current;

            if (context != null)
            {
                var response = context.Request;

                return response.Cookies[CookieName][name] ?? string.Empty;
            }

            return string.Empty;
        }

        public static string Server
        {
            get
            {
                return GetValue("Server");
            }
            set
            {
                SetValue("Server", value);                
            }
        }

        public static string Username
        {
            get
            {
                return GetValue("Username");
            }
            set
            {
                SetValue("Username", value);
            }
        }

        public static string Password
        {
            get
            {
                return GetValue("Password");
            }
            set
            {
                SetValue("Password", value);
            }
        }

        public static string JobName
        {
            get
            {
                return GetValue("JobName");
            }
            set
            {
                SetValue("JobName", value);
            }
        }
    }
}