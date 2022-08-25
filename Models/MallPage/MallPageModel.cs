using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.MallPage
{
    public class MallPageModel
    {
        public string Get_4_Random_Product()
        {
            return MallPageDatabase.GetFourRandomProduct();
        }
        public string  Get_4_Collected_Product()
        {
            return MallPageDatabase.GetFourCollectedProduct();
        }
        public string Get_4_Discount_Product()
        {
            return MallPageDatabase.GetFourDiscountProduct();
        }
        public string Get_Random_Shop_Product()
        {
            return MallPageDatabase.GetRandomShopProduct();
        }
     }
}
