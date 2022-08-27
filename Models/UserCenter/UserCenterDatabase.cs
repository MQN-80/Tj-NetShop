using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Validations.Rules;
using WebApi.Models.UserCenter;

namespace WebApi.Models.UserCenter
{
    public class UserCenterDatabase
    {
        private static OracleConnection DB;
        
        // 连接到数据库
        private static void CreateConnection()
        {
            //124.222.1.19
            var user = "shop";
            var pwd = "jy2051914";
            var dbAddr = "124.222.1.19/helowin";
            string conStringUser = "User ID=" + user + ";password=" + pwd + ";Data Source=" + dbAddr + ";";
            //string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl))); Persist Security Info=True;User ID=c##shop;Password=jinyi123mx427;";
            DB = new OracleConnection(conStringUser);
            DB.Open();
        }

        private static void CloseConnection()
        {
            DB.Close();
        }

        public static string GetUserInfo(int userID)
        {
            var storage = new List<UserInfo>();
            CreateConnection();
            var find = DB.CreateCommand();

            // Logics
            find.CommandText = "select user_name,user_detail, gender from user_info where user_id =: userID";
            find.Parameters.Add(new OracleParameter(":userID", userID));
            var ord = find.ExecuteReader();

            while (ord.Read())
            {
                var userInfo = new UserInfo();
                
                userInfo.UserName = ord.GetValue(0).ToString();
                userInfo.UserDetail = ord.GetValue(1).ToString();               
                storage.Add(userInfo);
            }
            CloseConnection();
            return JsonConvert.SerializeObject(storage);
        }

        public static string UpdateUserInfo(int userID, string userName, string userDetail, string gender)
        {
            if (is_user(userID, userName)) 
                return "no";
            CreateConnection();
            var edit = DB.CreateCommand();
            edit.CommandText = "update user_info set user_name=:userName, user_detail=:userDetail, gender=:gender where user_id=:userID";       
            edit.Parameters.Add(new OracleParameter("user_name", userName));
            edit.Parameters.Add(new OracleParameter("user_detail", userDetail));
            edit.Parameters.Add(new OracleParameter("gender", gender));
            edit.Parameters.Add(new OracleParameter("user_id", userID));

            var rowsAffected = edit.ExecuteNonQuery();
            CloseConnection();
            return rowsAffected.ToString();
        }
        public static bool is_user(int userid,string user_name)
        {
            CreateConnection();
            var find = DB.CreateCommand();
            find.CommandText = "select count(*) from user_info where user_name=:user_name and user_id!=:userid";
            find.Parameters.Add(new OracleParameter(":user_name", user_name));
            find.Parameters.Add(new OracleParameter(":userid", userid));
            int count = Convert.ToInt32(find.ExecuteScalar());
            CloseConnection();
            if (count > 0)
                return true;
            else
                return false;
        }
        public static string GetUserRoleRank(int userID)
        {
            CreateConnection();
            var find = DB.CreateCommand();

            find.CommandText = "select role_rank from user_info where user_id=:userID";
            find.Parameters.Add(new OracleParameter(":userID", userID));
            int count = Convert.ToInt32(find.ExecuteScalar());
            UserRoleRank user = new UserRoleRank();
            try
            {
                    user.RoleRank = count;
                    user.UserId = userID.ToString();
         
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            
            CloseConnection();
            return JsonConvert.SerializeObject(user);
        }
    }
}