using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ManageForm
{
    public class user_info
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string create_time { get; set; }

    }
    public class product_info
    {
        public string name { get; set; }
        public string img { get; set; }
        public string type_id { get; set; }
        public string product_id { get; set; }
        public string des { get; set; }
        public long price { get; set; }
        public string create_time { get; set; }
    }
    public class article
    {
        public string article_title { get; set; }
        public string article_context { get; set; }
        public string user_id { get; set; }
        public string create_time { get; set; }
        public string id { get; set; }
    }
    public class comment
    {
        public string comment_context { get; set; }
        public string create_time { get; set; }

        public string id { get; set; }
    }

}
