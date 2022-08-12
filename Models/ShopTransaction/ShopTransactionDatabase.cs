using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopTransaction
{
  public class ShopTransactionDatabase
  {
    public static OracleConnection DB;

    //建立数据库连接
    public static void CreateConn()  //更改此处数据库地址即可
    {
      //124.222.1.19
      /*string user = "system";
      string pwd = "030215Zhan";
      string db = "localhost/orcl";
      string conStringUser = "User ID=" + user + ";password=" + pwd + ";Data Source=" + db + ";";*/
      //string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl))); Persist Security Info=True;User ID=c##shop;Password=jinyi123mx427;";
      var connStr = $"DATA SOURCE=localhost/orcl; PASSWORD=030215Zhan; PERSIST SECURITY INFO=True; USER ID=system";
      DB = new OracleConnection(connStr);
      DB.Open();
    }
    //关闭数据库连接
    public static void CloseConn()
    {
      DB.Close();
    }

    //返回收货地址
    public static string GetDeliveryAddress(string UserID)
    {
      List<Delivery_address> storage = new List<Delivery_address>();
      CreateConn();
      OracleCommand Search = DB.CreateCommand();

      Search.CommandText = "select Id,User_id,Addr,Phone_number,Name,Add_default from delivery_address where User_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      while (Ord.Read())
      {
        Delivery_address delivery_address = new Delivery_address();
        delivery_address.Id = Ord.GetValue(0).ToString();
        delivery_address.User_id = Ord.GetValue(1).ToString();
        delivery_address.Addr = Ord.GetValue(2).ToString();
        delivery_address.Phone_number = Ord.GetValue(3).ToString();
        delivery_address.Name = Ord.GetValue(4).ToString();
        delivery_address.Add_default = Ord.GetValue(5).ToString();

        storage.Add(delivery_address);
      }
      //以字符串形式返回
      CloseConn();
      return JsonConvert.SerializeObject(storage);
    }

    //创建订单信息
    //添加成功返回UserID，添加失败返回“0”
    public static string AddDealRecord(string Trade_id, string Product_id, string Ord_price, string UserID, string Ord_payment)
    {
      CreateConn();
      OracleCommand Insert = DB.CreateCommand();
      string start_time = DateTime.Now.ToString();
      string status = "0";
      Insert.CommandText = "insert into deal_record (Trade_id,Product_id,Ord_price,User_id,Ord_payment,start_time,status) " +
        "values(:Trade_id,:Product_id,:Ord_price,:User_id,:Ord_payment,:start_time,:status)";
      Insert.Parameters.Add(new OracleParameter(":Trade_id", Trade_id));
      Insert.Parameters.Add(new OracleParameter(":Product_id", Product_id));
      Insert.Parameters.Add(new OracleParameter(":Ord_price", Ord_price));
      Insert.Parameters.Add(new OracleParameter(":UserID", UserID));
      Insert.Parameters.Add(new OracleParameter(":Ord_payment", Ord_payment));
      Insert.Parameters.Add(new OracleParameter(":start_time", start_time));
      Insert.Parameters.Add(new OracleParameter(":status", status));
      Insert.ExecuteNonQuery();

      OracleCommand find = DB.CreateCommand();

      find.CommandText = "select User_id from deal_record where start_time=:start_time";
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
    //返回订单信息
    public static string GetDealRecord(string UserID)
    {
      List<Deal_record> storage = new List<Deal_record>();
      CreateConn();
      OracleCommand Search = DB.CreateCommand();

      Search.CommandText = "select Trade_id,Product_id,Ord_price,Ord_payment,Start_time,End_time,Status from deal_record where User_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      while (Ord.Read())
      {
        Deal_record deal_record = new Deal_record();
        deal_record.Trade_id = Ord.GetValue(0).ToString();
        deal_record.Product_id = Ord.GetValue(1).ToString();
        deal_record.Ord_price = Ord.GetValue(2).ToString();
        deal_record.Ord_payment = Ord.GetValue(3).ToString();
        deal_record.Start_time = Ord.GetValue(4).ToString();
        deal_record.End_time = Ord.GetValue(5).ToString();
        deal_record.Status = Ord.GetValue(6).ToString();

        storage.Add(deal_record);
      }
      //以字符串形式返回
      CloseConn();
      return JsonConvert.SerializeObject(storage);
    }
  }
}
