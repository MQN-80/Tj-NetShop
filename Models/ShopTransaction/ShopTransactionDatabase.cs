﻿using Newtonsoft.Json;
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

    /*
     * 建立数据库连接
     */
    public static void CreateConn()  //更改此处数据库地址即可
    {
      //124.222.1.19
      /*string user = "system";
      string pwd = "030215Zhan";
      string db = "localhost/orcl";
      string conStringUser = "User ID=" + user + ";password=" + pwd + ";Data Source=" + db + ";";
      */
      //string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl))); Persist Security Info=True;User ID=c##shop;Password=jinyi123mx427;";
      var connStr = $"DATA SOURCE=localhost/orcl; PASSWORD=030215Zhan; PERSIST SECURITY INFO=True; USER ID=system";
      DB = new OracleConnection(connStr);
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
    public static string AddDealRecord(string Product_id, string Ord_price, string UserID)
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
    public static string GetUserCreadits(string UserID)
    {
      List<User_credits> storage = new List<User_credits>();
      CreateConn();
      OracleCommand Search = DB.CreateCommand();

      Search.CommandText = "select Creadits from User_credits where User_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      while (Ord.Read())
      {
        User_credits user_Credits = new User_credits();
        user_Credits.Creadits = Convert.ToInt32(Ord.GetValue(0));
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
    public static string ModifyCreaditsRecord(string UserID, string Trade_id, int Creadits_change, string Status)
    {
      CreateConn();
      //先查用户积分数量
      OracleCommand Search = DB.CreateCommand();
      Search.CommandText = "select Creadits from User_credits where User_id=:UserID";
      Search.Parameters.Add(new OracleParameter(":UserID", UserID));
      OracleDataReader Ord = Search.ExecuteReader();
      int Creadits = 0;
      while (Ord.Read())
      {
        Creadits = Convert.ToInt32(Ord.GetValue(0));
      }

      //然后修改用户积分数量
      OracleCommand edit = DB.CreateCommand();
      string Create_time = DateTime.Now.ToString();
      
      if (Status == "1")
      {
        Creadits += Creadits_change;
      }
      else if (Status == "0")
      {
        //判断积分是否为负
        if ((Creadits - Creadits_change) < 0)
        {
          return "error";
        }
        else
        {
          Creadits -= Creadits_change;
        }
      }
      else
      { 
        return "error"; 
      }
      //修改用户积分表
      edit.CommandText = "update User_credits set Creadits=:Creadits where User_id=:UserID";
      edit.Parameters.Add(new OracleParameter(":Creadits", Creadits));
      edit.Parameters.Add(new OracleParameter(":UserID", UserID));
      edit.ExecuteNonQuery();
      //然后插入积分记录表
      CloseConn();
      return "ok";
    }
  }
}
