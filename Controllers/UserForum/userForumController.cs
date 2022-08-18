using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.UserForum;

namespace WebApi.Controllers.UserForum
{
    public class userForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //获取发布的文章，每次返回sum个
        [Route("/userCenter/get_article")]
        [HttpGet]
        public string get_article(int sum)
        {
            userForum forum = new userForum();
            return forum.get_article(sum);
        }
        //增加新文章
        [Route("/userCenter/push_article")]
        [HttpPost]
        public string push_article(string title, string context, int user_id, int product_id)
        {
            userForum forum = new userForum();
            return forum.push_article(title, context, user_id, product_id);
        }
        //获取对应文章的评论
        [Route("/userCenter/get_comment")]
        [HttpGet]
        public string get_comment(string article_id)
        {
            userForum forum = new userForum();
            return forum.get_comment(article_id);
        }
        //在对应文章底部增加评论
        [Route("/userCenter/push_comment")]
        [HttpPost]
        public string push_comment(string context, int user_id, int article_id)
        {
            userForum forum = new userForum();
            return forum.push_comment(context,user_id,article_id);
        }
    }
}
