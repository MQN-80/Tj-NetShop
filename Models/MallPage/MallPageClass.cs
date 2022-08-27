using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.MallPage
{

    public class Product_info
    {
        public string id { get; set; }
        public string name { get; set; }
        public string des{ get; set; }
        public string price { get; set; }

        public string imgPath { get; set; }
    }

}
