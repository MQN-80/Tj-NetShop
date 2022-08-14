using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopTransaction
{
  public class ShopTransactionModel
  {
    public string Get_delivery_address(string UserID)
    {
      return ShopTransactionDatabase.GetDeliveryAddress(UserID);
    }

    public string Add_deal_record(string Trade_id, string Product_id, string Ord_price, string UserID)
    {
      return ShopTransactionDatabase.AddDealRecord(Trade_id, Product_id, Ord_price, UserID);
    }

    public string Get_deal_record(string UserID)
    {
      return ShopTransactionDatabase.GetDealRecord(UserID);
    }

    public string Modify_deal_record(string UserID,string Trade_id,string Ord_payment)
    {
      return ShopTransactionDatabase.ModifyDealRecord(UserID, Trade_id, Ord_payment);
    }

    public string Get_User_Creadits(string UserID)
    {
      return ShopTransactionDatabase.GetUserCreadits(UserID);
    }

    public string Modify_Creadits_Record(string UserID, string Trade_id, int Creadits_change, string Status)
    {
      return ShopTransactionDatabase.ModifyCreaditsRecord(UserID, Trade_id, Creadits_change, Status);
    }
  }
 
}
