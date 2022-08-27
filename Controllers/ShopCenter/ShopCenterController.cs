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

        //返回店铺信息
        [Route("/ShopCenter/get_shop_info")]
        [HttpGet]
        public string get_shop_info(string shopUserId)
        {
            ShopCenterModel shopCenterModel = new ShopCenterModel();
            return shopCenterModel.get_shop_info(shopUserId);
        }

        //返回店铺商品
        [Route("/ShopCenter/get_shop_product")]
        [HttpGet]
        public string get_shop_product(string shopUserId)
        {
            ShopCenterModel shopCenterModel = new ShopCenterModel();
            return shopCenterModel.get_shop_product(shopUserId);
        }

        //关注店铺
        [Route("/ShopCenter/follow_shop")]
        [HttpPost]
        public string follow_shop(int userId, string shopUserId)
        {
            ShopCenterModel shopCenterModel = new ShopCenterModel();
            return shopCenterModel.follow_shop(userId, shopUserId);
        }

        //取消关注店铺
        [Route("/ShopCenter/cancel_follow_shop")]
        [HttpDelete]
        public string cancel_follow_shop(int userId, string shopUserId)
        {
            ShopCenterModel shopCenterModel = new ShopCenterModel();
            return shopCenterModel.cancel_follow_shop(userId, shopUserId);
        }

        //发布商品
        [Route("/ShopCenter/post_product")]
        [HttpPost]
        public string post_product(string shopUserId, string productName, string productType, string productDes, int price)
        {
            ShopCenterModel shopCenterModel = new ShopCenterModel();
            return shopCenterModel.post_product(shopUserId, productName, productType, productDes, price);
        }

        //删除发布商品
        [Route("/ShopCenter/delete_product")]
        [HttpDelete]
        public string delete_product(int productId, string shopUserId)
        {
            ShopCenterModel shopCenterModel = new ShopCenterModel();
            return shopCenterModel.delete_product(productId, shopUserId);
        }
    }
}
