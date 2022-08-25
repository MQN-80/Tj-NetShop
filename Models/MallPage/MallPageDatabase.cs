using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.MallPage
{
    public class MallPageDatabase
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
            //var connStr = $"DATA SOURCE=localhost/orcl; PASSWORD=030215Zhan; PERSIST SECURITY INFO=True; USER ID=system";
            DB = new OracleConnection(conStringUser);
            DB.Open();
        }
        //关闭数据库连接
        public static void CloseConn()
        {
            DB.Close();
        }
        // 随机返回四个商品
        public static string GetFourRandomProduct()
        {
            List<Product_info> storage = new List<Product_info>();
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select product_id from (select * from product_collect oreder by sys_guid()) where rownum<=4";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                Product_info product_info = new Product_info();
                product_info.name = Ord.GetValue(0).ToString();
                product_info.img = Ord.GetValue(1).ToString();
                product_info.price = Ord.GetValue(2).ToString();

                storage.Add(product_info);
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        //返回收藏夹里面的四个商品
        public static string GetFourCollectedProduct(int userid)
        {
            List<Product_info> storage = new List<Product_info>();

            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select name,img,price from (select * from (select * from product_infomation where user_id =:userid) oreder by sys_guid()) where rownum<=4";
            Search.Parameters.Add(new OracleParameter(":userid", userid));
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                Product_info product_info = new Product_info();
                product_info.name = Ord.GetValue(0).ToString();
                product_info.img = Ord.GetValue(1).ToString();
                product_info.price = Ord.GetValue(2).ToString();

                storage.Add(product_info);
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        //返回四个打折商品
        public static string GetFourDiscountProduct()
        {
            List<Product_info> storage = new List<Product_info>();
            //找到随机的四个打折商品的product_id
            //string products[4];
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select product_id from (select * from activity_product_relationship oreder by sys_guid()) where rownum<=4";
            OracleDataReader Ord = Search.ExecuteReader();
            //string products[4];
            int i = 0;
            while (Ord.Read())
            {
                //products[i]= Ord.GetValue(0).ToString();
                i++;
            }
            //依次搜索四个商品
            for(int k=0;k<4;k++)
            {
                OracleCommand Search2 = DB.CreateCommand();
                Search2.CommandText = "select name,img,price from product_infomation where product_id =:productid";
                //Search2.Parameters.Add(new OracleParameter(":productid", products[k]));
                //OracleDataReader Ord = Search2.ExecuteReader();
                while (Ord.Read())
                {
                    Product_info product_info = new Product_info();
                    product_info.name = Ord.GetValue(0).ToString();
                    product_info.img = Ord.GetValue(1).ToString();
                    product_info.price = Ord.GetValue(2).ToString();

                    storage.Add(product_info);
                }
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        //返回随机一家店铺的四个商品
        public static string GetRandomShopProduct()
        {
            List<Product_info> storage = new List<Product_info>();
            CreateConn();
            //找到随机商店
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select shop_id from (select * from shop_product oreder by sys_guid()) where rownum<=1";
            OracleDataReader Ord = Search.ExecuteReader();
            string shopid;
            while (Ord.Read())
            {
               shopid=Ord.GetValue(0).ToString();
            }
            //搜索四个商品
            //string products[4];
            OracleCommand Search2 = DB.CreateCommand();
            Search2.CommandText = "select product_id from(select * form(select * from shop_product where shop_id=:shopid)oreder by sys_guid())where rownum<=4";
            //Search2.Parameters.Add(new OracleParameter(":shopid", shopid));
            OracleDataReader Ord2 = Search2.ExecuteReader();
            //string products[4];
            int i = 0;
            while (Ord.Read())
            {
                //products[i] = Ord.GetValue(0).ToString();
                i++;
            }
            //依次搜索四个商品
            for (int k = 0; k < 4; k++)
            {
               //OracleCommand Search2 = DB.CreateCommand();
                Search2.CommandText = "select name,img,price from product_infomation where product_id =:productid";
                //Search2.Parameters.Add(new OracleParameter(":productid", products[k]));
                //OracleDataReader Ord = Search2.ExecuteReader();
                while (Ord.Read())
                {
                    Product_info product_info = new Product_info();
                    product_info.name = Ord.GetValue(0).ToString();
                    product_info.img = Ord.GetValue(1).ToString();
                    product_info.price = Ord.GetValue(2).ToString();

                    storage.Add(product_info);
                }
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
    }
}
