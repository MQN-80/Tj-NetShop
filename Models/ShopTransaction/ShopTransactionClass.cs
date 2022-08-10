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
    public DateTime Ord_time { get; set; }
  }

}
