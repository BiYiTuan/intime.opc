using System;
using System.ComponentModel.DataAnnotations;

namespace Intime.OPC.WebApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public String Password { get; set; }
    }
}