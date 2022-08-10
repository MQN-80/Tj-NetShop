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

  }
}
