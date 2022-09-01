using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopCenter
{
    public class ShopCenterModel
    {
        public string getProduct(string id)
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.getProduct(id);
        }
        public string Get_4_Random_Product()
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.GetFourRandomProduct();
        }
        public string is_follow(int userid,string shopid)
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.is_follow(userid, shopid);
        }
        public string get_shop_info(string shopUserId)
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.getShopInfo(shopUserId);
        }

        public string get_shop_product(string shopUserId)
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.getShopProduct(shopUserId);
        }

        public string follow_shop(int userId, string shopUserId)
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.followShop(userId, shopUserId);
        }

        public string cancel_follow_shop(int userId, string shopUserId)
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.cancelFollowShop(userId, shopUserId);
        }

        public string post_product(string shopUserId, string productName, string productType, string productDes, int price)
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.postProduct(shopUserId, productName, productType, productDes, price);
        }

        public string delete_product(int productId, string shopUserId)
        {
            ShopCenterDatabase shopcenter = new ShopCenterDatabase();
            return shopcenter.deleteProduct(productId, shopUserId);
        }
    }
}
