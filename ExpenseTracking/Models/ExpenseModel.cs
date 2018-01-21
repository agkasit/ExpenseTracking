using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracking.Models
{
    public class ExpenseModel : BaseModel
    {
        public string expense_id { set; get; }
        public bool unpaid { set; get; }
        public bool not_get_receipt { set; get; }
    }

    public class SupplierModel : BaseModel
    {
        public string supplier_id { set; get; }
        public bool unpaid { set; get; }
        public bool not_get_receipt { set; get; }
    }


}