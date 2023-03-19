using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ProductCartViewModel
    {
        public List<Product> Products { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public DataSet DBProduct { get; set; }
    }
}