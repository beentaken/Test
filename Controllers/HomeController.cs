using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebApplication.Models;
using Shopping.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        static ShoppingCart cart = new ShoppingCart();
        private DataSet ds = null;
        static List<Product> products = new List<Product>();
        static String msg = string.Empty;
        public ActionResult Index()
        {
            ds = new GetDataFromDatabase().GetProductsFromDatabase();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Product product = new Product
                {
                    ProductID = Convert.ToInt32(row["ProductID"]),
                    ProductCode = row["ProductCode"].ToString(),
                    ProductName = row["ProductName"].ToString(),
                    Category = row["Category"].ToString(),
                    Price = Convert.ToDecimal(row["Price"])
                };
                products.Add(product);
            }

            var viewModel = new ProductCartViewModel
            {
                Products = products,
                ShoppingCart = cart,
                DBProduct = ds
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            Product prod = products.FirstOrDefault(p => p.ProductID == productId);

            cart.AddItem(prod, quantity);
            var viewModel = new ProductCartViewModel
            {
                Products = products,
                ShoppingCart = cart,
                DBProduct = ds
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int index)
        {
            // remove item from cart
            cart.RemoveItem(index);
            var viewModel = new ProductCartViewModel
            {
                Products = products,
                ShoppingCart = cart,
                DBProduct = ds
            };
            // redirect to cart view
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult CheckOutCart()
        {

            ViewBag.GetTotalPrice = cart.GetTotalPrice();
            ViewBag.GetItemCount = cart.GetItemCount();
            Session.Clear();
            Session["cart"] = cart;
            return View(cart);
        }

        [HttpPost]
        public ActionResult DiscardCart()
        {  
            cart.Discard();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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

        public ActionResult CheckoutCart()
        {
            ViewBag.Message = msg;
            return View(cart);
        }
    }
}