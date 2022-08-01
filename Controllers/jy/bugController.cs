using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers.jy
{
    [Route("api/[controller]")]
    [ApiController]
    public class bugController : ControllerBase
    {
        [Route("get_user_inf")]
        [HttpPost]
        public string GetUserInfo(int userid)
        {
            //获取当前请求用户的信息，包含token信息
            user_health op = new user_health();
            return op.Get_user_temp(userid);
        }
    }
}
