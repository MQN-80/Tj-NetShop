using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApi.Models
{
    public class ManageDatabase
    {
        public static OracleConnection DB;

    //建立数据库连接
    public static void CreateConn()  //更改此处数据库地址即可
    {
      //124.222.1.19
      string user = "shop";
      string pwd = "jy2051914";
      string db = "124.222.1.19/helowin";
      string conStringUser = "User ID=" + user + ";password=" + pwd + ";Data Source=" + db + ";";
      //string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl))); Persist Security Info=True;User ID=c##shop;Password=jinyi123mx427;";
      DB = new OracleConnection(conStringUser);
      DB.Open();
    }

    //关闭数据库连接
    public static void CloseConn()
    {
      DB.Close();
    }

    //与功能点1：登录与注册相关的操作

        //在MUser表中查询用户、密码是否错误(登录时使用)
        //密码或用户名错误返回false；密码和用户名正确返回true
        public static bool IsUserExist(int UserID, string Password)
        {
            int Count;
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select count(*) from user_info where user_id=:UserID and password=:Password";
            CMD.Parameters.Add(new OracleParameter(":UserID", UserID));
            CMD.Parameters.Add(new OracleParameter(":Password", Password));
            Count = Convert.ToInt32(CMD.ExecuteScalar());
            CloseConn();
            if (Count == 0)
                return false;
            else
                return true;

    }

        //向MUser表中增加一个新用户(注册)
        //添加成功返回UserID，添加失败返回“0”
        public static user_result AddUser(string UserName, string UserPassword)
        {
            string now = DateTime.Now.ToString();   //获取当前时间
            //假如存在该用户名,返回空
            if (FindUserInfo(UserName))
                return null;
            CreateConn();
            OracleCommand Insert = DB.CreateCommand();

            Insert.CommandText = "insert into user_info (user_name,password,create_time) values(:UserName,:UserPassword,:NowTime)";
            Insert.Parameters.Add(new OracleParameter(":UserName", UserName));
            Insert.Parameters.Add(new OracleParameter(":UserPassword", UserPassword));
            Insert.Parameters.Add(new OracleParameter(":NowTime", now));
            Insert.ExecuteNonQuery();        
            OracleCommand find = DB.CreateCommand();
            find.CommandText = "select id,user_id from user_info where user_name=:UserName";
            find.Parameters.Add(new OracleParameter(":UserName", UserName));
            OracleDataReader Ord = find.ExecuteReader();
            user_result result = new user_result();
            while (Ord.Read())
            {
                result.id = Ord.GetValue(0).ToString();
                result.user_id = Ord.GetValue(1).ToString();
            }
            OracleCommand credit = DB.CreateCommand();
            credit.CommandText = "insert into user_credits (user_id,credits)" +
                "values ((select id from user_info where user_name=:username),0)";
            credit.Parameters.Add(new OracleParameter(":username", UserName));
            credit.ExecuteNonQuery();
            CloseConn();
            return result;
        }
        public static user_result get_user(int user_id)
        {
            CreateConn();
            OracleCommand find = DB.CreateCommand();

            find.CommandText = "select id,user_id,user_name from user_info where user_id=:user_id";
            find.Parameters.Add(new OracleParameter(":user_id", user_id));
            OracleDataReader Ord = find.ExecuteReader();
            user_result result = new user_result();
            while (Ord.Read())
            {
                result.id = Ord.GetValue(0).ToString();
                result.user_id = Ord.GetValue(1).ToString();
                result.user_name = Ord.GetValue(2).ToString();
            }
            CloseConn();
            return result;
        }
        //查找该用户名是否存在
        public static bool FindUserInfo(string user_name)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select count(*) from user_info where user_name=:user_name";
            Search.Parameters.Add(new OracleParameter(":user_name", user_name));
            int count = Convert.ToInt32(Search.ExecuteScalar());
            CloseConn();
            if (count > 0)
                return true;
            else
                return false;
        }
    }

}
