using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.UserForum
{
    public class article
    {
        public string id { get; set; }
        public string article_title{ get; set; }
        public string article_context { get; set; }
        public string user_id { get; set; }
        public string create_time { get; set; }
        public string product_id { get; set; }
        public string article_id { get; set; }
    }
    public class article_comment
    {
        public string comment_context { get; set; }
        public string create_time { get; set; }
        public string user_name { get; set; }
    
    }

}
