using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Booklists
    {
        public int Id { get; set; }
        public int WechatUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
