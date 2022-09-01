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
            userForumDatabase forum = new userForumDatabase();
            if (num < 0)
                return "请求非法";
            return forum.get_article(num);
        }
        public string push_article(string title, string context, int user_id, int product_id)
        {
            userForumDatabase forum = new userForumDatabase();
            return forum.push_article(title, context, user_id, product_id);
        }
        public string get_comment(int article_id)
        {
            userForumDatabase forum = new userForumDatabase();
            return forum.get_comment(article_id);
        }
        public string push_comment(string context, int user_id, int article_id)
        {
            userForumDatabase forum = new userForumDatabase();
            return forum.push_comment(context, user_id, article_id);
        }
    }
}
