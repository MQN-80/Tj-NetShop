using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopCenter
{
   
    public class Product_info
    {
        public string name { get; set; }
        public string img { get; set; }
        public string price { get; set; }
    }

    public class user
    {
        public string userName { get; set; }
        public string userDetail { get; set; }
    }

    public class product
    {
        public string name { get; set; }
        public string price { get; set; }
    }
}
