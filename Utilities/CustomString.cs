using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Digix.Utilities
{
    public class CustomString
    {
        public string deTecNewline(string str)
        {
            string[] arr = Regex.Split(str, "\n");
            return deTecSpacbar(arr[1].ToString());
        }

        public string deTecSpacbar(string str)
        {
            string result = "";
            char[] arr = str.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != ' ')
                {
                    result += arr[i];
                }
            }
            return result;
        }

        public static string txtReplce(string txt, string strOld, string strNew) {
            try {
                string ret = txt.Replace(strOld, strNew);
                return ret;
            }
            catch (Exception ex) {
                return "-";
            }
        }

        public static string txtReplceImport(string txt, string strOld, string strNew)
        {
            try
            {
                string ret = txt.Replace(strOld, strNew);
                return ret;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string TelOrtherFormat(string tel,string telOrther) {
            //string result = "";
            try
            {
                //if (tel == string.Empty || tel == "")
                //{
                //    result = "";
                //}
                //else {
                //    result = tel + ",";
                //}

                //if (telOrther != string.Empty || telOrther != "") {
                //    result += telOrther;
                //}

                //return result; 

                //if (tel == string.Empty || tel == "")
                //{
                //    result = "";
                //}
                //else
                //{
                //    result = tel + ",";
                //}

                if (telOrther != string.Empty || telOrther != "")
                {
                    return  tel + "," +telOrther;
                }
                else {
                    if (tel == string.Empty || tel == "")
                    {
                        return "";
                    }
                    else
                    {
                        return tel;
                    }
                    
                }

            }
            catch (Exception ex) {
                return "";
            }
            
        }

        public static string CallCustom(string txtTel) {
            try
            {
                string txt = txtReplce(txtTel, "-", "");
                txt = txtReplce(txt, " ", "");
                txt = txtReplce(txt, "  ", "");
                txt = txtReplce(txt, "   ", "");

                if (txt.Length > 2)
                {
                    try
                    {

                        string pre = txt.Substring(0, 2);
                        if (pre == "06" || pre == "08" || pre == "09")
                        {
                            if (txt.Length >= 10)
                            {
                                return txt.Substring(0, 10);
                            }
                            else
                            {
                                return txt;
                            }

                        }
                        else
                        {
                            //result = txt.Substring(0, 9);
                            if (txt.Length >= 9)
                            {
                                return txt.Substring(0, 9);
                            }
                            else
                            {
                                return txt;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return "";
                    }

                }
                else
                {
                    return "";
                }
            }
            catch (Exception e) {
                return "";
            }
            
            
        }

        public static string txtTelFormat(string txtTel)
        {
            string result = string.Empty;

            if (txtTel.Equals(string.Empty) || txtTel == null || txtTel == "-")
            {
                result = "";
            }
            else { 
                result = txtTel;
            }

            return result;
        }

        public static string PreDomainName(string DomainName)
        {
            try {

                char[] arr = DomainName.ToArray();
                string str = string.Empty;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i == 0)
                    {
                        str += arr[i].ToString().ToUpper();
                    }
                    else {
                        str += arr[i];
                    }
                    
                }
                return str;

            }
            catch (Exception ex) {

                return DomainName;
            }
            
        }

        public static string MainAddressGetUser(string email) {
            try
            {
                MailAddress addr = new MailAddress(email);
                string username = addr.User;
                string domain = addr.Host;
                return username;
            }
            catch (Exception ex) {

                return "username";
            
            }
            
        }
        public static bool CheckStringFirstNumber(string DomainName)
        {
            char[] arr = DomainName.ToArray();
            int n;
            bool isNumeric = int.TryParse(arr[0].ToString(), out n);

            if (isNumeric)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsEnglish(string inputstring)
        {
            Regex regex = new Regex(@"[a-zA-Z0-9_]");
            MatchCollection matches = regex.Matches(inputstring);

            if (matches.Count.Equals(inputstring.Length))
                return true;
            else
                return false;
        }

        public static string GenderTH(string gender) {
            try
            {
                if (gender.ToLower() == "m")
                {
                    return "ชาย";
                }
                else if (gender.ToLower() == "f")
                {
                    return "หญิง";
                }
                else {
                    return "ไม่ระบุ";
                }

            }
            catch (Exception e) {
                return "ไม่ระบุ";
            }
        }
        public string DataCurrent()
        {
            string Month = DateTime.Now.Month.ToString();
            string Year = DateTime.Now.Year.ToString();
            string date = DateTime.Now.Day.ToString();
            return Year + twoPosition(Month) + twoPosition(date);

        }
        public string TimeCurrernt()
        {
            string hh = DateTime.Now.Hour.ToString();
            string mm = DateTime.Now.Minute.ToString();
            string ss = DateTime.Now.Second.ToString();
            return twoPosition(hh) + twoPosition(mm) + twoPosition(ss);
        }
        private string twoPosition(string strNum)
        {
            if (strNum.Length != 2)
            {
                strNum = "0" + strNum;
            }
            return strNum;
        }

       
        //public void detecWord(){
        //    List<string> list = new List<string>();
        //    list.Add("มูลนิธี");//ไม่ต้องเติม
        //    list.Add("โรงพยาบาล");//ไม่ต้องเติม
        //    list.Add("สถานพยาบาล");//ไม่ต้องเติม
        //    list.Add("สถานีอนามัย");//ไม่ต้องเติม
        //    list.Add("คลินิก");
        //}

        public static string ReplaceTextHospitalName(string strSource, string [] wordDetec,string prefix)
        {
            //string strword = ConfigurationManager.AppSettings["WORD_DETEC"].ToString();
            //string[] arr = strword.Split(',');
            Boolean result = false;
            for (int i = 0; i < wordDetec.Length; i++)
            {
                if (strSource.Contains(wordDetec[i]))
                {
                    result = true;
                    break;
                }
            }
            if (result)
            {
                return strSource;
            }
            else
            {
                return prefix + strSource;
            }
        }

        //public class ProvUpdate
        //{
        //    public int ProvOriginal { set; get; }
        //    public int ProNew { set; get; }
        //}

        public static string ProviceDigixTo1669(int provinceid, string pathfile)
        {
            try
            {
                string[] plines = System.IO.File.ReadAllLines(pathfile);
                string result = "0";
                for (int i = 1; i < plines.Length; i++)
                {
                    var l = plines[i];
                    string[] arr = l.Split(',');

                    int ProvOriginal = Convert.ToInt32(arr[0]);
                    int ProNew = Convert.ToInt32(arr[2]);
                    if (provinceid == ProNew)
                    {
                        result = ProvOriginal.ToString();
                        break;
                    }
                }

                return result;

            }
            catch (Exception ex) {
                return "0";
            }
        }


        public string RemoveHTMLTags(string HTMLCode)
        {
            return System.Text.RegularExpressions.Regex.Replace(
              HTMLCode, "<[^>]*>", "");
        }

        public string HTMLToText(string HTMLCode)
        {
            // Remove new lines since they are not visible in HTML
            HTMLCode = HTMLCode.Replace("\n", " ");

            // Remove tab spaces
            HTMLCode = HTMLCode.Replace("\t", " ");

            // Remove multiple white spaces from HTML
            HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");

            // Remove HEAD tag
            HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove any JavaScript
            HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
              , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Replace special characters like &, <, >, " etc.
            StringBuilder sbHTML = new StringBuilder(HTMLCode);
            // Note: There are many more special characters, these are just
            // most common. You can add new characters in this arrays if needed
            string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;", 
   "&gt;", "&reg;", "&copy;", "&bull;", "&trade;"};
            string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢" };
            for (int i = 0; i < OldWords.Length; i++)
            {
                sbHTML.Replace(OldWords[i], NewWords[i]);
            }

            // Check if there are line breaks (<br>) or paragraph (<p>)
            sbHTML.Replace("<br>", "\n<br>");
            sbHTML.Replace("<br ", "\n<br ");
            sbHTML.Replace("<p ", "\n<p ");

            // Finally, remove all HTML tags and return plain text
            return System.Text.RegularExpressions.Regex.Replace(
              sbHTML.ToString(), "<[^>]*>", "");
        }

        public string ErrorDynamicToString(dynamic _errors)
        {
            
            try
            {
                string _errorMessage = string.Empty;
                for (int i = 0; i < _errors.message.Count; i++)
                {
                    if (i == 0)
                    {
                        _errorMessage += _errors.message[i];
                    }
                    else
                    {
                        _errorMessage += ',' + _errors.message[i];
                    }

                }

                return _errorMessage;
            }
            catch (Exception ex)
            {
                return "Success"; //IF Not Have Error Message List
            }
        }

        #region For Tacteams Project.
        
        public static string getTeamName(string domainName) {
            //"https://dev.tacteams.com"
            string str = domainName.Substring(7);//dev.tacteams.com
            char[] arr = str.ToCharArray();
            string teamName = string.Empty;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != '.')
                {
                    teamName += arr[i];
                }
                else {
                    break;
                }
            }
            return teamName;//dev
        }
        #endregion 
    }
}
