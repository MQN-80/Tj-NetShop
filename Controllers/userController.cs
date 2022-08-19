using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        //获取JwtSettings对象信息
        private JwtSettings _jwtSettings;

        public object JsonHelper { get; private set; }



        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="user"></param>
        [Route("get_token")]
        [HttpGet]
        public string Token(string userid)
        {
            //测试自己创建的对象
            var user = new app_mobile_user
            {
                id = 1,
                phone = "138000000",
                password = "e10adc3949ba59abbe56e057f20f883e"
            };

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtClaimTypes.Audience, "aud"));
            claims.Add(new Claim(JwtClaimTypes.Issuer, "user"));
            claims.Add(new Claim(JwtClaimTypes.Name, user.phone.ToString()));
            claims.Add(new Claim(JwtClaimTypes.Name, user.phone.ToString()));
            claims.Add(new Claim(JwtClaimTypes.Id, userid));
            claims.Add(new Claim(JwtClaimTypes.PhoneNumber, user.phone.ToString()));
              
                          
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bc47a26eb9a59406057dddd62d0898f4"));
            //指定数字签名需要使用的密钥和算法
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //生成token
            JwtSecurityToken token = new JwtSecurityToken(issuer: "user",
                                                          audience: "aud",
                                                          claims: claims,
                                                          notBefore: DateTime.Now, //token生效时间
                                                          expires: DateTime.Now.AddMinutes(5), //token有效时间
                                                          signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }
       
        [Authorize]
        [Route("get_user_info")]
        [HttpPost]
        public IActionResult GetUserInfo()
        {
            //获取当前请求用户的信息，包含token信息
            var user = HttpContext.User;
            return Ok();
        }
        [Route("get_user_health")]
        [HttpGet]
        public string GetUserHealth(int userid)
        {
            user_health op = new user_health();
            return op.Get_user_temp(userid);
        }
        [HttpGet]
        [Route("is_user")]
        public string GetUser(string userid,string password)
        {
            

            if (ManageDatabase.IsUserExist(userid, password))
                return Token(userid);
            else
                return "no";
        }
        [HttpPost]
        public string AddUser(string username, string password)
        {
           user_result mid= ManageDatabase.AddUser(username, password);
            mid.token = Token(username);
            return JsonConvert.SerializeObject(mid);
        }

    }
}
