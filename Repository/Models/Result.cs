using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Result
    {
        public Result()
        {
            //date = Digix.KWC.Helper.Utility.GetDateTimeNow();
            date = DateTime.Now;
            api_version = ConfigurationManager.AppSettings["api_version"];
            root_url = ConfigurationManager.AppSettings["root_url"];
            local_zone = new Dictionary<string, string>(){
                {"display_name", TimeZoneInfo.Local.DisplayName},
                {"daylight_name", TimeZoneInfo.Local.DaylightName},
                {"id", TimeZoneInfo.Local.Id},
                {"standard_name", TimeZoneInfo.Local.StandardName}
            };
        }
        public void SetFailInProgress(string alert, dynamic message, dynamic result)
        {
            this.status = true;
            this.message = setMessageError(message);
            this.alert = alert;
            this.result = result;
        }
        public void SetFail(string alert, dynamic message, dynamic result)
        {
            this.status = false;
            this.message = setMessageError(message);
            this.alert = alert;
            this.result = result;
        }
        public void SetSuccess(dynamic result)
        {
            this.status = true;
            this.message = "Success";
            this.alert = "000";
            this.result = result;
        }
        public void SetSuccessList(dynamic result)
        {
            this.status = true;
            this.message = setMessage(message); ;
            this.alert = "000";
            this.result = result;
        }
        public void SetSuccess()
        {
            this.status = true;
            this.message = "Success";
            this.alert = "000";
            this.result = "";
        }
        public void SetError(dynamic message)
        {
            this.status = false;
            this.message = message;
            this.alert = "999";
            this.result = "";
        }
        public DateTime date { get; set; }
        public string root_url { get; set; }
        public string api_version { get; set; }
        public Dictionary<string, string> local_zone { get; set; }
        public bool status { get; set; }
        public string alert { get; set; }
        public dynamic message { get; set; }
        public dynamic result { get; set; }
        private string setMessageError(dynamic msg)
        {
            //มี 2 กรณี
            // 1. เป็น string ธรรมดา
            // 2. เป็น list
            try
            {

                var type = msg.GetType().ToString();
                if (type == "System.String")
                {
                    return msg;
                }
                else
                {

                    string _errorMessage = string.Empty;

                    for (int i = 0; i < msg.Count; i++)
                    {
                        if (i == 0)
                        {
                            _errorMessage += msg[i];
                        }
                        else
                        {
                            _errorMessage += " \n " + msg[i];
                        }

                    }
                    return _errorMessage;
                }


            }
            catch (Exception ex)
            {
                return "Success"; //IF Not Have Error Message List
            }
        }
        private string setMessage(dynamic msg)
        {
            //มี 2 กรณี
            // 1. เป็น string ธรรมดา
            // 2. เป็น list
            try
            {

                var type = msg.GetType().ToString();
                if (type == "System.String")
                {
                    return msg;
                }
                else
                {

                    string _errorMessage = string.Empty;

                    for (int i = 0; i < msg.Count; i++)
                    {
                        if (i == 0)
                        {
                            _errorMessage += msg[i];
                        }
                        else
                        {
                            _errorMessage += " \n " + msg[i];
                        }

                    }
                    return _errorMessage;
                }


            }
            catch (Exception ex)
            {
                return "Success"; //IF Not Have Error Message List
            }
        }
        public string getMessageErrors()
        {
            //มี 2 กรณี
            // 1. เป็น string ธรรมดา
            // 2. เป็น list
            try
            {

                var type = this.message.GetType().ToString();
                if (type == "System.String")
                {
                    return this.message;
                }
                else
                {

                    string _errorMessage = string.Empty;

                    for (int i = 0; i < this.message.Count; i++)
                    {
                        if (i == 0)
                        {
                            _errorMessage += this.message[i];
                        }
                        else
                        {
                            _errorMessage += " \n " + this.message[i];
                        }

                    }
                    return _errorMessage;
                }


            }
            catch (Exception ex)
            {
                return "Success"; //IF Not Have Error Message List
            }
        }
    }

    public class ResultPagging
    {
        public ResultPagging()
        {
            //date = Digix.KWC.Helper.Utility.GetDateTimeNow();
            date = DateTime.Now;
            api_version = ConfigurationManager.AppSettings["api_version"];
            root_url = ConfigurationManager.AppSettings["root_url"];
            local_zone = new Dictionary<string, string>(){
                {"display_name", TimeZoneInfo.Local.DisplayName},
                {"daylight_name", TimeZoneInfo.Local.DaylightName},
                {"id", TimeZoneInfo.Local.Id},
                {"standard_name", TimeZoneInfo.Local.StandardName}
            };
            this.page = 1;
            this.per_page = 10;
        }
        private void totalPages(int per_page, int total_record)
        {
            int _totalPages = 0;
            if (per_page > 0)
            {
                if (total_record <= per_page)
                {
                    _totalPages = 1;
                }
                else
                {
                    _totalPages = total_record / per_page;
                    if ((total_record % per_page) > 0)
                        _totalPages += 1;
                }
            }
            else
            {
                _totalPages = 1;
            }

            this.total_pages = _totalPages;
        }
        public void SetFail(string alert, dynamic message, int per_page)
        {
            this.status = false;
            this.message = setMessageError(message); ;
            this.alert = alert;
            this.result = "";
            this.per_page = per_page;
            this.total_record = 0;
            this.total_pages = 0;

        }
        public void SetSuccess(dynamic result, int per_page, int total_record)
        {
            this.status = true;
            this.message = "Success";
            this.alert = "000";
            this.result = result;
            this.per_page = per_page;
            this.total_record = total_record;

            totalPages(per_page, total_record);
        }
        public void SetSuccess(dynamic result, int per_page, int page, int total_record)
        {
            this.status = true;
            this.message = "Success";
            this.alert = "000";
            this.result = result;
            this.per_page = per_page;
            this.total_record = total_record;
            this.page = page;

            totalPages(per_page, total_record);
        }
        public void SetSuccess(int per_page, int total_record)
        {
            this.status = true;
            this.message = "Success";
            this.alert = "000";
            this.result = "";

            this.per_page = per_page;
            this.total_record = total_record;

            totalPages(per_page, total_record);
        }
        public void SetError(dynamic message, int per_page)
        {
            this.status = false;
            this.message = message;
            this.alert = "999";
            this.result = "";
            this.per_page = per_page;
            this.total_record = 0;
            this.total_pages = 0;
        }

        public DateTime date { get; set; }
        public string root_url { get; set; }
        public string api_version { get; set; }
        public Dictionary<string, string> local_zone { get; set; }
        public bool status { get; set; }
        public string alert { get; set; }
        public string message { get; set; }
        public dynamic result { get; set; }
        public int total_record { set; get; }
        public int total_pages { set; get; }
        public int page { set; get; }
        public int per_page { set; get; }

        private string setMessageError(dynamic msg)
        {
            //มี 2 กรณี
            // 1. เป็น string ธรรมดา
            // 2. เป็น list
            try
            {

                var type = msg.GetType().ToString();
                if (type == "System.String")
                {
                    return msg;
                }
                else
                {

                    string _errorMessage = string.Empty;

                    for (int i = 0; i < msg.Count; i++)
                    {
                        if (i == 0)
                        {
                            _errorMessage += msg[i];
                        }
                        else
                        {
                            _errorMessage += " \n " + msg[i];
                        }

                    }
                    return _errorMessage;
                }


            }
            catch (Exception ex)
            {
                return "Success"; //IF Not Have Error Message List
            }
        }
    }
    
}
