using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.ShopCenter;

namespace WebApi.Controllers.ShopCenter
{
    public class ShopCenterController
    {
        /*
         * 返回四个随机商品
         */
        [Route("/ShopCenter/get_delivery_address")]
        [HttpGet]
        public string get_4_random_product()
        {
            ShopCenterModel shopCenterModel = new ShopCenterModel();
            return shopCenterModel.Get_4_Random_Product();
        }
       
    }
}
