using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{

    [Table("Category")]
    public class Category
    {
        [Key]
        public int id { set; get; }
        public string category_name { set; get; }

        [NotMapped]
        public string category_id { set; get; }

    }
}
