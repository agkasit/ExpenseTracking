using Digix.Utilites;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities
{
    public class DateFormate
    {
        public static string DatetimeFormate()
        {
            string Month = DateTime.Now.Month.ToString();
            string Year = DateTime.Now.Year.ToString();
            string date = DateTime.Now.Day.ToString();

            string hh = DateTime.Now.Hour.ToString();
            string mm = DateTime.Now.Minute.ToString();
            string ss = DateTime.Now.Second.ToString();


            return Year + twoPosition(Month) + twoPosition(date) + " " + twoPosition(hh) + twoPosition(mm) + twoPosition(ss);
        }

        public static DateTime DatetimeFormate(string dateStr, string timeStr)
        {
            try
            {
                char[] arr = dateStr.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];

                char[] arrt = timeStr.ToCharArray();
                string hh = "" + arrt[0] + arrt[1];
                string mm = "" + arrt[2] + arrt[3];
                string ss = "" + arrt[4] + arrt[5];

                return new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d), Convert.ToInt32(hh), Convert.ToInt32(mm), Convert.ToInt32(ss));
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }

        }

        public static string DateForSortDate(string dateStr)
        {
            try
            {
                char[] arr = dateStr.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];

                var date = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d), 0, 0, 0);
                return date.ToString("dd MMM", CultureInfo.CreateSpecificCulture("en-US")) + " " + y;
            }
            catch (Exception ex)
            {
                return "-";
            }

        }
        //
        public static string DatetimeToDatepicker(DateTime datetime)
        {
            try
            {

                string hh = datetime.Hour.ToString().Length < 2 ?       "0" + datetime.Hour.ToString()      : datetime.Hour.ToString();
                string mm = datetime.Minute.ToString().Length < 2 ?     "0" + datetime.Minute.ToString()    : datetime.Minute.ToString();
                string Month = datetime.Month.ToString().Length < 2 ?   "0" + datetime.Month.ToString()     : datetime.Month.ToString();
                string Day = datetime.Day.ToString().Length < 2 ?       "0" + datetime.Day.ToString()       : datetime.Day.ToString();
                return datetime.Year + "-" + Month + "-" + Day + " " + hh + ":" + mm;

            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static string DateToDatepicker(DateTime datetime)
        {
            try
            {
                string Month = datetime.Month.ToString().Length < 2 ? "0" + datetime.Month.ToString() : datetime.Month.ToString();
                string Day = datetime.Day.ToString().Length < 2 ? "0" + datetime.Day.ToString() : datetime.Day.ToString();
                return Day + "/" + Month + "/" + datetime.Year;

            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static DateTime convertToDatetime(string dateStr)
        {
            try
            {
                //20170129
                char[] arr = dateStr.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];

               // var current = DateTime.Parse(Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                var create_date = DateTime.Parse(new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d)).ToString(), new System.Globalization.CultureInfo("en-US"));

                return create_date;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }

        }

        public static string DatetimeFormate1669()
        {
            string Month = DateTime.Now.Month.ToString();
            string Year = DateTime.Now.Year.ToString();
            string date = DateTime.Now.Day.ToString();

            string hh = DateTime.Now.Hour.ToString();
            string mm = DateTime.Now.Minute.ToString();
            string ss = DateTime.Now.Second.ToString();

            //SET @Result = @Day+'/'+@Month+'/'+@Year
            return Year + "-" + twoPosition(Month) + "-" + twoPosition(date) + " " + twoPosition(hh) + ":" + twoPosition(mm) + ":" + twoPosition(ss);
        }


        public static string DatetimeFormate1669_SendMail()
        {
            string Month = DateTime.Now.Month.ToString();
            string Year = DateTime.Now.Year.ToString();
            string date = DateTime.Now.Day.ToString();

            int y = Convert.ToInt32(Year) + 543;

            string hh = DateTime.Now.Hour.ToString();
            string mm = DateTime.Now.Minute.ToString();
            string ss = DateTime.Now.Second.ToString();

            return twoPosition(date) + "/" + twoPosition(Month) + "/" + y.ToString() + " " + twoPosition(hh) + ":" + twoPosition(mm) + ":" + twoPosition(ss) + " น.";
        }

        public static string DateCurrent()
        {
            string Month = DateTime.Now.Month.ToString();
            string Year = DateTime.Now.Year.ToString();
            string date = DateTime.Now.Day.ToString();
            return Year + twoPosition(Month) + twoPosition(date);

        }


        public static string DateCurrentPi()
        {
            string Month = DateTime.Now.Month.ToString();
            string Year = DateTime.Now.Year.ToString();
            string date = DateTime.Now.Day.ToString();
            //return Year + twoPosition(Month) + twoPosition(date);
            return twoPosition(date) + "/" + twoPosition(Month) + "/" + Year;
        }

        public static string TimeCurrernt()
        {
            string hh = DateTime.Now.Hour.ToString();
            string mm = DateTime.Now.Minute.ToString();
            string ss = DateTime.Now.Second.ToString();
            return twoPosition(hh) + twoPosition(mm) + twoPosition(ss);
        }

        public static string DateCurrentThaiFormat()
        {
            string Month = DateTime.Now.Month.ToString();
            string Year = DateTime.Now.Year.ToString();
            string date = DateTime.Now.Day.ToString();

            try
            {

                int yyyy = Convert.ToInt32(Year) + 543;

                return (date + " " + ToMonthThai(Convert.ToInt32(Month)) + " " + yyyy);


            }
            catch (Exception e)
            {
                return "-";
            }

        }
        public static string strToDate(string str)
        {
            try
            {

                char[] arr = str.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];
                return (d + "/" + m + "/" + y);
            }
            catch (Exception e)
            {
                return "-";
            }

        }

        public static string strToDateForMoblie(string str)
        {
            try
            {
                char[] arr = str.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];
                return (y + "-" + m + "-" + d);
            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static string strToDateForMoblieInMall(string str)
        {
            try
            {
                char[] arr = str.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];
                return (d + "/" + m + "/" + y);
            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static DateTime toDateForDBInMall(string strDate)
        {
            try
            {
                //20170129
                string[] arr = strDate.Split('/');
                string d = arr[0];
                string m = arr[1];
                string y = arr[2];

                // var current = DateTime.Parse(Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                var create_date = DateTime.Parse(new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d)).ToString(), new System.Globalization.CultureInfo("en-US"));

                return create_date;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        public static string strToTimeForMoblie(string str)
        {
            try
            {
                char[] arr = str.ToCharArray();
                string h = "" + arr[0] + arr[1];
                string m = "" + arr[2] + arr[3];
                string s = "" + arr[4] + arr[5];
                return (h + ":" + m + ":" + s);
            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static string strToTimeInMall(string str)
        {
            try
            {
                char[] arr = str.ToCharArray();
                string h = "" + arr[0] + arr[1];
                string m = "" + arr[2] + arr[3];
                string s = "" + arr[4] + arr[5];
                return (h + "." + m );
            }
            catch (Exception e)
            {
                return "-";
            }
        }



        public static string strToDatePicker(string str)
        {
            if (str == "")
            {
                return null;
            }
            else
            {
                char[] arr = str.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];
                return (m + "/" + d + "/" + y);
            }
        }
        /*'2011-03-05'*/
        public static string convertDateForBootstrapDatepicker(string str)
        {
            if (str == "")
            {
                return null;
            }
            else
            {
                char[] arr = str.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];
                return (y + "-" + m + "-" + d);
            }
        }
        private static string twoPosition(string strNum)
        {
            if (strNum.Length != 2)
            {
                strNum = "0" + strNum;
            }
            return strNum;
        }
        public static string setDate(string date)
        {
            //2014-06-11
            try
            {
                string[] arr = date.Split('-');
                return arr[0] + arr[1] + arr[2];
            }
            catch (Exception e)
            {
                return string.Empty;
            }

        }

        public static string setTHtoISO(string date)
        {
            //2558-06-11
            try
            {
                string[] arr = date.Split('-');
                int k = Convert.ToInt32(arr[0]) - 543;
                return k + "" + arr[1] + arr[2];
            }
            catch (Exception e)
            {
                return string.Empty;
            }

        }

        public static string setISOToTH(string date)
        {
            //2014-06-11
            try
            {
                string[] arr = date.Split('-');
                int k = Convert.ToInt32(arr[0]) - 543;
                return arr[0] + arr[1] + arr[2];
            }
            catch (Exception e)
            {
                return string.Empty;
            }

        }


        public static string setDate_spaceBar(string date)
        {
            //08/22/1989
            try
            {
                string[] arr = date.Split('/');
                return arr[2] + arr[1] + arr[0];
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static string differentDays(string end)
        {
            if (end != "")
            {
                char[] arr = end.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];
                DateTime dtEnd = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d));
                DateTime dtStart = DateTime.Now;
                int num = DaysBetween(dtStart, dtEnd);
                if (num > 0)
                {
                    return num.ToString();
                }
                else
                {
                    return "หมดเขตแล้ว";
                }
            }
            else
            {
                return "ไม่ได้กำหนด";
            }
        }

        public static int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d2.Subtract(d1);
            return (int)span.TotalDays;
        }

        public static string toDateForDB(string strDate)
        {
            if (strDate == null)
            {
                return null;
            }
            string[] arr = strDate.Split('/');
            string d = arr[0];
            string m = arr[1];
            string y = arr[2];

            int year = Convert.ToInt32(y);
            if (year > 2500)
            {
                year = year - 543;
                y = year.ToString();
            }

            return (y + m + d);
        }

        public static string toTimeForDB(string strTime)
        {
            if (strTime == null | strTime == "")
            {
                return null;
            }
            string[] arr = strTime.Split(':');
            if (arr.Length == 3)
            {
                string h = twoPosition(arr[0]);
                string m = twoPosition(arr[1]);
                string s = twoPosition(arr[2]);
                return (h + m + s);
            }
            else
            {
                return null;
            }

        }

        public static string DatePickerToDateForDB(string strDate)
        {
            try
            {
                /*13-06-2017*/
                string[] arr = strDate.Split('-');
                string d = arr[0];
                string m = arr[1];
                string y = arr[2];
                string date = (y + m + d);
                return date;
            }
            catch (Exception ex) {
                //string Month = DateTime.Now.Month.ToString();
                //string Year = DateTime.Now.Year.ToString();
                //string date = DateTime.Now.Day.ToString();
                //return Year + twoPosition(Month) + twoPosition(date);
                return string.Empty;
            }
           
        }

        public static string DatePickerToDateForDB2(string strDate)
        {
            //04/09/2015
            try
            {
                /*2017-06-13*/
                string[] arr = strDate.Split('-');
                string y = arr[0]; //yyyy - 543
                int yyyy = Convert.ToInt32(y) - 543;
                string m = arr[1];
                string d = arr[2];
                string date = (yyyy + "" + m + "" + d);
                return date;
            }
            catch (Exception)
            {
                return "";

            }
        }

        public static string DatePickerToDateForDB3(string strDate)
        {
            //04/09/2015
            try
            {
                /*2017-06-13*/
                string[] arr = strDate.Split('-');
                string y = arr[0]; //yyyy - 543
                string m = arr[1];
                string d = arr[2];
                string date = (y + "" + m + "" + d);
                return date;
            }
            catch (Exception)
            {
                return "";

            }
        }


        public static string DateForDBToDatePicker(string strDate)
        {
            //21-01-2018
            if (strDate == "" || strDate == null)
            {
                return "";
            }
            else
            {
                char[] arr = strDate.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];
                return (d + "-" + m + "-" + y);
                //string date = (m + "/" + d + "/" + y);
                //return date;
            }

        }

        public static string ToThaiFormatDate(string str)
        {
            try
            {
                char[] arr = str.ToCharArray();
                string y = "" + arr[0] + arr[1] + arr[2] + arr[3];
                string m = "" + arr[4] + arr[5];
                string d = "" + arr[6] + arr[7];

                int yyyy = Convert.ToInt32(y) + 543;

                return (d + " " + ToMonthThai(Convert.ToInt32(m)) + " " + yyyy);

            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static string ToThaiFormatDate(DateTime datetime)
        {
            try
            {

                int yyyy = datetime.Year + 543;
                return (datetime.Day + " " + ToMonthThai(datetime.Month) + " " + yyyy);

            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static string ToYYYYMMDD(DateTime datetime)
        {
            try
            {
                return (datetime.Day + "" + ToMonthThai(datetime.Month) + "" + datetime.Year);
            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static string ToThaiFormatDatetime(DateTime datetime)
        {
            try
            {

                int yyyy = datetime.Year + 543;
                return (datetime.Day + " " + ToMonthThai(datetime.Month) + " " + yyyy) + " " + datetime.Hour+":" + datetime.Minute +":"+datetime.Second;

            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static string toTimeString(DateTime datetime)
        {
            try
            {

                int yyyy = datetime.Year + 543;
                return  datetime.Hour + ":" + datetime.Minute + ":" + datetime.Second;

            }
            catch (Exception e)
            {
                return "-";
            }
        }

        public static bool IsExpired(DateTime effective_date, DateTime expiryDate)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var dateNow = DateTime.Parse(Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
            var CurrentDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 0, 0, 0);

            if (effective_date < CurrentDate) {
                effective_date = CurrentDate;
            }

            double total = (expiryDate - effective_date).TotalDays;
            return (total >= 0d) ? false : true; // true = หมดอายุ , false = ใช้งานได้
        }

        public static bool IsEffectiveInMall(DateTime LastDatetime)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var dateNow = DateTime.Parse(Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
            LastDatetime.AddHours(1);

            //var one = DateTime.Now.AddHours(1);
            //var two = DateTime.Now;
            var diff = dateNow.TimeOfDay - LastDatetime.TimeOfDay;
            var count = diff.Minutes;

            if (count > 60)
            {
                return true; // เกิน 1 hr แล้ว insert ซ้ำได้
            }
            else {
                return false; // ยังไม่ถึง 1 hr insert ซ้ำไม่ได้ 
            }
        }

        //effective
        public static bool IsEffective(DateTime effective)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var dateNow = DateTime.Parse(Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
            var CurrentDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 0, 0, 0);

            double total = (CurrentDate - effective).TotalDays;
            return (total >= 0d) ? true : false;
        }

        public static DateTime CurrentDateTime() {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var dateNow = DateTime.Parse(Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
            return new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 0, 0, 0);
        }

        public static DateTime setDateTime(string strdate, string time)
        {

            //date = "07/04/2017"
            //time = "15:00"
            string[] arr = strdate.Split('/');
            string[] t = time.Split(':');
            int yyyy = Convert.ToInt32(arr[2]);
            int m = Convert.ToInt32(arr[1]);
            int d = Convert.ToInt32(arr[0]);

            DateTime dateNow = new DateTime(yyyy, m, d);
            //TimeSpan ts = new TimeSpan(Convert.ToInt32(t[0]), Convert.ToInt32(t[1]), 0);

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //var dateNow = DateTime.Parse(dnow + " " + ts, new System.Globalization.CultureInfo("en-US"));
            var datenow = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, Convert.ToInt32(t[0]), Convert.ToInt32(t[1]), 0);
            return datenow;
        }

        public static int DiscountExpiryDate(DateTime effective_date, DateTime expiryDate)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var dateNow = DateTime.Parse(Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
            var CurrentDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 0, 0, 0);
            if (effective_date < CurrentDate)
            {
                effective_date = CurrentDate;
            }
            double total = (expiryDate - effective_date).TotalDays;
            return (int) total + 1;
        }

        public static string DatetimeStrDisplayBackend(DateTime effective_date, DateTime? expiry_date)
        {

            try
            {

                if (expiry_date == null)
                {
                    return effective_date.ToShortDateString();
                }
                else
                {

                    bool is_expire_date = DateFormate.IsExpired(effective_date, (DateTime)expiry_date);
                    if (is_expire_date)
                    {
                        string start_date = effective_date.ToString("dd/MM/yyyy");
                        string expired_date = ((DateTime)expiry_date).ToString("dd/MM/yyyy");

                        return start_date + "-" + expired_date + "<span class='badge badge-danger'> Expired </span>";
                    }
                    else
                    {
                        int discount_expire_date = DateFormate.DiscountExpiryDate(effective_date, (DateTime)expiry_date);

                        string start_date = effective_date.ToString("dd/MM/yyyy");
                        string expired_date = ((DateTime)expiry_date).ToString("dd/MM/yyyy");

                        string desc = (discount_expire_date == 1) ? " <span class='badge badge-success'> Last Date</span>" : " <span class='badge badge-success'> " + discount_expire_date + " Day</span>";
                        return start_date + " - " + expired_date + desc;
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        //30-03-2560

        public static DateTime convertToDatetimeDDMMYYYY(string dateStr)
        {
            try
            {
                //2560-03-31
                string[] arr = dateStr.Split('-');
                string y =  arr[0];
                string m =  arr[1];
                string d =  arr[2] ;
                //IFormatProvider culture = new CultureInfo("en-US",true);
                //DateTime datetime = new DateTime.ParseExact(date,"yyyy-MM-dd", culture);
                return new DateTime(Convert.ToInt32(y) -543, Convert.ToInt32(m), Convert.ToInt32(d));
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }

        }

        public static string ToDayThai(string strDate)
        {
            if (strDate == "" || strDate == null)
            {
                return "";
            }
            else
            {
                //char[] arr = strDate.ToCharArray();
                string[] arr = strDate.Split('/');
                int y = Convert.ToInt32(arr[2]) + 543; // พ.ศ.
                string m = ToMonthThai(Convert.ToInt32(arr[0]));
                string d = arr[1];
                //return (d + "/" + m + "/" + y);
                string date = (d + " " + m + " " + y);
                return date;
            }
        }
        public static string ToMonthThai(int number)
        {
            switch (number)
            {
                case 1:
                    return "มกราคม";
                case 2:
                    return "กุมภาพันธ์";
                case 3:
                    return "มีนาคม";
                case 4:
                    return "เมษายน";
                case 5:
                    return "พฤษภาคม";
                case 6:
                    return "มิถุนายน";
                case 7:
                    return "กรกฎาคม";
                case 8:
                    return "สิงหาคม";
                case 9:
                    return "กันยายน";
                case 10:
                    return "ตุลาคม";
                case 11:
                    return "พฤศจิกายน";
                case 12:
                    return "ธันวาคม";
                default:
                    return "-";
            }
        }



        public static string toHHMM(string strDate)
        {
            if (strDate != null)
            {
                char[] arr = strDate.ToCharArray();
                string h = "" + arr[0] + arr[1];
                string m = "" + arr[2] + arr[3];
                //string s = "" + arr[4] + arr[5];
                return h + ":" + m;
            }
            else {
                return null;
            }

        }

        #region DatePicker
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
        #endregion
    }
}
