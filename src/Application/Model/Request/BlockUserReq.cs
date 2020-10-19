using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model.Request
{
    public class BlockUserReq
    {        
        [Required]
        public string Username { get; set; }
    }
}
