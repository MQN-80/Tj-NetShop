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
        // 随机返回四个商品,好像没什么问题,新增数据之后再试一下
        public static string GetFourRandomProduct()
        {
            List<Product_info> storage = new List<Product_info>();
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select id,name,des,price from (select * from product_information order by sys_guid()) where rownum<=4";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                Product_info product_info = new Product_info();
                product_info.id = Ord.GetValue(0).ToString();
                product_info.name = Ord.GetValue(1).ToString();
                product_info.des = Ord.GetValue(2).ToString();
                product_info.price = Ord.GetValue(3).ToString();
                storage.Add(product_info);
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        //返回最近的四个商品
        public static string GetFourCollectedProduct()
        {
            List<Product_info> storage = new List<Product_info>();

            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select id,name,des,price,product_id from product_information where rownum<=4 order by product_id desc";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                Product_info product_info = new Product_info();
                product_info.id = Ord.GetValue(0).ToString();
                product_info.name = Ord.GetValue(1).ToString();
                product_info.des = Ord.GetValue(2).ToString();
                product_info.price = Ord.GetValue(3).ToString();
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
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select id,name,des,price,product_id,discount from product_information where rownum<=4 and discount<1";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                Product_info product_info = new Product_info();
                product_info.id = Ord.GetValue(0).ToString();
                product_info.name = Ord.GetValue(1).ToString();
                product_info.des = Ord.GetValue(2).ToString();
                product_info.price = Ord.GetValue(3).ToString();
                storage.Add(product_info);
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
        //返回随机一家店铺的多个商品
        public static string GetRandomShopProduct()
        {
            List<Product_info> storage = new List<Product_info>();
            CreateConn();
            //找到随机商店
            OracleCommand findShop = DB.CreateCommand();
            findShop.CommandText = "select * from ( select a.shop_id from shop_product a join product_information b on a.product_id=b.product_id group by a.shop_id having count(*)>=1 ) where rownum=1";
            string shop_id = findShop.ExecuteScalar().ToString();
            //return shop_id;
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select a.id,a.name,a.des,a.price,a.product_id from shop_product b join product_information a on a.product_id=b.product_id "
                + "where a.product_id=b.product_id and b.shop_id=:shop_id";
            Search.Parameters.Add(new OracleParameter(":shop_id", shop_id));
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                Product_info product_info = new Product_info();
                product_info.id = Ord.GetValue(0).ToString();
                product_info.name = Ord.GetValue(1).ToString();
                product_info.des = Ord.GetValue(2).ToString();
                product_info.price = Ord.GetValue(3).ToString();
                storage.Add(product_info);
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
    }
}
