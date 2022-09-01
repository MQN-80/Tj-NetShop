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
    public string product_id { get; set; }
    public string des { get; set; }
    public string surplus { get; set; }
    public string status { get; set; }
    public string price { get; set; }
    public string create_time { get; set; }
    public string discount { get; set; }
  }

  /*
  * 用户收藏夹连表查询返回值
  */
  public class User_collect
  {
    public string Name { get; set; }//商品名称
    public string id { get; set; }//商品id
    public string create_time { get; set; }//商品收藏时间
    public string collectPrice { get; set; }//商品收藏价格
    public string nowPrice { get; set; }//商品现在价格
    }

  /*
 * 用户收藏店铺连表查询返回值
 */
  public class User_collectShop
  {
    public string shop_id { get; set; }//店铺id
    public string collect_time { get; set; }//店铺收藏时间
    public string name { get; set; }//用户名称
    public string img { get; set; }
  }

  /*
   * 查询商户id和姓名返回值
   */
  public class Goods_UserInfo
  {
    public string User_id{ get; set; }
    public string User_name { get; set; }
  }

  /*
   *返回购物车
   */
  public class User_Trolley
  {
    public string Img { get; set; }
    public int Price { get; set; }
    public string Name { get; set; }
    public string Product_num { get; set; }
  }

}
