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
      return ShopTransactionDatabase.GetDeliveryAddress(UserID);
    }

    public string Add_deal_record(string Product_id, string Ord_price, int UserID)
    {
      return ShopTransactionDatabase.AddDealRecord(Product_id, Ord_price, UserID);
    }

    public string Get_deal_record(int UserID)
    {
      return ShopTransactionDatabase.GetDealRecord(UserID);
    }

    public string Modify_deal_record(string Trade_id)
    {
      return ShopTransactionDatabase.ModifyDealRecord(Trade_id);
    }

    public string Get_User_Credits(string UserID)
    {
      return ShopTransactionDatabase.GetUserCredits(UserID);
    }

    public string Modify_Credits_Record(string UserID, string Trade_id, int Credits_change, string Status)
    {
      return ShopTransactionDatabase.ModifyCreditsRecord(UserID, Trade_id, Credits_change, Status);
    }

    public string Goods_Transaction(string Consumer_UserID, string Business_UserID, string Trade_id, int Credits_change, string Status)
    {
      return ShopTransactionDatabase.GoodsTransaction(Consumer_UserID, Business_UserID, Trade_id, Credits_change, Status);
    }

    // from lyp
    public string Get_Credits_Record(string UserID)
    {
      return ShopTransactionDatabase.GetCreditRecord(UserID);
    }

    public string Search_ProductInfo(string product_name)
    {
      return ShopTransactionDatabase.SearchProductInfo(product_name);
    }

    public string Search_User_Collect(int UserID)
    {
      return ShopTransactionDatabase.SearchUserCollect(UserID);
    }

    public string Search_User_CollectShop(int UserID)
    {
      return ShopTransactionDatabase.SearchUserCollectShop(UserID);
    }

    public string Add_Delivery_Address(int user_id, string addr, string phone_number, string name, int add_default)
    {
      return ShopTransactionDatabase.AddDeliveryAddress(user_id, addr, phone_number, name, add_default);
    }

    public string Delete_Delivery_Address(string id)
    {
      return ShopTransactionDatabase.DeleteDeliveryAddress(id);
    }
  }
 
}
