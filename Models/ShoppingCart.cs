using System.Collections.Generic;
using System.Linq;

namespace Shopping.Models
{
    public class ShoppingCart
    {
        static List<ShoppingCartItem> items = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
            items = new List<ShoppingCartItem>();
        }
        public List<ShoppingCartItem> CartItem()
        {
            return items;
        }



        // Add an item to the shopping cart
        public void AddItem(Product product, int quantity)
        {
            ShoppingCartItem item = items.SingleOrDefault(i => i.Product.ProductID == product.ProductID);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                items.Add(new ShoppingCartItem { Product = product, Quantity = quantity });
            }
        }

        // Remove an item from the shopping cart
        public void RemoveItem(int ProductID)
        {
            ShoppingCartItem item = items.SingleOrDefault(i => i.Product.ProductID == ProductID);

            if (item != null)
            {
                items.Remove(item);
            }
        }

        // Get the total number of items in the shopping cart
        public int GetItemCount()
        {
            return items.Sum(i => i.Quantity);
        }

        // Get the total price of all items in the shopping cart
        public decimal GetTotalPrice()
        {
            return items.Sum(i => i.Quantity * i.Product.Price);
        }

        // Get a list of ShoppingCartItems in the shopping cart
        public List<ShoppingCartItem> GetItems()
        {
            return items;
        }

        // Clear all items from the shopping cart
        public void Discard()
        {
            items.Clear();
        }
    }
}
