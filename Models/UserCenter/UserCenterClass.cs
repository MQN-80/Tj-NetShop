using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.UserCenter
{
    public class UserInfo
    {
        public string UserName { get; set; }  // 用户昵称
        public string UserDetail { get; set; } // 用户的个人简介
        public string Gender { get; set; }
    }

    public class UserRoleRank
    {
        public string UserId { get; set; }
        public int RoleRank { get; set; } // 用户的权限等级
    }
    
}