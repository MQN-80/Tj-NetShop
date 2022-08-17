using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.UserForum
{
    public class userForum
    {
        public string get_article(int num)
        {
            if (num < 0)
                return "请求非法";
            return userForumDatabase.get_article(num);
        }
        public string push_article(string title, string context, int user_id, int product_id)
        {
            return userForumDatabase.push_article(title, context, user_id, product_id);
        }
    }
}
