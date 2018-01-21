using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            iDisplayStart = 0;
            iDisplayLength = 10;
        }
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        public string sFrom { get; set; }

        public string sTo { get; set; }

        public int iSortCol_0 { get; set; }

        public string iSort { get; set; }

        public string sSortDir_0 { get; set; }

        public int Coursekey_0 { set; get; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        //public int Key { get; set; }

        public string Type { get; set; }
        public string lang { set; get; }



    }
}
