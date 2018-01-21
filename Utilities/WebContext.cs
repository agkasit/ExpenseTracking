using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    public class WebContext
    {

        /// <summary>
        /// Convert NameValueCollection to querystring
        /// </summary>
        /// <param name="nvc"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string toQueryString(NameValueCollection nvc, out Exception ex)
        {
            try
            {
                string[] NameValue = new string[nvc.Count];
                string Message = string.Empty;
                for (int i = 0; i < nvc.Count; i++)
                {
                    NameValue[i] = string.Concat(nvc.GetKey(i), "=", nvc[i]);
                }
                Message = string.Join("&", NameValue);
                ex = null;

                return Message;
            }
            catch (Exception exec)
            {
                ex = exec;
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return current user ip address</returns>
        public static string GetUserIPAddress()
        {
            return HttpContext.Current.Request.ServerVariables["remote_addr"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return current user browser agent</returns>
        public static string GetUserAgent()
        {
            return HttpContext.Current.Request.ServerVariables["http_user_agent"];
        }

        /// <summary>
        /// Go to ResultStatus.aspx
        /// </summary>
        /// <param name="message"></param>
        /// <param name="returnUrl"></param>
        /// <param name="returnDesc"></param>
        /// <param name="errorFlag"></param>
        public static void GotoResultStatusPage(string message, string returnUrl, string returnDesc, int errorFlag)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("~/ResultStatus.aspx?");
            sb.AppendFormat("message={0}", message);
            sb.Append("&").AppendFormat("returnUrl={0}", string.IsNullOrEmpty(returnUrl) ? "" : returnUrl);
            sb.Append("&").AppendFormat("returnDesc={0}", string.IsNullOrEmpty(returnDesc) ? "" : returnDesc);
            sb.Append("&").AppendFormat("error={0}", errorFlag);
            HttpContext.Current.Response.Redirect(sb.ToString(), false);
        }

        /// <summary>
        /// Go to Admin/ResultStatus.aspx
        /// </summary>
        /// <param name="message"></param>
        /// <param name="returnUrl"></param>
        /// <param name="returnDesc"></param>
        /// <param name="errorFlag"></param>
        public static void GotoAdminResultStatusPage(string message, string returnUrl, string returnDesc, int errorFlag)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("~/ResultStatus.aspx?");
            sb.AppendFormat("message={0}", message);
            sb.Append("&").AppendFormat("returnUrl={0}", string.IsNullOrEmpty(returnUrl) ? "" : returnUrl);
            sb.Append("&").AppendFormat("returnDesc={0}", string.IsNullOrEmpty(returnDesc) ? "" : returnDesc);
            sb.Append("&").AppendFormat("error={0}", errorFlag);
            HttpContext.Current.Response.Redirect(sb.ToString(), false);
        }

        /// <summary>
        /// Get AppSetting by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (value == null || value.CompareTo("") == 0)
            {
                throw new Exception(string.Format("Unknow AppSetting key {0}", key));
            }
            return value;
        }

        /// <summary>
        /// Get Session value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        /// <summary>
        /// Remove Session value by key
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveSession(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        /// <summary>
        /// Set Session value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetSession(string key, object value)
        {
            if (HttpContext.Current.Session[key] != null)
                HttpContext.Current.Session.Remove(key);
            HttpContext.Current.Session[key] = value;
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Abandon();
            //GotoLoginPage();
        }
        /// <summary>
        /// Go to specific page
        /// </summary>
        /// <param name="page"></param>
        public static void GotoPage(string page)
        {
            HttpContext.Current.Response.Redirect(page, false);
        }
        /// <summary>
        /// Go to login page
        /// </summary>
        public static void GotoLoginPage()
        {
            HttpContext.Current.Response.Redirect("/Account/Login", false);
        }
        /// <summary>
        /// Go to home page
        /// </summary>
        public static void GotoHome()
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx", false);
        }
        /// <summary>
        /// Go to logout page
        /// </summary>
        public static void GotoLogoutPage()
        {
            HttpContext.Current.Response.Redirect("~/logout.aspx", false);
        }

        /// <summary>
        /// Get query string by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>value</returns>
        public static string GetQueryString(string key)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[key])) return null;
            return HttpContext.Current.Request.QueryString[key];
        }
    }
}
