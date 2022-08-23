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

        public string update_user_info(int user_id, string user_name, string user_intro, string icon_addr)
        {
            return UserCenterDatabase.UpdateUserInfo(user_id, user_name, user_intro, icon_addr);
        }
    }
}