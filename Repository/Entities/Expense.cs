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
CREATE TABLE [dbo].[Expense](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[submited] [varchar](8) NULL,
	[category_id] [int] NULL,
	[supplier_id] [int] NULL,
	[description] [nvarchar](max) NULL,
	[amount] [float] NULL,
	[due_date] [varchar](8) NULL,
	[paid_date] [varchar](8) NULL,
	[get_receipt] [bit] NULL,
	[withholding_tax] [bit] NULL,
	[receipt_format] [varchar](50) NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
         */
    [Table("Expense")]
    public class Expense
    {
        [Key]
        public int id { set; get; }

        public string submited { set; get; }
        //public string name_th { set; get; }
        public int category_id { set; get; }

        public int supplier_id { set; get; }
        public string description { set; get; }
        public string due_date { set; get; }
        public string paid_date { set; get; }

        public bool withholding_tax { set; get; }
        public bool get_receipt { set; get; }

        public string receipt_format { set; get; }
        public double amount { set; get; }

        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }

        [NotMapped]
        public string expense_id { set; get; }

        public Expense()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            this.create_date = DateTime.Parse(Digix.Utilites.Utility.GetDateTimeNow().ToString(), new System.Globalization.CultureInfo("en-US"));
            this.update_date = this.create_date;

            this.submited = this.create_date.ToShortDateString();
            this.category_id = 6;
            this.supplier_id = 1;
        }

    }
}
