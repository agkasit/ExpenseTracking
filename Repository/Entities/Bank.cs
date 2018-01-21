using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{

    [Table("Bank")]
    public class Bank
    {
        [Key]
        public int id { set; get; }
        public string bank_name { set; get; }

        [NotMapped]
        public string bank_id { set; get; }

    }
}
