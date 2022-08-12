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

    public string Add_deal_record(string Trade_id, string Product_id, string Ord_price, string UserID, string Ord_payment)
    {
      return ShopTransactionDatabase.AddDealRecord(Trade_id, Product_id, Ord_price, UserID, Ord_payment);
    }

    public string Get_deal_record(string UserID)
    {
      return ShopTransactionDatabase.GetDealRecord(UserID);
    }
  }
 
}
