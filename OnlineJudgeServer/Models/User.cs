using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineJudgeServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime RegisterTime { get; set; }
        
    }
}