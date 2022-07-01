using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
    


}