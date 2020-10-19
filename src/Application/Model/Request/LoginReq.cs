using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model.Request
{
    public class LoginReq
    {
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
