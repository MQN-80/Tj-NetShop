using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.ShopTransaction;

namespace WebApi.Controllers.ShopTransaction
{
  public class ShopTransactionController
  {
    /*
     * 返回全部的收货地址
     */
    [Route("/ShopTransaction/get_delivery_address")]
    [HttpGet]
    public string get_delivery_address(int UserID)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Get_delivery_address(UserID);
    }
    /*
     * 创建新的订单
     */
    [Route("/ShopTransaction/add_deal_record")]
    [HttpPost]
    public string add_deal_record(string Product_id, string Ord_price, int UserID)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Add_deal_record(Product_id, Ord_price, UserID);
    }
    /*
     * 返回用户订单
     */
    [Route("/ShopTransaction/get_deal_record")]
    [HttpGet]
    public string get_deal_record(int UserID)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Get_deal_record(UserID);
    }
    /*
     * 修改订单信息
     */
    [Route("/ShopTransaction/modify_deal_record")]
    [HttpPut]
    public string modify_deal_record(string Trade_id)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Modify_deal_record(Trade_id);
    }
    /*
     * 返回用户积分
     */
    [Route("/ShopTransaction/get_User_Credits")]
    [HttpGet]
    public string get_User_Credits(string UserID)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Get_User_Credits(UserID);
    }
    /*
     *修改用户积分
     */
    [Route("/ShopTransaction/modify_Credits_Record")]
    [HttpPut]
    public string modify_Credits_Record(string UserID, string Trade_id, int Credits_change, string Status)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Modify_Credits_Record(UserID, Trade_id, Credits_change, Status);
    }

    [Route("/ShopTransaction/goods_Transaction")]
    [HttpPut]
    public string goods_Transaction(string Consumer_UserID, string Business_UserID,int Credits_change, string Status)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Goods_Transaction(Consumer_UserID, Business_UserID,Credits_change, Status);
    }

    // from lyp
    [Route("/ShopTransaction/get_credit_record")]
    [HttpGet]
    public string get_credit_record(string userID)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Get_Credits_Record(userID);
    }

    [Route("/ShopTransaction/search_productInfo")]
    [HttpGet]
    public string search_productInfo(string product_name)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Search_ProductInfo(product_name);
    }

    [Route("/ShopTransaction/search_user_collect")]
    [HttpGet]
    public string search_user_collect(int UserID)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Search_User_Collect(UserID);
    }

    [Route("/ShopTransaction/search_user_collectShop")]
    [HttpGet]
    public string search_user_collectShop(int UserID)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Search_User_CollectShop(UserID);
    }

    [Route("/ShopTransaction/add_delivery_address")]
    [HttpPost]
    public string add_delivery_address(int user_id, string addr, string phone_number, string name, int add_default)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Add_Delivery_Address(user_id, addr, phone_number, name, add_default);
    }

    [Route("/ShopTransaction/delete_delivery_address")]
    [HttpDelete]
    public string delete_delivery_address(string id)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Delete_Delivery_Address(id);
    }

    [Route("/ShopTransaction/edit_delivery_address")]
    [HttpPut]
    public string edit_delivery_address(string id, string addr, string phone_number, string name, int add_default)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Edit_Delivery_Address(id, addr, phone_number, name, add_default);
    }

    [Route("/ShopTransaction/goods_transaction_primer_plus")]
    [HttpPut]
    public string goods_transaction_primer_plus(string Trade_id)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.GoodsTransactionPrimerPlus(Trade_id);
    }

    [Route("/ShopTransaction/get_goods_userInfo")]
    [HttpGet]
    public string get_goods_userInfo(string id)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.GetGoodsUserInfo(id);
    }

    [Route("/ShopTransaction/add_trolley")]
    [HttpPost]
    public string add_trolley(int User_id, string Product_id, int Product_num)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Addtrolley(User_id, Product_id, Product_num);
    }

    [Route("/ShopTransaction/get_trolley")]
    [HttpGet]
    public string get_trolley(string user_id)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.GetTrolley(user_id);
    }

    [Route("/ShopTransaction/add_collect")]
    [HttpPost]
    public string add_collect(int user_id, string product_id, int price)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.AddCollect(user_id, product_id, price);
    }

    [Route("/ShopTransaction/is_collect")]
    [HttpGet]
    public string is_collect(string id, int user_id)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.IsCollect(id, user_id);
    }
    //根据商品类别查找商品
    [Route("/ShopTransaction/search_product_type")]
    [HttpGet]
    public string search_product_type(string product_type)
    {
       ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
       return shopTransactionModel.findProductType(product_type);
    }
        [Route("/ShopTransaction/clear_trolley")]
        [HttpDelete]
        public string clear_trolley(int user_id)
        {
            ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
            return shopTransactionModel.ClearTrolley(user_id);
        }

        [Route("/ShopTransaction/delete_trolley")]
        [HttpDelete]
        public string delete_trolley(int id)
        {
            ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
            return shopTransactionModel.DeleteTrolley(id);
        }
    }
}

    
