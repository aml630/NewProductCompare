using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace ProductCompareDotNet.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public string ProductBigImg { get; set; }
        public string ProductLink { get; set; }
        public int ProductPrice { get; set; }
        public string ProductDescription { get; set; }

        public string title { get; set; }
        public string intro { get; set; }
        public string par1 { get; set; }
        public string par2 { get; set; }
        public string par3 { get; set; }
        public string conclusion { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        public DateTime DateTime { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }

   

        //public virtual ICollection<ApplicationUser> Users { get; set; }





    }
}
