using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.UserCenter
{
    public class UserCenter
    {
        public string get_user_info(int user_id)
        {
            return UserCenterDatabase.GetUserInfo(user_id);
        }

        public string update_user_info(int user_id, string user_name, string user_detail, string gender)
        {
            return UserCenterDatabase.UpdateUserInfo(user_id, user_name, user_detail, gender);
        }

        public string get_user_role_rank(int user_id)
        {
            return UserCenterDatabase.GetUserRoleRank(user_id);
        }
    }
}