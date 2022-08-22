using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopCenter
{
    public class ShopCenterModel
    {
        public string Get_4_Random_Product()
        {
            return ShopCenterDatabase.GetFourRandomProduct();
        }
    }
}
