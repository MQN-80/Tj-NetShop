using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User
    {
        public virtual string UserID { get; set; }
        public virtual string UserPassword { get; set; }
    }
    public class JwtSettings
    {
        /// <summary>
        /// token是谁颁发的
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// token可以给那些客户端使用
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密的key（SecretKey必须大于16个,是大于，不是大于等于）
        /// </summary>
        public string SecretKey { get; set; }
    }
    public class app_mobile_user
    {
        public long id { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

    }
    
        public class user_health
    {
        public string date;        //对应时间
        public string temperature; //用户体温
        public string status;      //用户状态
        public string Get_user_temp(int user_id)
        {

            List<user_health> health = new List<user_health>();
            health.Add(new user_health { date = "2022/6/8 8:21:23", temperature = "35.9", status = "无不适症状" });
            health.Add(new user_health { date = "2022/6/9 13:31:27", temperature = "36.2", status = "无不适症状" });
            health.Add(new user_health { date = "2022/6/10 18:42:33", temperature = "35.4", status = "无不适症状" });
            health.Add(new user_health { date = "2022/6/11 8:12:19", temperature = "36.3", status = "无不适症状" });
            health.Add(new user_health { date = "2022/6/12 0:0:48", temperature = "36.2", status = "无不适症状" });
            health.Add(new user_health { date = "2022/6/13 21:02:24", temperature = "35.9", status = "无不适症状" });
            string ListJson = JsonConvert.SerializeObject(health);
            return ListJson;
        }
    }
    //用于用户认证鉴权
    public class Jwt
    {
        public object JsonHelper { get; private set; }



        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="user"></param>
        public object Token(int id,string password,string phone="123")
        {
            //测试自己创建的对象
            

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtClaimTypes.Audience, "aud"));
            claims.Add(new Claim(JwtClaimTypes.Issuer, "user"));
            claims.Add(new Claim(JwtClaimTypes.Name, phone));
            claims.Add(new Claim(JwtClaimTypes.Name, phone));
            claims.Add(new Claim(JwtClaimTypes.Id, id.ToString()));
            claims.Add(new Claim(JwtClaimTypes.PhoneNumber,phone));


            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bc47a26eb9a59406057dddd62d0898f4"));
            //指定数字签名需要使用的密钥和算法
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //生成token
            JwtSecurityToken token = new JwtSecurityToken(issuer: "user",
                                                          audience: "aud",
                                                          claims: claims,
                                                          notBefore: DateTime.Now, //token生效时间
                                                          expires: DateTime.Now.AddMinutes(50), //token有效时间
                                                          signingCredentials: credentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public  object Ok(string v)
        {
            throw new NotImplementedException();
        }
    }



}