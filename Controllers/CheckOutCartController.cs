using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebApplication.Models;
using Shopping.Models;
using System.Data.Entity.Infrastructure;

namespace WebApplication.Controllers
{
    public class CheckOutCartController : Controller
    {
        static ShoppingCart cart = new ShoppingCart();
        static String msg = string.Empty;
        public ActionResult CheckOutCart()
        {
             ViewBag.Message = msg;


            return View(cart);
        }

        [HttpPost]
        public ActionResult CheckOutProduct(ShoppingCart carts)
        {

           var ShoppingCheckOutCart = (ShoppingCart)Session["cart"];

            cart = ShoppingCheckOutCart;
            msg = string.Empty;
            foreach (var item in cart.GetItems())
            {

                Dictionary<string, object> parameters = new Dictionary<string, object>();

                parameters.Add("@ShoppingCarttID", (DateTime.Now.Date - new DateTime(2023, 01, 01).Date).TotalDays);
                parameters.Add("@ProductID", item.Product.ProductID);
                parameters.Add("@Quantity", item.Quantity);


                 msg = new GetDataFromDatabase().DBAddShoppingCart(parameters);
            }


            String search = "successfully";

            if (msg.ToLower().IndexOf(search.ToLower()) != -1)
            {



                cart.Discard();
                ViewBag.GetTotalPrice = 0;
                ViewBag.GetItemCount = 0;

                Session.Clear();
            }

           


            return RedirectToAction("CheckOutCart", "Home");
        }
    }
}