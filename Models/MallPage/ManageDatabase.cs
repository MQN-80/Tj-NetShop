using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.MallPage;

namespace WebApi.Models
{
    public class Database
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

        //查询该管理员是否存在,管理员的role_rank为3
        //测试账号jy,34567,id为2004168
        public static bool IsUserExist(int UserID, string Password)
        {
            int Count;
            CreateConn();
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select count(*) from user_info where user_id=:UserID and password=:Password and role_rank=3";
            CMD.Parameters.Add(new OracleParameter(":UserID", UserID));
            CMD.Parameters.Add(new OracleParameter(":Password", Password));
            Count = Convert.ToInt32(CMD.ExecuteScalar());
            CloseConn();
            if (Count == 0)
                return false;
            else
                return true;

        }
        //获取用户列表，每次返回20个,num从0开始
        public static string getUserList(int num)
        
        {
            List<user_info> storage = new List<user_info>();
            CreateConn();
            user_info mid = new user_info();  //用于存储中间量
            OracleCommand find = DB.CreateCommand();
            int begin = 2001042 + 521 * num * 20;
            int end = begin + 521 * 19;
            find.CommandText="select user_id,user_name,create_time from user_info where user_id>=:begin and user_id <= :end";
            find.Parameters.Add(new OracleParameter(":begin", begin));
            find.Parameters.Add(new OracleParameter(":end", end));
            OracleDataReader Ord = find.ExecuteReader();
            while(Ord.Read())
            {
                mid.user_id = Ord.GetValue(0).ToString();
                mid.user_name = Ord.GetValue(1).ToString();
                mid.create_time = Ord.GetValue(2).ToString();
                storage.Add(mid);
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);

        }
        //获取等待审核的商品,同样采取分批请求的方式,sum从0开始
        public static string getProduct(int sum)
        {
            List<product_info> storage = new List<product_info>();
            CreateConn();
            OracleCommand find = DB.CreateCommand();
            //首先获取待处理的总数
            find.CommandText = "select count(*) from manage_product";
            int count = Convert.ToInt32(find.ExecuteScalar());
            OracleCommand get_list = DB.CreateCommand();
            //获取当前请求范围
            int begin = sum* 20+1;
            int end = (sum + 1) * 20;
            if (begin > count)
                return JsonConvert.SerializeObject("请求过大");
            //防止请求超过范围
            if (end > count)
                end = count;
            get_list.CommandText = "select a.name,a.img,a.type_id,a.product_id,a.des,a.price,a.create_time from product_information a join manage_product  b on a.product_id=b.product_id  where rownum>=:begin and rownum<=:end order by a.product_id";
            get_list.Parameters.Add(new OracleParameter(":begin", begin)); 
            get_list.Parameters.Add(new OracleParameter(":end", end));
            OracleDataReader Ord = get_list.ExecuteReader();
            product_info mid = new product_info();
            while (Ord.Read())
            {
                mid.name = Ord.GetValue(0).ToString();
                mid.img = Ord.GetValue(1).ToString();
                mid.type_id = Ord.GetValue(2).ToString();
                mid.product_id = Ord.GetValue(3).ToString();
                mid.des = Ord.GetValue(4).ToString();
                mid.price = (long)Ord.GetValue(5);
                mid.create_time= Ord.GetValue(6).ToString();
                storage.Add(mid);
            }
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        //向MUser表中增加一个新用户(注册)
        //添加成功返回UserID，添加失败返回“0”
        public static string AddUser(string UserName, string UserPassword)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into user_info (user_name,password) values(:UserName,:UserPassword)";
            Insert.Parameters.Add(new OracleParameter(":UserName", UserName));
            Insert.Parameters.Add(new OracleParameter(":UserPassword", UserPassword));
            Insert.ExecuteNonQuery();

            OracleCommand find = DB.CreateCommand();

            find.CommandText = "select user_id from user_info where user_name=:UserName";
            find.Parameters.Add(new OracleParameter(":UserName", UserName));
            OracleDataReader Ord = find.ExecuteReader();
            string result = "0";
            while (Ord.Read())
            {
                result = Ord.GetValue(0).ToString();
            }
            CloseConn();
            return result;
        }

        //查找个人信息
        public static User FindUserInfo(string UserID)
        {
            User U = new User();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from user_info where user_id=:UserID";
            Search.Parameters.Add(new OracleParameter(":UserID", UserID));
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                U.UserID = UserID;
                U.UserPassword = Ord.GetValue(1).ToString();
            }
            return U;
        }
    }

}