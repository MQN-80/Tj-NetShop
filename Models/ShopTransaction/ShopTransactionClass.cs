using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopTransaction
{
  /*
   * 3.1.10收货地址表
   */
  public class Delivery_address
  {
    public string Id { get; set; }
    public string User_id { get; set; }
    public string Addr { get; set; }
    public string Phone_number { get; set; }
    public string Name { get; set; }
    public string Add_default { get; set; }
  }
  /*
   * 3.1.9交易记录表
   */
  public class Deal_record
  {
    public string Id { get; set; }
    public string Trade_id { get; set; }
    public string Product_id { get; set; }
    public string Ord_price { get; set; }
    public string User_id { get; set; }
    public string Ord_payment { get; set; }
    public string Start_time { get; set; }
    public string End_time { get; set; }
    public string Status { get; set; }

  }
  /*
   * 3.1.30用户积分表
   */
  public class User_credits
  {
    public string User_id { get; set; }
    public string Creadits { get; set; }

  }
  /*
   * 3.1.31积分记录表
   */
  public class Creadits_record
  {
    public string User_id { get; set; }
    public string Trade_id { get; set; }
    public string Creadits_change { get; set; }
    public string Status { get; set; }
    public string Create_time { get; set; }
  }
}
