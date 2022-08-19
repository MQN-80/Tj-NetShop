using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApi.Models.UserForum
{
    public class userForumDatabase
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
        public static string get_article(int sum)
        {
            List<article> storage = new List<article>();
            CreateConn();

            OracleCommand find = DB.CreateCommand();
            //首先获取待处理的总数
            find.CommandText = "select count(*) from article where status=1";
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
            get_list.CommandText = "select a.id,a.article_title,a.article_context,(select user_name from user_info where user_id=a.user_id),a.create_time,a.product_id,a.article_id from article a " +
                "where rownum>=:begin and rownum<=:end and a.status=1 order by a.article_id desc";
            get_list.Parameters.Add(new OracleParameter(":begin", begin));
            get_list.Parameters.Add(new OracleParameter(":end", end));
            OracleDataReader Ord = get_list.ExecuteReader();
            while (Ord.Read())
            {
                article mid = new article();
                mid.id = Ord.GetValue(0).ToString();
                mid.article_title= Ord.GetValue(1).ToString();
                mid.article_context = Ord.GetValue(2).ToString();
                mid.user_name= Ord.GetValue(3).ToString();
                mid.create_time = Ord.GetValue(4).ToString();
                mid.product_id = Ord.GetValue(5).ToString();
                mid.article_id= Ord.GetValue(6).ToString();
                storage.Add(mid);
            }
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        public static string push_article(string title,string context,int user_id,int product_id)
        {
            CreateConn();
            //先修改status的状态,将其改为1
            OracleCommand edit = DB.CreateCommand();
            string timenow=DateTime.Now.ToString();
            edit.CommandText = "insert into article " +
                "(user_id,article_title,article_context,create_time,product_id,status,article_id) " +
                "values (:user_id,:title,:context,:timeNow,:product_id,0,article_seq.nextval)";
            edit.Parameters.Add(new OracleParameter(":user_id", user_id));
            edit.Parameters.Add(new OracleParameter(":title", title));
            edit.Parameters.Add(new OracleParameter(":context", context));
            edit.Parameters.Add(new OracleParameter(":timeNow", timenow));
            edit.Parameters.Add(new OracleParameter(":product_id", product_id));
            int m = edit.ExecuteNonQuery();
            
            CloseConn();
            return m.ToString();
        }
        public static string get_comment(string article_id)
        {
            List<article_comment> storage = new List<article_comment>();
            CreateConn();
            OracleCommand get_list = DB.CreateCommand();
            //获取当前请求范围
            get_list.CommandText = "select a.comment_context,a.create_time,(select user_name from user_info where user_id=a.user_id ) " +
                "from article_comment a where a.article_id=:article_id and a.status=1";
            get_list.Parameters.Add(new OracleParameter(":article_id", article_id));
            OracleDataReader Ord = get_list.ExecuteReader();
            while (Ord.Read())
            {
                article_comment mid = new article_comment();
                mid.comment_context = Ord.GetValue(0).ToString();
                mid.create_time = Ord.GetValue(1).ToString();
                mid.user_name= Ord.GetValue(2).ToString();
                storage.Add(mid);
            }
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        public static string push_comment(string context,int user_id,int article_id)
        {
            CreateConn();
            //先修改status的状态,将其改为1
            OracleCommand edit = DB.CreateCommand();
            string timenow=DateTime.Now.ToString();
            edit.CommandText = "insert into article_comment " +
                "(user_id,comment_context,create_time,status,article_id) " +
                "values (:user_id,:context,:timeNow,0,:article_id)";
            edit.Parameters.Add(new OracleParameter(":user_id", user_id));
            edit.Parameters.Add(new OracleParameter(":context", context));
            edit.Parameters.Add(new OracleParameter(":timeNow", timenow));
            edit.Parameters.Add(new OracleParameter(":article_id", article_id));
            int m = edit.ExecuteNonQuery();
            CloseConn();
            return m.ToString();
        }

    }
}
