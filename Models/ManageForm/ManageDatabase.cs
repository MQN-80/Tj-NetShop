using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.ManageForm;

namespace WebApi.Models
{
    public class Database
    {
        public  OracleConnection DB;

        //建立数据库连接
        public  void CreateConn()  //更改此处数据库地址即可
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
        public  void CloseConn()
        {
            DB.Close();
        }

        //查询该管理员是否存在,管理员的role_rank为3
        //测试账号jy,34567,id为2004168
        public  bool IsUserExist(int UserID, string Password)
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
        public  string getUserList(int num)
        
        {
            List<user_info> storage = new List<user_info>();
            CreateConn();
            OracleCommand find = DB.CreateCommand();
            int begin = 2001042 + 521 * num * 20;
            int end = begin + 521 * 19;
            find.CommandText="select user_id,user_name,create_time from user_info where user_id>=:begin and user_id <= :end";
            find.Parameters.Add(new OracleParameter(":begin", begin));
            find.Parameters.Add(new OracleParameter(":end", end));
            OracleDataReader Ord = find.ExecuteReader();
            while (Ord.Read())
            {
                user_info mid = new user_info();
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
        public  string getProduct(int sum)
        {
            List<product_info> storage = new List<product_info>();
            CreateConn();
            OracleCommand find = DB.CreateCommand();
            //首先获取待处理的总数
            find.CommandText = "select count(*) from product_information where status=0";
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
            get_list.CommandText = "select name,type_id,product_id,des,price,create_time  from product_information where status=0 and rownum>=:begin and rownum<=:end order by product_id";
            get_list.Parameters.Add(new OracleParameter(":begin", begin)); 
            get_list.Parameters.Add(new OracleParameter(":end", end));
            OracleDataReader Ord = get_list.ExecuteReader();
            while (Ord.Read())
            {
                product_info mid = new product_info();
                mid.name = Ord.GetValue(0).ToString();               
                mid.type_id = Ord.GetValue(1).ToString();
                mid.product_id = Ord.GetValue(2).ToString();
                mid.des = Ord.GetValue(3).ToString();
                mid.price = Ord.GetValue(4).ToString();
                mid.create_time= Ord.GetValue(5).ToString();
                storage.Add(mid);
            }
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        //商品审核通过
        public  string agreeProduct(int product_id,int status)
        {
            CreateConn();
            //0代表删除
            if (status == 0)
            {
                OracleCommand delete1 = DB.CreateCommand();
                delete1.CommandText = "delete from shop_product where product_id=:product_id";
                delete1.Parameters.Add(new OracleParameter(":product_id", product_id));
                delete1.ExecuteNonQuery();
                OracleCommand delete2 = DB.CreateCommand();
                delete2.CommandText = "delete from product_information where product_id=:product_id";
                delete2.Parameters.Add(new OracleParameter(":product_id", product_id));
                delete2.ExecuteNonQuery();
            }
            else
            {

                //先修改status的状态,将其改为1
                OracleCommand edit = DB.CreateCommand();
                edit.CommandText = "update shop_product set status=:status where product_id=:product_id";
                edit.Parameters.Add(new OracleParameter(":status", status));
                edit.Parameters.Add(new OracleParameter(":product_id", product_id));
                //commit();
                edit.ExecuteNonQuery();
                OracleCommand update = DB.CreateCommand();
                update.CommandText = "update product_information set status=:status where product_id=:product_id";
                
                update.Parameters.Add(new OracleParameter(":status", status));
                update.Parameters.Add(new OracleParameter(":product_id", product_id));
                update.ExecuteNonQuery();
            }
            CloseConn();
            return "ok";
        }   

        //提交前执行一下commit,防止update上锁,目前已设置自动提交
        public  void commit()
        {
            OracleCommand commit = DB.CreateCommand();
            commit.CommandText = "commit";
            commit.ExecuteNonQuery();
        }
        //请求待审核文章,每次返回20个
        public  string get_article(int sum)
        {
            
            List<article> storage = new List<article>();
            CreateConn();
            
            OracleCommand find = DB.CreateCommand();
            //首先获取待处理的总数
            find.CommandText = "select count(*) from article where status=0";
            int count = Convert.ToInt32(find.ExecuteScalar());
            OracleCommand get_list = DB.CreateCommand();
            //获取当前请求范围
            int begin = sum * 20 + 1;
            int end = (sum + 1) * 20;
            if (begin > count)
                return JsonConvert.SerializeObject("请求过大");
            //防止请求超过范围
            if (end > count)
                end = count;
            get_list.CommandText = "select article_title,article_context,user_id,create_time,id from article where rownum>=:begin and rownum<=:end and status=0";
            get_list.Parameters.Add(new OracleParameter(":begin", begin));
            get_list.Parameters.Add(new OracleParameter(":end", end));
            OracleDataReader Ord = get_list.ExecuteReader();
            while (Ord.Read())
            {
                article mid = new article();
                
                mid.article_title = Ord.GetValue(0).ToString();
                mid.article_context = Ord.GetValue(1).ToString();
                mid.user_id = Ord.GetValue(2).ToString();
                mid.create_time = Ord.GetValue(3).ToString();
                mid.id = Ord.GetValue(4).ToString();
                storage.Add(mid);
            }
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }

        //审核文章通过
        public  string agree_article(string id,int option)
        {
            CreateConn();
            //先修改status的状态,将其改为1
            OracleCommand edit = DB.CreateCommand();
            if (option == 1)  //表示审核通过
            {
                edit.CommandText = "update article set status=1 where id=:id";
                edit.Parameters.Add(new OracleParameter(":id", id));
            }
            else
            {
                edit.CommandText = "delete from article where id=:id";
                edit.Parameters.Add(new OracleParameter(":id", id));
            }
            //commit();
            int m=edit.ExecuteNonQuery();
            CloseConn();
            return m.ToString();
        }
        //获取评论
        public  string getComment(int sum)
        {
            List<comment> storage = new List<comment>();
            CreateConn();

            OracleCommand find = DB.CreateCommand();
            //首先获取待处理的总数
            find.CommandText = "select count(*) from article_comment where status=0";
            int count = Convert.ToInt32(find.ExecuteScalar());
            OracleCommand get_list = DB.CreateCommand();
            //获取当前请求范围
            int begin = sum * 20 + 1;
            int end = (sum + 1) * 20;
            if (begin > count)
                return JsonConvert.SerializeObject("请求过大");
            //防止请求超过范围
            if (end > count)
                end = count;
            get_list.CommandText = "select comment_context,create_time,id from article_comment where rownum>=:begin and rownum<=:end and status=0";
            get_list.Parameters.Add(new OracleParameter(":begin", begin));
            get_list.Parameters.Add(new OracleParameter(":end", end));
            OracleDataReader Ord = get_list.ExecuteReader();
            while (Ord.Read())
            {
                comment mid = new comment();
                mid.comment_context = Ord.GetValue(0).ToString();
                mid.create_time = Ord.GetValue(1).ToString();
                mid.id = Ord.GetValue(2).ToString();
                storage.Add(mid);
            }
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        //审核评论是否通过
        public  string agreeComment(string comment_id,int option)
        {
            CreateConn();
            //先修改status的状态,将其改为1
            OracleCommand edit = DB.CreateCommand();
            if (option == 1)  //表示审核通过
            {
                edit.CommandText = "update article_comment set status=1 where id=:id";
                edit.Parameters.Add(new OracleParameter(":id", comment_id));
            }
            else
            {
                edit.CommandText = "delete from article_comment where id=:id";
                edit.Parameters.Add(new OracleParameter(":id", comment_id));
            }
            //commit();
            int m = edit.ExecuteNonQuery();
            CloseConn();
            return m.ToString();
        }
        //向MUser表中增加一个新用户(注册)
        //添加成功返回UserID，添加失败返回“0”
        public  string AddUser(string UserName, string UserPassword)
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
        public  User FindUserInfo(string UserID)
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
