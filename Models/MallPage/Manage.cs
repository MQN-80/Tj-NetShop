using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
namespace WebApi.Models.MallPage
{
    public class Manage
    {
        //用于管理员的登陆
        public bool get_user(int userid,string password,string phone)
        {
            Jwt mid = new Jwt();
            if (Database.IsUserExist(userid, password))
                return true;
            else
                return false;
        }
        public string get_user_list(int sum)
        {
            return Database.getUserList(sum);
        }
    }
    public class user_info
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string create_time { get; set; }

    }

}
