using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "Login_Username", ErrorMessageResourceType = typeof(StaticResoure.Resources))]
        public string Username { set; get; }

        [Required(ErrorMessageResourceName = "Login_Password", ErrorMessageResourceType = typeof(StaticResoure.Resources))]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}