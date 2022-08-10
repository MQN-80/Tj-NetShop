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
  }
 
}
