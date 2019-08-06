using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace MvcwithoutE.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        [DisplayName("Product Name")]
        public string productName { get; set; }
        public decimal price { get; set; }
        public int Count { get; set; }
    }
}