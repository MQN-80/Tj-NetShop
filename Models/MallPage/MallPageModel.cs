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
            MallPageDatabase mall = new MallPageDatabase();
            return mall.GetFourRandomProduct();
        }
        public string  Get_4_Collected_Product()
        {
            MallPageDatabase mall = new MallPageDatabase();
            return mall.GetFourCollectedProduct();
        }
        public string Get_4_Discount_Product()
        {
            MallPageDatabase mall = new MallPageDatabase();
            return mall.GetFourDiscountProduct();
        }
        public string Get_Random_Shop_Product()
        {
            MallPageDatabase mall = new MallPageDatabase();
            return mall.GetRandomShopProduct();
        }
     }
}
