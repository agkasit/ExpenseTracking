using Digix.Utilites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class ActivityLog : BaseModel
    {
        public ActivityLog()
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            this.create_datefull = DateTime.Parse(Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
            this.createdate = Utility.GetDateTimeNow().ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
            this.createtime = Utility.GetDateTimeNow().ToString("HHmmss", CultureInfo.CreateSpecificCulture("en-US"));
            this.createby = this.refid;

            this.page = 1;
            this.per_page = 10;
        }

        public long id { get; set; }

        public string message { get; set; }

        [StringLength(15)]
        public string phone { set; get; }

        public string refid { get; set; }

        public string status { get; set; }

        public string error_message { get; set; }

        public string action { get; set; }

        public string controller { get; set; }

        public DateTime create_datefull { get; set; }

        public string createdate { get; set; }

        public string createtime { get; set; }

        public string createby { get; set; }

        public string udid { get; set; }

        public string machine { get; set; }
        public string platform { get; set; }
        public string version_app { get; set; }
        public string version_os { get; set; }
        public string ip_address { get; set; }
        public string network { get; set; }
        public string param { get; set; }
        public string alert { set; get; }


        [NotMapped]
        public int page { set; get; }
        [NotMapped]
        public int per_page { set; get; }

        [NotMapped]
        public string token_id { set; get; }

        [NotMapped]
        public string keyword { set; get; }

        //[NotMapped]
        //public string lang { set; get; }

        [NotMapped]
        public string local_lang { set; get; }

        [NotMapped]
        public string country_code { set; get; }

        [NotMapped]
        public string locale_id { set; get; }

        [NotMapped]
        public string indoor_atlas_id { set; get; }
    }
}
