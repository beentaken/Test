using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebApplication.Models;
using Shopping.Models;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Reflection;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        static ShoppingCartItem cartitem = new ShoppingCartItem();
        static String msg = string.Empty;
        GetDataFromDatabase DB = new GetDataFromDatabase();

        public ActionResult Product()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProductWithId(int index)
        {

            Dictionary<string, object> paraIn = new Dictionary<string, object>();

            paraIn.Add("@ProductID", index);

            Dictionary<string, object> paraOut = new Dictionary<string, object>();

            paraOut.Add("@ProductID", p.ProductID);
            paraOut.Add("@ProductCode", p.ProductCode);
            paraOut.Add("@ProductName", p.ProductName);
            paraOut.Add("@Category", p.Category);
            paraOut.Add("@Quantity", cartitemin.Quantity);
            paraOut.Add("@Price", p.Price);



            DB.DBGetProduct(paraIn, paraOut);

      //      cartitem.Product.ProductID =
      //      cartitem.Quantity =


      //      @ProductID = [ProductID]
      //,@ProductCode = [ProductCode]
      //,@ProductName = [ProductName]
      //,@Category = [Category]
      //,@Quantity = [Quantity]
      //,@Price = [Price]




            return View(cartitem);
        }
        [HttpPost]
        public ActionResult UpdateProduct(ShoppingCartItem cartitemin)
        {

            Dictionary<string, object> paraIn = new Dictionary<string, object>();
            Product p = cartitemin.Product;

            paraIn.Add("@ProductID", p.ProductID);
            paraIn.Add("@ProductCode", p.ProductCode);
            paraIn.Add("@ProductName", p.ProductName);
            paraIn.Add("@Category", p.Category);
            paraIn.Add("@Quantity", cartitemin.Quantity);
            paraIn.Add("@Price", p.Price);

            // Update product in data source using updatedProduct.ProductId
            DB.DBUpdateProduct(paraIn);
            // Redirect to a different action that displays the updated product details
            return RedirectToAction("ViewProduct", new { productId = cartitemin.Product.ProductID });
        }
    }
}