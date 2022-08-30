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
    }

    public class UserRoleRank
    {
        public string UserId { get; set; }
        public int RoleRank { get; set; } // 用户的权限等级
    }

    public class OrderHistory
    {
        public string Name { get; set; } // 商品名称
        public int status { get; set; } // 订单状态
        public int order_price { get; set; } // 成交价格

        public string id { get; set; }
        public string start_time { get; set; }
    }
}