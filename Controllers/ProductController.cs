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

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        static ShoppingCartItem cartitem = new ShoppingCartItem();
        static String msg = string.Empty;

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

            paraOut.Add("@ProductID", index);
            paraOut.Add("@ProductCode", index);
            paraOut.Add("@ProductName", index);
            paraOut.Add("@Category", index);



              = new GetDataFromDatabase().DBGetProduct(paraIn, paraOut);

            cartitem.Product.ProductID =
            cartitem.Quantity =


            @ProductID = [ProductID]
      ,@ProductCode = [ProductCode]
      ,@ProductName = [ProductName]
      ,@Category = [Category]
      ,@Quantity = [Quantity]
      ,@Price = [Price]




            return View(cartitem);
        }
        [HttpPost]
        public ActionResult UpdateProduct(ShoppingCartItem cartitemin)
        {
            // Update product in data source using updatedProduct.ProductId
            UpdateProductById(cartitemin);
            // Redirect to a different action that displays the updated product details
            return RedirectToAction("ViewProduct", new { productId = cartitemin.Product.ProductID });
        }
    }
}