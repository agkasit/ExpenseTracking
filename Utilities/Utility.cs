using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Xml;
using System.Net;
using System.Runtime.CompilerServices;
using RestSharp;
using RestSharp.Authenticators;
//using RestSharp;
//using RestSharp.Authenticators;

namespace Digix.Utilites
{
    public enum RandomType
    {
        STRING,
        NUMBER
    }
    public enum TimeFormatType
    {
        HH_MM_SS,
        HHMMSS
    }

    public struct ErrorAPICode
    {
        public const string Parameter = "001";
        public const string Unique = "002";
        public const string Other = "003";
    }


    public class Utility
    {
        #region Mailgun
        public static RestClient MailGunSetClient()
        {
            RestClient client = new RestClient();
            Uri myUri = new Uri(ConfigurationManager.AppSettings["MAILGUN_API_BASE_URL"].ToString(), UriKind.Absolute);
            client.BaseUrl = myUri;
            client.Encoding = UTF8Encoding.UTF8;
            client.Authenticator = new HttpBasicAuthenticator("api",
                             ConfigurationManager.AppSettings["MAILGUN_API_KEY"].ToString()
                             );

            return client;
        }
        public static RestRequest MailGunSetRequest()
        {
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                ConfigurationManager.AppSettings["MAILGUN_DOMAIN"].ToString() // mail.tacteams.com
                                , ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from",
                ConfigurationManager.AppSettings["MAILGUN_SMTP_NAME"].ToString() + " " +   // Tacteams
                ConfigurationManager.AppSettings["MAILGUN_SMTP_LOGIN"].ToString());  // postmaster@mail.tacteams.com
            request.Method = Method.POST;

            return request;
        }

        #endregion
        public static void SendMailMessage(MailMessage msg)
        {
            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            try
            {

                string Username = ConfigurationManager.AppSettings["mail_server"];
                string Password = ConfigurationManager.AppSettings["mail_password"];

                SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587);
                mailer.Credentials = new NetworkCredential(Username, Password);
                mailer.EnableSsl = true;
                mailer.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailer.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public enum E_APP
        {
            GREAT_WALL = 1,
            DIGI_SMS = 2,
            //DIGI_MAG = 3
        }

        public enum DATE_FORMAT
        {
            DD_MM_YYYY = 0,// 25/10/2009
            DDMMYYYY = 1,// 25102009
            YYYY_MM_DD = 2,// 2009/10/29
            YYYYMMDD = 3 // 20091225
        }

        public enum HEIGHTLIGH
        {
            SUCCESS,
            ERROR,
            HIGHLIGHT
        }

        public static string GetCurrentYear()
        {
            return DateTime.Now.Year.ToString();
        }

        public static DateTime GetDateTimeNow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            DateTime myUtcDateTime = DateTime.UtcNow;
            DateTime myConvertedDateTime = TimeZoneInfo.ConvertTime(myUtcDateTime, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            return myConvertedDateTime;
        }

        /// <summary>
        /// Get current datetime stamp
        /// </summary>
        /// <returns>YYYYMMDDHHMMSSssss</returns>
        public static string GetTimeStamp()
        {
            return GetYYYYMMDD() + GetHHMMSS() + DateTime.Now.Millisecond.ToString();
        }

        /// <summary>
        /// timestamp_filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetTimeStampAppendFileName(string filename)
        {
            return GetTimeStamp() + "_" + filename;
        }

        public static string GetTimeFormat(string source, TimeFormatType OldFormat, TimeFormatType NewFormat)
        {
            try
            {
                if (string.IsNullOrEmpty(source))
                    return "-";
                if (source.Length != 6)
                    return "-";

                switch (OldFormat)
                {
                    case TimeFormatType.HH_MM_SS:
                        switch (NewFormat)
                        {
                            case TimeFormatType.HH_MM_SS:
                                return source;
                            case TimeFormatType.HHMMSS:
                                return source.Split(':').ToString();
                        }
                        break;
                    case TimeFormatType.HHMMSS:
                        switch (NewFormat)
                        {
                            case TimeFormatType.HH_MM_SS:
                                string h = source.Substring(0, 2);
                                string m = source.Substring(2, 2);
                                string s = source.Substring(4, 2);
                                return h + ":" + m + ":" + s;
                            case TimeFormatType.HHMMSS:
                                return source;
                        }
                        break;
                    default:
                        return source;
                }
                return source;
            }
            catch (Exception)
            {
                return source;
            }
        }

        /// <summary>
        /// Get random string or number
        /// </summary>
        /// <param name="length"></param>
        /// <param name="randomType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GetRandom(int length, RandomType randomType)
        {
            string sourceStr = "abcdefghijklmnopqrstuvwxyz0123456789";
            string sourceNum = "0123456789";
            Random random = new Random();
            string output = "";
            switch (randomType)
            {
                case RandomType.STRING:
                    for (int i = 0; i < length; i++)
                    {
                        int index = random.Next(sourceStr.Length);
                        output += sourceStr.Substring(index, 1);
                    }
                    break;
                case RandomType.NUMBER:
                    for (int i = 0; i < length; i++)
                    {
                        int index = random.Next(sourceNum.Length);
                        output += sourceNum.Substring(index, 1);
                    }
                    break;
                default:
                    break;
            }
            return output;
        }

        /// <summary>
        /// Get random string or number
        /// </summary>
        /// <param name="length"></param>
        /// <param name="randomType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GetRandomLoop(int length, RandomType randomType, Random random)
        {
            string sourceStr = "abcdefghijklmnopqrstuvwxyz0123456789";
            string sourceNum = "0123456789";
            string output = "";
            switch (randomType)
            {
                case RandomType.STRING:
                    for (int i = 0; i < length; i++)
                    {
                        int index = random.Next(sourceStr.Length);
                        output += sourceStr.Substring(index, 1);
                    }
                    break;
                case RandomType.NUMBER:
                    for (int i = 0; i < length; i++)
                    {
                        int index = random.Next(sourceNum.Length);
                        output += sourceNum.Substring(index, 1);
                    }
                    break;
                default:
                    break;
            }
            return output;
        }

        public static string GetYYYYMMDD()
        {
            string returnDate = "";
            try
            {
                DateTime currentDate = DateTime.Now;
                returnDate = currentDate.Year.ToString() + currentDate.Month.ToString("00") + currentDate.Day.ToString("00");

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return returnDate;
        }

        public static string GetLastMonthStartDay(int iMonth, bool format)
        {
            string returnDate = "";
            try
            {
                DateTime currentDate = DateTime.Now.AddMonths(iMonth);
                currentDate = GetFirstDayOfMonth(currentDate);
                if (format)
                {
                    returnDate = currentDate.Day.ToString("00") + "/" + currentDate.Month.ToString("00") + "/" + currentDate.Year.ToString();
                }
                else
                {
                    returnDate = currentDate.Year.ToString() + currentDate.Month.ToString("00") + currentDate.Day.ToString("00");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return returnDate;
        }
        public static string GetLastMonthEndDay(int iMonth, bool format)
        {
            string returnDate = "";
            try
            {
                DateTime currentDate = DateTime.Now.AddMonths(iMonth);
                currentDate = GetLastDayOfMonth(currentDate);
                if (format)
                {
                    returnDate = currentDate.Day.ToString("00") + "/" + currentDate.Month.ToString("00") + "/" + currentDate.Year.ToString();
                }
                else
                {
                    returnDate = currentDate.Year.ToString() + currentDate.Month.ToString("00") + currentDate.Day.ToString("00");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return returnDate;
        }

        public static DateTime GetFirstDayOfMonth(DateTime dtDate)
        {
            // set return value to the first day of the month
            // for any date passed in to the method

            // create a datetime variable set to the passed in date
            DateTime dtFrom = dtDate;

            // remove all of the days in the month
            // except the first day and set the
            // variable to hold that date
            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            // return the first day of the month
            return dtFrom;
        }
        public static DateTime GetLastDayOfMonth(DateTime dtDate)
        {
            // set return value to the last day of the month
            // for any date passed in to the method

            // create a datetime variable set to the passed in date
            DateTime dtTo = dtDate;

            // overshoot the date by a month
            dtTo = dtTo.AddMonths(1);

            // remove all of the days in the next month
            // to get bumped down to the last day of the
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));

            // return the last day of the month
            return dtTo;
        }

        public static string GetHHMMSS()
        {
            string returnTime = "";
            try
            {
                DateTime currentDate = DateTime.Now;
                returnTime = currentDate.Hour.ToString("00") + currentDate.Minute.ToString("00") + currentDate.Second.ToString("00");

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return returnTime;
        }

        public static string GenHTMLHeightlight(string title, string message, HEIGHTLIGH TYPE)
        {
            StringBuilder sb = new StringBuilder();
            switch (TYPE)
            {
                case HEIGHTLIGH.SUCCESS:
                    sb.Append("<div class=\"ui-widget\">");
                    sb.Append("<div class=\"ui-state-highlight ui-corner-all\" style=\"margin-top: 20px; padding: 0 .7em;\">");
                    sb.Append("<p><span class=\"ui-icon ui-icon-info\" style=\"float: left; margin-right: .3em;\"></span>");
                    sb.Append("<strong>").Append(message).Append("</strong>");
                    sb.Append("</p>").Append("</div>").Append("</div>");
                    break;
                case HEIGHTLIGH.ERROR:
                    sb.Append("<div class=\"ui-widget\">");
                    sb.Append("<div class=\"ui-state-error ui-corner-all\" style=\"padding: 0 .7em;\">");
                    sb.Append("<p><span class=\"ui-icon ui-icon-alert\" style=\"float: left; margin-right: .3em;\"></span>");
                    sb.Append("<strong>").Append(message).Append("</strong></p>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    break;
                case HEIGHTLIGH.HIGHLIGHT:
                    sb.Append("<div style=\"margin-top: 20px; padding: 0pt 0.7em;\" class=\"ui-state-highlight ui-corner-all\">");
                    sb.Append("<p><span style=\"float: left; margin-right: 0.3em;\" class=\"ui-icon ui-icon-info\"></span>");
                    sb.Append("<strong>").Append(title).Append("</strong>");
                    sb.Append(message).Append("</p>");
                    sb.Append("</div>");
                    break;
                default:
                    break;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get new format date given datetime and new format
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetFormatDateTime(DateTime source, DATE_FORMAT format)
        {
            string year = source.Year.ToString();
            string month = source.Month.ToString("00");
            string day = source.Day.ToString("00");

            switch (format)
            {
                case DATE_FORMAT.DD_MM_YYYY:
                    return day + "/" + month + "/" + year;
                case DATE_FORMAT.DDMMYYYY:
                    return day + month + year;
                case DATE_FORMAT.YYYY_MM_DD:
                    return year + "/" + month + "/" + day;
                case DATE_FORMAT.YYYYMMDD:
                    return year + month + day;
                default:
                    return source.ToShortDateString();
            }
        }


        /// <summary>
        /// Format Date specifig sourcedate,oldformat,newformat
        /// </summary>
        /// <param name="source"></param>
        /// <param name="newFormat"></param>
        /// <returns>newformat</returns>
        public static string FormatDate(string source, DATE_FORMAT oldFormat, DATE_FORMAT newFormat)
        {
            try
            {
                if (string.IsNullOrEmpty(source))
                {
                    return "";
                }
                string dateTmp = "";
                string day = "";
                string month = "";
                string year = "";

                switch (oldFormat)
                {
                    case DATE_FORMAT.DD_MM_YYYY:
                        dateTmp = RemoveIllegalCharacters(source);
                        day = dateTmp.Substring(0, 2);
                        month = dateTmp.Substring(2, 2);
                        year = dateTmp.Substring(4, 4);
                        return GetNewFormat(year + month + day, newFormat);
                    case DATE_FORMAT.DDMMYYYY:
                        day = source.Substring(0, 2);
                        month = source.Substring(2, 2);
                        year = source.Substring(4);
                        return GetNewFormat(year + month + day, newFormat);
                    case DATE_FORMAT.YYYY_MM_DD:
                        dateTmp = RemoveIllegalCharacters(source);
                        return GetNewFormat(dateTmp, newFormat);
                    case DATE_FORMAT.YYYYMMDD:
                        return GetNewFormat(source, newFormat);
                    default:
                        return "";
                }
            }
            catch (Exception)
            {

                return source;
            }
        }

        private static string GetNewFormat(string p, DATE_FORMAT newFormat)
        {
            try
            {
                if (string.IsNullOrEmpty(p))
                    return "";
                if (p.Length != 8)
                    return "";

                string year = p.Substring(0, 4);
                string month = p.Substring(4, 2);
                string day = p.Substring(6, 2);
                switch (newFormat)
                {
                    case DATE_FORMAT.DD_MM_YYYY:
                        return day + "/" + month + "/" + year;
                    case DATE_FORMAT.DDMMYYYY:
                        return day + month + year;
                    case DATE_FORMAT.YYYY_MM_DD:
                        return year + "/" + month + "/" + day;
                    case DATE_FORMAT.YYYYMMDD:
                        return p;
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DateTime GetDate(string yyymmdd)
        {
            try
            {
                if (yyymmdd.Length > 8 || yyymmdd.Length < 8)
                    return DateTime.Now;

                string year = yyymmdd.Substring(0, 4);
                string month = yyymmdd.Substring(4, 2);
                string day = yyymmdd.Substring(6, 2);
                DateTime dated = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day), new GregorianCalendar());
                return dated;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Strips all illegal characters from the specified title.
        /// </summary>
        public static string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = text.Replace(" ", "-");
            text = RemoveDiacritics(text);
            text = RemoveExtraHyphen(text);

            return HttpUtility.UrlEncode(text).Replace("%", string.Empty);
        }

        private static string RemoveExtraHyphen(string text)
        {
            if (text.Contains("--"))
            {
                text = text.Replace("--", "-");
                return RemoveExtraHyphen(text);
            }

            return text;
        }

        private static String RemoveDiacritics(string text)
        {
            String normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                Char c = normalized[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString();
        }

        private static readonly Regex STRIP_HTML = new Regex("<[^>]*>", RegexOptions.Compiled);
        /// <summary>
        /// Strips all HTML tags from the specified string.
        /// </summary>
        /// <param name="html">The string containing HTML</param>
        /// <returns>A string without HTML tags</returns>
        public static string StripHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return STRIP_HTML.Replace(html, string.Empty);
        }

        private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);

        /// <summary>
        /// Removes the HTML whitespace.
        /// </summary>
        /// <param name="html">The HTML.</param>
        public static string RemoveHtmlWhitespace(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            html = REGEX_BETWEEN_TAGS.Replace(html, "> ");
            html = REGEX_LINE_BREAKS.Replace(html, string.Empty);

            return html.Trim();
        }

        /// Retrieves the subdomain from the specified URL.
        /// </summary>
        /// <param name="url">The URL from which to retrieve the subdomain.</param>
        /// <returns>The subdomain if it exist, otherwise null.</returns>
        public static string GetSubDomain(Uri url)
        {
            if (url.HostNameType == UriHostNameType.Dns)
            {
                string host = url.Host;
                if (host.Split('.').Length > 2)
                {
                    int lastIndex = host.LastIndexOf(".");
                    int index = host.LastIndexOf(".", lastIndex - 1);
                    return host.Substring(0, index);
                }
            }

            return null;
        }


        private static string mobileDevices = @"(nokia|sonyericsson|blackberry|samsung|sec\-|windows ce|motorola|mot\-|up.b|midp\-)";

        private static readonly Regex MOBILE_REGEX = new Regex(mobileDevices, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Gets a value indicating whether the client is a mobile device.
        /// </summary>
        /// <value><c>true</c> if this instance is mobile; otherwise, <c>false</c>.</value>
        public static bool IsMobile
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    HttpRequest request = context.Request;
                    if (request.Browser.IsMobileDevice)
                        return true;

                    if (!string.IsNullOrEmpty(request.UserAgent) && MOBILE_REGEX.IsMatch(request.UserAgent))
                        return true;
                }

                return false;
            }
        }


        #region Send e-mail

        /// <summary>
        /// Sends a MailMessage object using the SMTP settings.
        /// </summary>
        /// 
        //public static void SendMailMessage(MailMessage msg)
        //{
        //    //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        //    try
        //    {

        //        string Username = ConfigurationManager.AppSettings["mail_server"];
        //        string Password = ConfigurationManager.AppSettings["mail_password"];

        //        SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587);
        //        mailer.Credentials = new NetworkCredential(Username, Password);
        //        mailer.EnableSsl = true;
        //        mailer.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        mailer.Send(msg);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //public static void SendMailMessage(MailMessage message)
        //{
        //    if (message == null)
        //        throw new ArgumentNullException("message");

        //    try
        //    {
        //        message.IsBodyHtml = true;
        //        message.BodyEncoding = Encoding.UTF8;
        //        SmtpClient smtp = new SmtpClient("SmtpServer");
        //        smtp.Credentials = new System.Net.NetworkCredential("SmtpUserName", "SmtpPassword");
        //        smtp.Port = 25;
        //        smtp.EnableSsl = false;
        //        smtp.Send(message);
        //        OnEmailSent(message);
        //    }
        //    catch (SmtpException)
        //    {
        //        OnEmailFailed(message);
        //    }
        //    finally
        //    {
        //        // Remove the pointer to the message object so the GC can close the thread.
        //        message.Dispose();
        //        message = null;
        //    }
        //}

        /// <summary>
        /// Sends the mail message asynchronously in another thread.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public static void SendMailMessageAsync(MailMessage message)
        {
            ThreadPool.QueueUserWorkItem(delegate { Utility.SendMailMessage(message); });
        }

        /// <summary>
        /// Occurs after an e-mail has been sent. The sender is the MailMessage object.
        /// </summary>
        public static event EventHandler<EventArgs> EmailSent;
        private static void OnEmailSent(MailMessage message)
        {
            if (EmailSent != null)
            {
                EmailSent(message, new EventArgs());
            }
        }

        /// <summary>
        /// Occurs after an e-mail has been sent. The sender is the MailMessage object.
        /// </summary>
        public static event EventHandler<EventArgs> EmailFailed;
        private static void OnEmailFailed(MailMessage message)
        {
            if (EmailFailed != null)
            {
                EmailFailed(message, new EventArgs());
            }
        }

        #endregion

        #region Semantic discovery

        /// <summary>
        /// Finds the semantic documents from a URL based on the type.
        /// </summary>
        /// <param name="url">The URL of the semantic document or a document containing semantic links.</param>
        /// <param name="type">The type. Could be "foaf", "apml" or "sioc".</param>
        /// <returns>A dictionary of the semantic documents. The dictionary is empty if no documents were found.</returns>
        public static Dictionary<Uri, XmlDocument> FindSemanticDocuments(Uri url, string type)
        {
            Dictionary<Uri, XmlDocument> list = new Dictionary<Uri, XmlDocument>();

            string content = DownloadWebPage(url);
            if (!string.IsNullOrEmpty(content))
            {
                string upper = content.ToUpperInvariant();

                if (upper.Contains("</HEAD") && upper.Contains("</HTML"))
                {
                    List<Uri> urls = FindLinks(type, content);
                    foreach (Uri xmlUrl in urls)
                    {
                        XmlDocument doc = LoadDocument(url, xmlUrl);
                        if (doc != null)
                            list.Add(xmlUrl, doc);
                    }
                }
                else
                {
                    XmlDocument doc = LoadDocument(url, url);
                    if (doc != null)
                        list.Add(url, doc);
                }
            }

            return list;
        }

        /// <summary>
        /// Downloads a web page from the Internet and returns a string. .
        /// </summary>
        /// <param name="url">The URL to download from.</param>
        /// <returns>The HTML or null if the URL isn't valid.</returns>
        public static string DownloadWebPage(Uri url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.UseDefaultCredentials = true;
                    client.Headers.Add(System.Net.HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1;)");
                    using (StreamReader reader = new StreamReader(client.OpenRead(url)))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException)
            {
                return null;
            }
        }


        private static XmlDocument LoadDocument(Uri url, Uri xmlUrl)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                if (url.IsAbsoluteUri)
                {
                    doc.Load(xmlUrl.ToString());
                }
                else
                {
                    string absoluteUrl = null;
                    if (!url.ToString().StartsWith("/"))
                        absoluteUrl = (url + xmlUrl.ToString());
                    else
                        absoluteUrl = url.Scheme + "://" + url.Authority + xmlUrl;

                    doc.Load(absoluteUrl);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return doc;
        }

        private const string PATTERN = "<head.*<link( [^>]*title=\"{0}\"[^>]*)>.*</head>";
        private static readonly Regex HREF = new Regex("href=\"(.*)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Finds semantic links in a given HTML document.
        /// </summary>
        /// <param name="type">The type of link. Could be foaf, apml or sioc.</param>
        /// <param name="html">The HTML to look through.</param>
        /// <returns></returns>
        public static List<Uri> FindLinks(string type, string html)
        {
            MatchCollection matches = Regex.Matches(html, string.Format(PATTERN, type), RegexOptions.IgnoreCase | RegexOptions.Singleline);
            List<Uri> urls = new List<Uri>();

            foreach (Match match in matches)
            {
                if (match.Groups.Count == 2)
                {
                    string link = match.Groups[1].Value;
                    Match hrefMatch = HREF.Match(link);

                    if (hrefMatch.Groups.Count == 2)
                    {
                        Uri url;
                        string value = hrefMatch.Groups[1].Value;
                        if (Uri.TryCreate(value, UriKind.Absolute, out url))
                        {
                            urls.Add(url);
                        }
                    }
                }
            }

            return urls;
        }

        #endregion

        /// <summary>
        /// Encrypts a string using the SHA256 algorithm.
        /// </summary>
        public static string HashPassword(string plainMessage)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainMessage);
            using (HashAlgorithm sha = new SHA256Managed())
            {
                byte[] encryptedBytes = sha.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(sha.Hash);
            }
        }




        /// <summary>
        /// ใช้ Char.IsDigit(x) เช็คทีละตัว
        /// </summary>
        /// <param name="numberic"></param>
        /// <returns></returns>
        public static bool IsNumberic(string numberic)
        {
            try
            {
                char[] chars = numberic.ToCharArray();
                foreach (char c in chars)
                {
                    if (Char.IsDigit(c) == false)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw new Exception(numberic+" ไม่ใช่ตัวเลข");
            }
        }

        /// <summary>
        /// ใช้ Cast to Integer เพื่อความเร็ว
        /// </summary>
        /// <param name="numberic"></param>
        /// <returns></returns>
        public static bool IsNumberic2(string numberic)
        {
            try
            {
                Int32 num = Int32.Parse(numberic);
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw new Exception(numberic + " ไม่ใช่ตัวเลข");
            }
        }
        public static string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
        public static int GetHightByWidth(int oldWidth, int oldHeight, int newWidth)
        {
            return oldHeight * newWidth / oldWidth;
        }
        public static int GetWidthByHight(int oldWidth, int oldHeight, int newHeight)
        {
            return oldWidth * newHeight / oldHeight; //w=100,h=139
        }

        public static string Concat(string str, int w)
        {
            string ext = str.Substring(str.Length - 4, 4);
            return str.Replace(ext, "_" + w + ext);

        }

        public static double StringTryParseDouble(object str)
        {
            double dnum = 0;
            if (!string.IsNullOrEmpty(str.ToString()))
                double.TryParse(str.ToString(), out dnum);
            return dnum;
        }
        public static decimal StringTryParseDecimal(object str)
        {
            decimal dnum = 0.00M;
            if (str != null)
                decimal.TryParse(str.ToString(), out dnum);
            return dnum;
        }
        public static long StringTryParseLong(object str)
        {
            long dnum = 0;
            if (!string.IsNullOrEmpty(str.ToString()))
                Int64.TryParse(str.ToString(), out dnum);
            return dnum;
        }
        public static int StringTryParseInt(object str)
        {
            int dnum = 0;
            try
            {
                if (!string.IsNullOrEmpty(str.ToString()))
                    Int32.TryParse(str.ToString(), out dnum);
            }
            catch { }
            return dnum;
        }
    }
}
