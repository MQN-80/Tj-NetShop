using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Validations.Rules;

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
            var storage = new List<userInfo>();
            CreateConnection();
            var find = DB.CreateCommand();
            
            
            // logics
            
            CloseConnection();
            return JsonConvert.SerializeObject(storage);
        }

        public static bool PushUserInfo(int UserID, string userName, string userIntro, string iconAddr)
        {
            CreateConnection();
            var find = DB.CreateCommand();
            bool isPushSuccess = true;

            // logics...
            isPushSuccess = false;

            return isPushSuccess;
        }
    }
}