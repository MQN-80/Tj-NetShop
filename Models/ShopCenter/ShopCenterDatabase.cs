using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopCenter
{
    public class ShopCenterDatabase
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
        public  string getProduct(string id)
        {
            CreateConn();
            OracleCommand Search = DB.CreateCommand();
            product_info product_info = new product_info();
            Search.CommandText = "select name,type_id,product_id,des,price,create_time,discount " +
                "from product_information where status=1 and id=:id";
            Search.Parameters.Add(new OracleParameter(":id", id));
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                product_info.name = Ord.GetValue(0).ToString();
                product_info.type_id = Ord.GetValue(1).ToString();
                product_info.product_id = Ord.GetValue(2).ToString();
                product_info.des = Ord.GetValue(3).ToString();
                product_info.price = Ord.GetValue(4).ToString();
                product_info.create_time = Ord.GetValue(5).ToString();
                product_info.discount = Ord.GetValue(6).ToString();
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(product_info);
        }
        // 随机返回四个商品
        public  string GetFourRandomProduct()
        {
            List<Product_info> storage = new List<Product_info>();
            CreateConn();
            OracleCommand Search = DB.CreateCommand();

            Search.CommandText = "select name,img,price from (select * from product_infomation oreder by sys_guid()) where rownum<=4";
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

        //返回店铺信息
        public  string getShopInfo(string shopUserId)
        {
            List<user> storage = new List<user>();
            CreateConn();

            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select user_name, user_detail from user_info where id = :shopUserId";
            Search.Parameters.Add(new OracleParameter(":shopUserId", shopUserId));
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                user User = new user();
                User.userName = Ord.GetValue(0).ToString();
                User.userDetail = Ord.GetValue(1).ToString();
                storage.Add(User);
            }

            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }

        //返回店铺商品
        public  string getShopProduct(string shopUserId)
        {
            List<product> storage = new List<product>();
            CreateConn();

            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select a.id,a.name, a.price from product_information a join shop_product b " +
                " on a.product_id=b.product_id where b.shop_id = :shopUserId";
            Search.Parameters.Add(new OracleParameter(":shopUserId", shopUserId));
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                product Product = new product();
                Product.id = Ord.GetValue(0).ToString();
                Product.name = Ord.GetValue(1).ToString();
                Product.price = Ord.GetValue(2).ToString();
                Product.img = "http://106.12.131.109:8083/product/" + Product.id + ".jpg";
                storage.Add(Product);
            }

            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }

        //关注店铺
        public  string followShop(int userId, string shopUserId)
        {
            CreateConn();

            OracleCommand Insert = DB.CreateCommand();
            string createTime = DateTime.Now.ToString();
            Insert.CommandText = "insert into subscribe_shop (user_id, shop_id, collect_time)" +
                                 "values(:userId, :shopUserId, :createTime)";
            Insert.Parameters.Add(new OracleParameter(":userId", userId));
            Insert.Parameters.Add(new OracleParameter(":shopUserId", shopUserId));
            Insert.Parameters.Add(new OracleParameter(":createTime", createTime));
            int result = Insert.ExecuteNonQuery();

            //失败返回0
            CloseConn();
            return result.ToString();
        }

        //取消关注店铺
        public  string cancelFollowShop(int userId, string shopUserId)
        {
            CreateConn();

            OracleCommand Delete = DB.CreateCommand();
            Delete.CommandText = "delete from subscribe_shop where user_id = :userId and shop_id = :shopUserId";
            Delete.Parameters.Add(new OracleParameter(":userId", userId));
            Delete.Parameters.Add(new OracleParameter(":shopUserId", shopUserId));
            int result = Delete.ExecuteNonQuery();

            //失败返回0
            CloseConn();
            return result.ToString();
        }
        public  string is_follow(int userid,string shopid)
        {
            CreateConn();
            OracleCommand find = DB.CreateCommand();
            find.CommandText = "select count(*) from subscribe_shop where user_id=:userid and shop_id=:shopid";
            find.Parameters.Add(new OracleParameter(":userid", userid));
            find.Parameters.Add(new OracleParameter(":shopid", shopid));
            int result = Convert.ToInt32(find.ExecuteScalar());
            if (result == 1)
                return "1";
            else
                return "0";
        }
        //发布商品
        public  string postProduct(string shopUserId, string productName, string productType, string productDes, int price)
        {
            CreateConn();

            OracleCommand Insert1 = DB.CreateCommand();
            string createTime = DateTime.Now.ToString();
            Insert1.CommandText = "insert into product_information (name, product_id,type_id, des, price, status, create_time)" +
                                  "values(:productName,product_seq.nextval, :productType, :productDes, :price, 0, :createTime)";
            Insert1.Parameters.Add(new OracleParameter(":productName", productName));
            Insert1.Parameters.Add(new OracleParameter(":productType", productType));
            Insert1.Parameters.Add(new OracleParameter(":productDes", productDes));
            Insert1.Parameters.Add(new OracleParameter(":price", price));
            Insert1.Parameters.Add(new OracleParameter(":createTime", createTime));
            int result1 = Insert1.ExecuteNonQuery();

            if (result1 == 0)
            {
                //失败返回0
                CloseConn();
                return result1.ToString();
            }

            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select product_id,id from product_information where create_time = :createTime and name=:productName";      
            Search.Parameters.Add(new OracleParameter(":createTime", createTime));
            Search.Parameters.Add(new OracleParameter(":productName", productName));
            OracleDataReader Ord = Search.ExecuteReader();
            string result2 = "0";
            string id = "";
            while (Ord.Read())
            {
                result2 = Ord.GetValue(0).ToString();
                id= Ord.GetValue(1).ToString();
            }

            if (result2 == "0")
            {
                //失败返回0
                CloseConn();
                return result2.ToString();
            }

            OracleCommand Insert2 = DB.CreateCommand();
            Insert2.CommandText = "insert into shop_product (shop_id, product_id, status)" +
                                  "values(:shopUserId, :productId, 0)";
            Insert2.Parameters.Add(new OracleParameter(":shopUserId", shopUserId));
            Insert2.Parameters.Add(new OracleParameter(":productId", result2));
            int result3 = Insert2.ExecuteNonQuery();

            if (result3 == 0)
            {
                //失败返回0
                CloseConn();
                return result3.ToString();
            }

            //成功返回商品id
            CloseConn();
            return result2.ToString();
        }

    //删除发布商品
    public  string deleteProduct(int productId, string shopUserId)
    {
      CreateConn();

      OracleCommand Delete1 = DB.CreateCommand();
      Delete1.CommandText = "delete from product_information where product_id = :productId";
      Delete1.Parameters.Add(new OracleParameter(":productId", productId));
      int result1 = Delete1.ExecuteNonQuery();

      if (result1 == 0)
      {
        //失败返回0
        CloseConn();
        return result1.ToString();
      }

      OracleCommand Delete2 = DB.CreateCommand();
      Delete2.CommandText = "delete from shop_product where product_id = :productId and shop_id = :shopUserId";
      Delete2.Parameters.Add(new OracleParameter(":productId", productId));
      Delete2.Parameters.Add(new OracleParameter(":shopUserId", shopUserId));
      int result2 = Delete2.ExecuteNonQuery();

      //失败返回0
      CloseConn();
      return result2.ToString();
    }
  }

}
