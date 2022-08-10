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
    [Route("/ShopTransaction/get_delivery_address/{UserID}")]
    [HttpGet]
    public string get_delivery_address(string UserID)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Get_delivery_address(UserID);
    }
    /*
     * 创建新的订单
     */
    [Route("/ShopTransaction/add_deal_record")]
    [HttpPost]
    public string add_deal_record(string Trade_id, string Product_id, string Ord_price, string UserID, string Ord_payment)
    {
      ShopTransactionModel shopTransactionModel = new ShopTransactionModel();
      return shopTransactionModel.Add_deal_record(Trade_id, Product_id, Ord_price, UserID, Ord_payment);
    }
  }
}
