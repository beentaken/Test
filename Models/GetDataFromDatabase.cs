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

        public String DBAddShoppingCart(Dictionary<String, Object> keyValueIn)
        {
            return bc.DBStoreprocInMsg("SpAddShoppingCart", keyValueIn);
        }
        public String DBGetProduct(Dictionary<String, Object> keyValueIn, out Dictionary<String, Object> keyValueOut)
        {
            return bc.DBStoreprocInOut("SpGetProduct", keyValueIn, out keyValueOut);
        }
        public String DBUpdateProduct(Dictionary<String, Object> keyValueIn)
        {
            return bc.DBStoreprocInMsg("SpUpdateProduct", keyValueIn);
        }
    }
}