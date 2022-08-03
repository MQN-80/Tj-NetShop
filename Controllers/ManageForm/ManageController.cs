using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.MallPage;

namespace WebApi.Controllers.ManageForm
{
    public class ManageController : Controller
    {
        //管理员注册接口
        [Route("/Manage/login")]
        [HttpGet]
        public object ManageLogin(int id,string password,string phone="122")
        {
            Manage manage = new Manage();
            userController user = new userController();

            if (manage.get_user(id, password, phone))
                return user.Token(id.ToString());
            else
                return null;
        }
        //获取用户列表，因用户可能过多，采用分批请求
        [Route("/Manage/get_userList")]
        [HttpGet]
        public string GetUserlist(int sum)
        {
            Manage manage = new Manage();
            return manage.get_user_list(sum);
        }

    }
}
