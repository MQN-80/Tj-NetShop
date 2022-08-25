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
    public string Trade_id { get; set; }
    public string Product_id { get; set; }
    public string Ord_price { get; set; }
    public string Start_time { get; set; }
    public string Status { get; set; }

  }
  /*
   * 3.1.30用户积分表
   */
  public class User_credits
  {
    public string User_id { get; set; }
    public int Credits { get; set; }

  }
  /*
   * 3.1.31积分记录表
   */
  public class Credits_record
  {
    public string id { get; set; }
    public string User_id { get; set; }
    public string Trade_id { get; set; }
    public int Credits_change { get; set; }
    public string Status { get; set; }
    public string Create_time { get; set; }
  }

  /*
   * 3.1.2商品信息表
   */
  public class Product_information
  {
    public string id { get; set; }
    public string name { get; set; }
    public string img { get; set; }
    public string type_id { get; set; }
    public int product_id { get; set; }
    public string des { get; set; }
    public int surplus { get; set; }
    public int status { get; set; }
    public int price { get; set; }
    public string create_time { get; set; }
  }

  /*
  * 用户收藏夹连表查询返回值
  */
  public class User_collect
  {
    public string Name { get; set; }//商品名称
    public string Img { get; set; }//商品图片
    public string Des { get; set; }//商品简介
    public int Price { get; set; }//商品价格
  }

  /*
 * 用户收藏店铺连表查询返回值
 */
  public class User_collectShop
  {
    public string Store_name { get; set; }//店铺名称
    public string Store_img { get; set; }//商品图片
    public string Collet_time { get; set; }//商品简介
  }

}
