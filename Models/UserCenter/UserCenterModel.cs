using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.ShopCenter;

namespace WebApi.Models.UserCenter
{
    public class UserCenter
    {
        public string get_user_info(int user_id)
        {
            UserCenterDatabase usercenter = new UserCenterDatabase();
            return usercenter.GetUserInfo(user_id);
        }

        public string update_user_info(int user_id, string user_name, string user_detail)
        {
            UserCenterDatabase usercenter = new UserCenterDatabase();
            return usercenter.UpdateUserInfo(user_id, user_name, user_detail);
        }

        public string get_user_role_rank(int user_id)
        {
            UserCenterDatabase usercenter = new UserCenterDatabase();
            return usercenter.GetUserRoleRank(user_id);
        }

        public string get_order_history(int user_id)
        {
            UserCenterDatabase usercenter = new UserCenterDatabase();
            return usercenter.GetOrderHistory(user_id);
        }
    }
}