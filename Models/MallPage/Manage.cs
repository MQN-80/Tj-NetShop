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
    }
}
