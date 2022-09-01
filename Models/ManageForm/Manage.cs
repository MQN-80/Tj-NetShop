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
            Database data = new Database();
            Jwt mid = new Jwt();
            if (data.IsUserExist(userid, password))
                return true;
            else
                return false;
        }
        public string get_user_list(int sum)
        {
            Database data = new Database();
            return data.getUserList(sum);
        }
        //获取待处理的商品
        public string get_Unproduct(int sum)
        {
            Database data = new Database();
            return data.getProduct(sum);
        }
        public string agree_product(int product_id,int status)
        {
            Database data = new Database();
            if (status == 1 || status == 0)
                return data.agreeProduct(product_id,status);
            else
                return "参数违法";
        }
        public string get_article(int sum)
        {
            Database data = new Database();
            if (sum < 0)
                return "参数违法";
            else
                return data.get_article(sum);
        }
        public string manage_article(string id,int option)
        {
            Database data = new Database();
            if (option!=1&&option!=0)
            {
                return "参数非法";
            }
            else
                return data.agree_article(id,option);
        }
        public string get_comment(int sum)
        {
            Database data = new Database();
            if (sum < 0)
                return "参数违法";
            else
                return data.getComment(sum);

        }
        public string agree_comment(string comment_id,int option)
        {
            Database data = new Database();
            return data.agreeComment(comment_id,option);
        }
    }

}
