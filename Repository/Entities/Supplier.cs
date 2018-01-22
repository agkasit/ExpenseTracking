using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Entities
{
    /*
    CREATE TABLE [dbo].[Supplier](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[supplier_name] [nvarchar](255) NULL,
	[tax_id] [nvarchar](50) NULL,
	[contact] [nvarchar](255) NULL,
	[phone] [nvarchar](50) NULL,
	[email] [nvarchar](200) NULL,
	[bank_id] [int] NULL,
	[account_no] [varchar](50) NULL,
	[account_name] [nvarchar](255) NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
        */
    [Table("Supplier")]
    public class Supplier
    {
        [Key]
        public int id { set; get; }
        public string supplier_name { set; get; }
        //public string name_th { set; get; }
        public string tax_id { set; get; }
        public string contact { set; get; }
        public string phone { set; get; }

        public string email { set; get; }
        public int bank_id { set; get; }

        public string account_no { set; get; }

        public string account_name { set; get; }

        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }

        [NotMapped]
        public string supplier_id { set; get; }

        public Supplier()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            this.create_date = DateTime.Parse(Digix.Utilites.Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
            this.update_date = this.create_date;
        }

        public void setUpdateDate() {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            this.update_date = DateTime.Parse(Digix.Utilites.Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
        }

    }
}
