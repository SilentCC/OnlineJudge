using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Reviews
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int WechatUserId { get; set; }
        public int? Score { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
