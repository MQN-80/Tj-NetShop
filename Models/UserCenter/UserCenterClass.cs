using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.UserCenter
{
    public class UserInfo
    {
        public string UserId { get; set; }  // 用户ID作为主键
        public string UserName { get; set; }  // 用户昵称
        public string AvatarID { get; set; }  // 用户头像在资源服务器中的地址
        public string UserDetail { get; set; } // 用户的个人简介
    }

    public class UserRoleRank
    {
        public string UserId { get; set; }
        public int RoleRank { get; set; } // 用户的权限等级
    }
}