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

        public string get_shop_info(string shopUserId)
        {
            return ShopCenterDatabase.getShopInfo(shopUserId);
        }

        public string get_shop_product(string shopUserId)
        {
            return ShopCenterDatabase.getShopProduct(shopUserId);
        }

        public string follow_shop(int userId, string shopUserId)
        {
            return ShopCenterDatabase.followShop(userId, shopUserId);
        }

        public string cancel_follow_shop(int userId, string shopUserId)
        {
            return ShopCenterDatabase.cancelFollowShop(userId, shopUserId);
        }

        public string post_product(string shopUserId, string productName, string productType, string productDes, int price)
        {
            return ShopCenterDatabase.postProduct(shopUserId, productName, productType, productDes, price);
        }

        public string delete_product(int productId, string shopUserId)
        {
            return ShopCenterDatabase.deleteProduct(productId, shopUserId);
        }
    }
}
