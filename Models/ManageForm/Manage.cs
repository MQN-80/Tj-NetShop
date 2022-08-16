using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
namespace WebApi.Models.ManageForm
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
        //获取待处理的商品
        public string get_Unproduct(int sum)
        {
            return Database.getProduct(sum);
        }
        public string agree_product(int manage_id, int product_id, string explain, string manage_name,int status)
        {
            if (status == 1 || status == 0)
                return Database.agreeProduct(manage_id, product_id, explain, manage_name, status);
            else
                return "参数违法";
        }
        public string get_article(int sum)
        {
            if (sum < 0)
                return "参数违法";
            else
                return Database.get_article(sum);
        }
        public string manage_article(string id,int option)
        {
            if (option!=1&&option!=0)
            {
                return "参数非法";
            }
            else
                return Database.agree_article(id,option);
        }
        public string get_comment(int sum)
        {
            if (sum < 0)
                return "参数违法";
            else
                return Database.getComment(sum);

        }
        public string agree_comment(string comment_id,int option)
        {
            return Database.agreeComment(comment_id,option);
        }
    }

}
