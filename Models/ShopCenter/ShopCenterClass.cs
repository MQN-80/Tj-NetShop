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
        public long price { get; set; }
    }   

    public class shop
    {
        public string shopId { get; set; }
        public string storeName { get; set; }
        public string storeImg { get; set; }
        public string storeTypeId { get; set; }
        public string storeDesc { get; set; }
        public string createTime { get; set; }
    }
}
