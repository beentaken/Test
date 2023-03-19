using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication.App_Data;

namespace WebApplication.Models
{
    public class GetDataFromDatabase
    {
        DBConnectionClass bc = new DBConnectionClass();
        public DataSet GetProductsFromDatabase() {
            return bc.DBViews("ViewProducts");
        }

        public String DBAddShoppingCart(Dictionary<String, Object> keyValuePairs)
        {
            return bc.DBAddShoppingCart("SpAddShoppingCart", keyValuePairs);
        }
    }
}