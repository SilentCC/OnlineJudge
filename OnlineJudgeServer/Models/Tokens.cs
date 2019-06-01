using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Tokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IsUsed { get; set; }
    }
}
