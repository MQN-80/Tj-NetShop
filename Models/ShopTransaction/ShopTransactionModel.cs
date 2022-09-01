using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopTransaction
{
  public class ShopTransactionModel
  {
    public string Get_delivery_address(int UserID)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
      return shopt.GetDeliveryAddress(UserID);
    }

    public string Add_deal_record(string Product_id, string Ord_price, int UserID)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.AddDealRecord(Product_id, Ord_price, UserID);
    }

    public string Get_deal_record(int UserID)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.GetDealRecord(UserID);
    }

    public string Modify_deal_record(string Trade_id)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.ModifyDealRecord(Trade_id);
    }

    public string Get_User_Credits(string UserID)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.GetUserCredits(UserID);
    }

    public string Modify_Credits_Record(string UserID, string Trade_id, int Credits_change, string Status)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.ModifyCreditsRecord(UserID, Trade_id, Credits_change, Status);
    }

    public string Goods_Transaction(string Consumer_UserID, string Business_UserID,int Credits_change, string Status)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.GoodsTransaction(Consumer_UserID, Business_UserID,Credits_change, Status);
    }

    // from lyp
    public string Get_Credits_Record(string UserID)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.GetCreditRecord(UserID);
    }

    public string Search_ProductInfo(string product_name)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.SearchProductInfo(product_name);
    }

    public string Search_User_Collect(int UserID)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.SearchUserCollect(UserID);
    }

    public string Search_User_CollectShop(int UserID)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.SearchUserCollectShop(UserID);
    }

    public string Add_Delivery_Address(int user_id, string addr, string phone_number, string name, int add_default)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.AddDeliveryAddress(user_id, addr, phone_number, name, add_default);
    }

    public string Delete_Delivery_Address(string id)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.DeleteDeliveryAddress(id);
    }

    public string Edit_Delivery_Address(string id, string addr, string phone_number, string name, int add_default)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.EditDeliveryAddress(id, addr, phone_number, name, add_default);
    }

    public string GoodsTransactionPrimerPlus(string Trade_id,string trolley_id)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.GoodsTransactionPrimerPlus(Trade_id, trolley_id);
    }

    public string GetGoodsUserInfo(string id)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.GetGoodsUserInfo(id);
    }

    public string Addtrolley(int User_id, string Product_id, int Product_num)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.Addtrolley(User_id, Product_id, Product_num);
    }
    
    public string GetTrolley(string user_id)
    {
            ShopTransactionDatabase shopt = new ShopTransactionDatabase();
            return shopt.GetTrolley(user_id);
    }
  }
 
}
