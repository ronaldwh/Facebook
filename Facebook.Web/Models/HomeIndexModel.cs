using Facebook.Web.Models.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Web.Models
{
    public class HomeIndexModel
    {
        public User user { get; set; }
        public List<User> data { get; set; }

        public HomeIndexModel()
        {
            this.user = new User();
            this.data = new List<User>();
        }
    }
}