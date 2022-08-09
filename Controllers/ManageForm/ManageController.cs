﻿using Microsoft.AspNetCore.Mvc;
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
        //获取待处理商品,同样采取分批请求
        [Route("/Manage/get_product")]
        [HttpGet]
        public string GetProductist(int sum)
        {
            Manage manage = new Manage();
            return manage.get_Unproduct(sum);
        }
        /*
         * 审核通过,修改该商品状态,处理完成后,将其加入审核表mange_product
         */
        [Route("/Manage/agree_product")]
        [HttpPut]
        public string agree_product(int manage_id, int product_id, string explain, string manage_name,int status)
        {
            Manage manage = new Manage();
            return manage.agree_product(manage_id, product_id, explain, manage_name,status);
        }
        //获取待审核的文章内容,同样分批返回
        [Route("/Manage/get_article")]
        [HttpGet]
        public string get_article(int num)
        {
            Manage manage = new Manage();
            return manage.agree_product(manage_id, product_id, explain, manage_name, status);
        }
    }
}
