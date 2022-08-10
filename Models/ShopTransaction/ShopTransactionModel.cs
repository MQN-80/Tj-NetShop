using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopTransaction
{
  public class ShopTransactionModel
  {
    public string Get_delivery_address()
    {
      return ShopTransactionDatabase.GetDeliveryAddress();
    }
  }
 
}
