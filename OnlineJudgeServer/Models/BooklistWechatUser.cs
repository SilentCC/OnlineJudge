using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class BooklistWechatUser
    {
        public int BooklistId { get; set; }
        public int WechatUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
