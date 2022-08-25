using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.MallPage;

namespace WebApi.Controllers.MallPage
{
    public class MallPageController
    {
        /*
         * 返回四个随机商品
         */
        [Route("/MallPage/get4randomProduct")]
        [HttpGet]
        public string get_4_random_product()
        {
            MallPageModel mallPageModel = new MallPageModel();
            return mallPageModel.Get_4_Random_Product();
        }

        //返回四个最近上架商品,基本没问题了
        [Route("/MallPage/get4recentProduct")]
        [HttpGet]
        public string get_4_collected_product()
        {
            MallPageModel mallPageModel = new MallPageModel();
            return mallPageModel.Get_4_Collected_Product();
        }
        //返回四个活动商品
        [Route("/MallPage/get4discountproduct")]
        [HttpGet]
        public string get_4_discount_product()
        {
            MallPageModel mallPageModel = new MallPageModel();
            return mallPageModel.Get_4_Discount_Product();
        }
        //返回一家商店的多个商品
        [Route("/MallPage/get4shopProduct")]
        [HttpGet]
        public string get_random_shop_product()
        {
            MallPageModel mallPageModel = new MallPageModel();
            return mallPageModel.Get_Random_Shop_Product();
        }
    }
}
