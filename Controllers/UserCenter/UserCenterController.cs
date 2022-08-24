using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.UserCenter;

namespace WebApi.Controllers.UserCenter
{
    public class UserCenterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("userCenter/get_user_info")]
        [HttpGet]
        public string get_user_info(int user_id)
        {
            Models.UserCenter.UserCenter center = new Models.UserCenter.UserCenter();
            return center.get_user_info(user_id);
        }
        
        [Route("userCenter/update_user_info")]
        [HttpPost]
        public string update_user_info(int user_id, string user_name, string user_detail, string avatar_id)
        {
            Models.UserCenter.UserCenter center = new Models.UserCenter.UserCenter();
            return center.update_user_info(user_id, user_name, user_detail, avatar_id);
        }
    }
}