using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopTransaction
{
  public class ShopTransactionDatabase
  {
    public static OracleConnection DB;

    /*
     * 建立数据库连接
     */
    public static void CreateConn() //更改此处数据库地址即可
    {
      //124.222.1.19
      string user = "shop";
      string pwd = "jy2051914";
      string db = "124.222.1.19/helowin";
      string conStringUser = "User ID=" + user + ";password=" + pwd + ";Data Source=" + db + ";";

      //string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl))); Persist Security Info=True;User ID=c##shop;Password=jinyi123mx427;";
      //var connStr = $"DATA SOURCE=localhost/orcl; PASSWORD=030215Zhan; PERSIST SECURITY INFO=True; USER ID=system";
      DB = new OracleConnection(conStringUser);
      DB.Open();
    }

    /*
     * 关闭数据库连接
     */
    public static void CloseConn()
    {
      DB.Close();
    }

    /*
     * 返回收货地址
     */
    public static string GetDeliveryAddress(int UserID)
    {
      List<Delivery_address> storage = new List<Delivery_address>();
      CreateConn();
      OracleCommand Search = DB.CreateCommand();

      Search.CommandText = "select Addr,Phone_number,Name,Add_default from delivery_address where User_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      while (Ord.Read())
      {
        Delivery_address delivery_address = new Delivery_address();
        delivery_address.Addr = Ord.GetValue(0).ToString();
        delivery_address.Phone_number = Ord.GetValue(1).ToString();
        delivery_address.Name = Ord.GetValue(2).ToString();
        delivery_address.Add_default = Ord.GetValue(3).ToString();

        storage.Add(delivery_address);
      }

      //以字符串形式返回
      CloseConn();
      return JsonConvert.SerializeObject(storage);
    }

    /*
     * 创建订单信息
     * 添加成功返回订单id，添加失败返回“0”
     */
    public static string AddDealRecord(string Product_id, string Ord_price, int UserID)
    {
      CreateConn();
      OracleCommand Insert = DB.CreateCommand();
      string start_time = DateTime.Now.ToString();
      Insert.CommandText = "insert into deal_record (Trade_id,Product_id,Ord_price,User_id,start_time,status) " +
                           "values(deal_seq.nextval,:Product_id,:Ord_price,:User_id,:start_time,1)";
      Insert.Parameters.Add(new OracleParameter(":Product_id", Product_id));
      Insert.Parameters.Add(new OracleParameter(":Ord_price", Ord_price));
      Insert.Parameters.Add(new OracleParameter(":UserID", UserID));
      Insert.Parameters.Add(new OracleParameter(":start_time", start_time));
      Insert.ExecuteNonQuery();

      OracleCommand find = DB.CreateCommand();

      find.CommandText = "select id from deal_record where start_time=:start_time";
      find.Parameters.Add(new OracleParameter(":start_time", start_time));
      OracleDataReader Ord = find.ExecuteReader();
      string result = "0";
      while (Ord.Read())
      {
        result = Ord.GetValue(0).ToString();
      }

      CloseConn();
      return result;
    }

    /*
     * 返回订单信息
     */
    public static string GetDealRecord(int UserID)
    {
      List<Deal_record> storage = new List<Deal_record>();
      CreateConn();
      OracleCommand Search = DB.CreateCommand();

      Search.CommandText = "select Trade_id,Product_id,Ord_price,Start_time,Status " +
                           "from deal_record where User_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      while (Ord.Read())
      {
        Deal_record deal_record = new Deal_record();
        deal_record.Trade_id = Ord.GetValue(0).ToString();
        deal_record.Product_id = Ord.GetValue(1).ToString();
        deal_record.Ord_price = Ord.GetValue(2).ToString();
        deal_record.Start_time = Ord.GetValue(3).ToString();
        deal_record.Status = Ord.GetValue(4).ToString();

        storage.Add(deal_record);
      }

      //以字符串形式返回
      CloseConn();
      return JsonConvert.SerializeObject(storage);
    }

    /*
     * 修改订单信息
     */
    public static string ModifyDealRecord(string Trade_id)
    {
      CreateConn();
      //先修改status的状态,将其改为1
      OracleCommand edit = DB.CreateCommand();
      string End_time = DateTime.Now.ToString();
      edit.CommandText = "update deal_record set Status=1 where id=:Trade_id";
      edit.Parameters.Add(new OracleParameter(":Trade_id", Trade_id));
      edit.ExecuteNonQuery();
      CloseConn();
      return "ok";
    }

    /*
     * 拉取用户积分
     */
    public static string GetUserCredits(string UserID)
    {
      List<User_credits> storage = new List<User_credits>();
      CreateConn();
      OracleCommand Search = DB.CreateCommand();

      Search.CommandText = "select Credits from User_credits where User_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      while (Ord.Read())
      {
        User_credits user_Credits = new User_credits();
        user_Credits.Credits = Convert.ToInt32(Ord.GetValue(0));
        user_Credits.User_id = UserID;
        storage.Add(user_Credits);
      }

      //以字符串形式返回
      CloseConn();
      return JsonConvert.SerializeObject(storage);
    }

    /*
     *修改用户积分
     */
    public static string ModifyCreditsRecord(string UserID, string Trade_id, int Credits_change, string Status)
    {
      CreateConn();
      //先查用户积分数量
      OracleCommand Search = DB.CreateCommand();
      Search.CommandText = "select Credits from User_credits where User_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      int Credits = 0;
      while (Ord.Read())
      {
        Credits = Convert.ToInt32(Ord.GetValue(0));
      }

      //然后修改用户积分数量
      OracleCommand edit = DB.CreateCommand();
      string Create_time = DateTime.Now.ToString();

      if (Status == "1")
      {
        Credits += Credits_change;
      }
      else if (Status == "0")
      {
        //判断积分是否为负
        if ((Credits - Credits_change) < 0)
        {
          return "error";
        }
        else
        {
          Credits -= Credits_change;
        }
      }
      else
      {
        return "error";
      }

      //修改用户积分表
      edit.CommandText = "update User_credits set Credits=:Credits where User_id=:UserID";
      edit.Parameters.Add(new OracleParameter(":Credits", Credits));
      edit.Parameters.Add(new OracleParameter(":UserID", UserID));
      edit.ExecuteNonQuery();
      //然后插入积分记录表
      OracleCommand Insert = DB.CreateCommand();
      Insert.CommandText = "insert into credits_record (user_id,trade_id,credits_change,status,create_time) " +
                           "values(:User_id,:Trade_id,:Credits_change,:Status,:Create_time)";
      Insert.Parameters.Add(new OracleParameter(":User_id", UserID));
      Insert.Parameters.Add(new OracleParameter(":Trade_id", Trade_id));
      Insert.Parameters.Add(new OracleParameter(":Credits_change", Credits_change));
      Insert.Parameters.Add(new OracleParameter(":Status", Status));
      Insert.Parameters.Add(new OracleParameter(":Create_time", Create_time));
      Insert.ExecuteNonQuery();


      CloseConn();
      return "ok";
    }


    /*
     *交易
     */
    public static string GoodsTransaction(string Consumer_UserID, string Business_UserID,
      int Credits_change, string Status)
    {
      CreateConn();
      //先查用户积分数量
      OracleCommand SearchConsumer = DB.CreateCommand();
      SearchConsumer.CommandText = "select Credits from User_credits where User_id=:Consumer_UserID";
      SearchConsumer.Parameters.Add(new OracleParameter(":Consumer_UserID", Consumer_UserID));
      OracleDataReader OrdConsumer = SearchConsumer.ExecuteReader();
      int Consumer_Credits = 0;
      while (OrdConsumer.Read())
      {
        Consumer_Credits = Convert.ToInt32(OrdConsumer.GetValue(0));
      }

      //然后查商家积分数量
      OracleCommand SearchBusiness = DB.CreateCommand();
      SearchBusiness.CommandText = "select Credits from User_credits where User_id=:Business_UserID";
      SearchBusiness.Parameters.Add(new OracleParameter(":Business_UserID", Business_UserID));
      OracleDataReader OrdBusiness = SearchBusiness.ExecuteReader();
      int Business_Credits = 0;
      while (OrdBusiness.Read())
      {
        Business_Credits = Convert.ToInt32(OrdBusiness.GetValue(0));
      }

      string Consumer_Status = "";
      string Business_Status = "";

      OracleCommand editConsumer = DB.CreateCommand();
      OracleCommand editBusiness = DB.CreateCommand();
      if (Status == "1") //“1”为商品正常交易，用户扣除积分，商家增加积分
      {
        Consumer_Status = "0";
        Business_Status = "1";
        //检查用户扣除积分后是否小于零
        if ((Consumer_Credits - Credits_change) < 0)
        {
          return "error";
        }
        else
        {
          Consumer_Credits -= Credits_change;
          Business_Credits += Credits_change;
        }
      }
      else if (Status == "0") //“0”为商品退款，用户增加积分，商家扣除积分
      {
        Consumer_Status = "1";
        Business_Status = "0";
        //检查商家扣除积分后是否小于零
        if ((Business_Credits - Credits_change) < 0)
        {
          return "error";
        }
        else
        {
          Consumer_Credits += Credits_change;
          Business_Credits -= Credits_change;
        }
      }
      else
      {
        return "error";
      }

      string Create_time = DateTime.Now.ToString();
      OracleCommand InsertConsumer = DB.CreateCommand();
      OracleCommand InsertBusiness = DB.CreateCommand();
      //开始一个事务
      OracleTransaction txn = DB.BeginTransaction(IsolationLevel.ReadCommitted);
      try
      {
        //更新User_credits表
        editConsumer.CommandText = "update User_credits set Credits=:Consumer_Credits where User_id=:Consumer_UserID";
        editConsumer.Parameters.Add(new OracleParameter(":Consumer_Credits", Consumer_Credits));
        editConsumer.Parameters.Add(new OracleParameter(":Consumer_UserID", Consumer_UserID));
        editBusiness.CommandText = "update User_credits set Credits=:Business_Credits where User_id=:Business_UserID";
        editBusiness.Parameters.Add(new OracleParameter(":Business_Credits", Business_Credits));
        editBusiness.Parameters.Add(new OracleParameter(":Business_UserID", Business_UserID));

        //插入credits_record表
        InsertConsumer.CommandText =
          "insert into credits_record (user_id,trade_id,credits_change,status,create_time) " +
          "values(:Consumer_UserID,deal_seq.nextval,:Credits_change,:Consumer_Status,:Create_time)";
        InsertConsumer.Parameters.Add(new OracleParameter(":Consumer_UserID", Consumer_UserID));
        InsertConsumer.Parameters.Add(new OracleParameter(":Credits_change", Credits_change));
        InsertConsumer.Parameters.Add(new OracleParameter(":Consumer_Status", Consumer_Status));
        InsertConsumer.Parameters.Add(new OracleParameter(":Create_time", Create_time));
        InsertBusiness.CommandText =
          "insert into credits_record (user_id,trade_id,credits_change,status,create_time) " +
          "values(:Business_UserID,deal_seq.nextval,:Credits_change,:Business_Status,:Create_time)";
        InsertBusiness.Parameters.Add(new OracleParameter(":Business_UserID", Business_UserID));
        InsertBusiness.Parameters.Add(new OracleParameter(":Credits_change", Credits_change));
        InsertBusiness.Parameters.Add(new OracleParameter(":Business_Status", Business_Status));
        InsertBusiness.Parameters.Add(new OracleParameter(":Create_time", Create_time));

        editConsumer.ExecuteNonQuery();
        editBusiness.ExecuteNonQuery();
        InsertConsumer.ExecuteNonQuery();
        InsertBusiness.ExecuteNonQuery();

        txn.Commit();
      }
      catch (Exception e)
      {
        // 打印错误信息
        Console.WriteLine("e.Message = " + e.Message);
        // 回滚事务
        txn.Rollback();
      }

      //释放事务的资源
      txn.Dispose();

      CloseConn();
      return "ok";
    }

    /*
     *返回积分记录
     *From lyp, 前端说要一个返回积分记录的接口，我就直接在里面写了
     */
    public static string GetCreditRecord(string userID)
    {
      var storage = new List<Credits_record>();
      CreateConn();
      var find = DB.CreateCommand();

      try
      {
        find.CommandText = "select id,trade_id,credits_change,status,create_time from credits_record where user_id=:userID";
        find.Parameters.Add(new OracleParameter(":UserID", userID));
        var ord = find.ExecuteReader();

        while (ord.Read())
        {
          var creditRecord = new Credits_record();

          creditRecord.id = ord.GetValue(0).ToString();
          creditRecord.Trade_id = ord.GetValue(1).ToString();
          try
          {
            creditRecord.Credits_change = int.Parse(ord.GetValue(2).ToString());
          }
          catch (Exception e)
          {
            Console.WriteLine("Error: " + e.Message + "\n in reading credit recod");
            throw e;
          }

          creditRecord.Status = ord.GetValue(3).ToString();
          creditRecord.Create_time = ord.GetValue(4).ToString();

          storage.Add(creditRecord);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: " + e.Message + "\n in reading credit recod");
        throw e;
      }

      CloseConn();
      return JsonConvert.SerializeObject(storage);
    }

    /*
     *查找商品
     */
    public static string SearchProductInfo(string product_name)
    {
      List<Product_information> storage = new List<Product_information>();
      CreateConn();
      var Search = DB.CreateCommand();
      try
      {
        Search.CommandText = "select id,name,img,type_id,product_id,des,surplus,status,price,create_time " +
          "from product_information " +
          "where name like CONCAT(CONCAT('%', :product_name),'%') ";
        Search.Parameters.Add(new OracleParameter(":product_name", product_name));
        OracleDataReader Ord = Search.ExecuteReader();
        while (Ord.Read())
        {
          Product_information product_information = new Product_information();
          product_information.id = Ord.GetValue(0).ToString();
          product_information.name = Ord.GetValue(1).ToString();
          product_information.img = Ord.GetValue(2).ToString();
          product_information.type_id = Ord.GetValue(3).ToString();
          product_information.product_id = Convert.ToInt32(Ord.GetValue(4).ToString());
          product_information.des = Ord.GetValue(5).ToString();
          product_information.surplus = Convert.ToInt32(Ord.GetValue(6).ToString());
          product_information.status = Convert.ToInt32(Ord.GetValue(7).ToString());
          product_information.price = Convert.ToInt32(Ord.GetValue(8).ToString());
          product_information.create_time = Ord.GetValue(9).ToString();
          storage.Add(product_information);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Error: " + e.Message + "\n in SearchProductInfo");
        throw e;
      }
      //以字符串形式返回
      CloseConn();
      return JsonConvert.SerializeObject(storage);
    }

    /*
     *用户收藏夹连表查询
     */
    public static string SearchUserCollect(int UserID)
    {
      List<User_collect> Storage = new List<User_collect>();
      CreateConn();
      var Search = DB.CreateCommand();
      Search.CommandText = "select a.name,a.id,b.create_time,a.price,b.product_price " +
          "from product_information a,product_collect b " +
          "where a.product_id=b.product_id and b.user_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      while (Ord.Read())
      {
        User_collect user_collect = new User_collect();
        user_collect.Name = Ord.GetValue(0).ToString();
        user_collect.id = Ord.GetValue(1).ToString();
        user_collect.create_time = Ord.GetValue(2).ToString();
        user_collect.nowPrice = Ord.GetValue(3).ToString();
        user_collect.collectPrice = Ord.GetValue(4).ToString();
        Storage.Add(user_collect);
      }

      //以字符串形式返回
      CloseConn();
      return JsonConvert.SerializeObject(Storage);
    }

    /*
     *用户收藏店铺连表查询
     */
    public static string SearchUserCollectShop(int UserID)
    {
      List<User_collectShop> Storage = new List<User_collectShop>();
      CreateConn();
      var Search = DB.CreateCommand();
      Search.CommandText = "select b.shop_id,b.collect_time," +
                "(select user_name from user_info where id=b.shop_id)" +
          "from user_info a,subscribe_shop b " +
          "where a.user_id=:UserID and b.user_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      while (Ord.Read())
      {
        User_collectShop user_collectShop = new User_collectShop();
        user_collectShop.shop_id = Ord.GetValue(0).ToString();
        user_collectShop.collect_time = Ord.GetValue(1).ToString();
        user_collectShop.name = Ord.GetValue(2).ToString();
        user_collectShop.img = "http://106.12.131.109:8083/avator/" + user_collectShop.shop_id + ".jpg";
        Storage.Add(user_collectShop);
      }

      //以字符串形式返回
      CloseConn();
      return JsonConvert.SerializeObject(Storage);
    }

    /*
     * 添加收货地址
     */
    public static string AddDeliveryAddress(int user_id, string addr, string phone_number, string name, int add_default)
    {
      CreateConn();
      var Insert = DB.CreateCommand();
      Insert.CommandText = "insert into delivery_address (user_id,addr,phone_number,name,add_default) " +
                           "values(:user_id,:addr,:phone_number,:name,:add_default)";
      Insert.Parameters.Add(new OracleParameter(":user_id", user_id));
      Insert.Parameters.Add(new OracleParameter(":addr", addr));
      Insert.Parameters.Add(new OracleParameter(":phone_number", phone_number));
      Insert.Parameters.Add(new OracleParameter(":name", name));
      Insert.Parameters.Add(new OracleParameter(":add_default", add_default));
      Insert.ExecuteNonQuery();

      return "OK";
    }

    /*
     * 删除收货地址
     */
    public static string DeleteDeliveryAddress(string id)
    {
      CreateConn();
      var Delete = DB.CreateCommand();
      Delete.CommandText = "delete from delivery_address where id = :id";
      Delete.Parameters.Add(new OracleParameter(":id", id));
      Delete.ExecuteNonQuery();

      return "OK";
    }

    /*
    * 修改收货地址
    */
    public static string EditDeliveryAddress(string id, string addr, string phone_number, string name, int add_default)
    {
      CreateConn();
      var edit = DB.CreateCommand();
      //修改用户收货地址
      edit.CommandText = "update delivery_address set addr=:addr,phone_number=:phone_number,name=:name,add_default=:add_default where id=:id";
      edit.Parameters.Add(new OracleParameter(":addr", addr));
      edit.Parameters.Add(new OracleParameter(":phone_number", phone_number));
      edit.Parameters.Add(new OracleParameter(":name", name));
      edit.Parameters.Add(new OracleParameter(":add_default", add_default));
      edit.Parameters.Add(new OracleParameter(":id", id));
      edit.ExecuteNonQuery();
      return "OK";
    }

  }
}
