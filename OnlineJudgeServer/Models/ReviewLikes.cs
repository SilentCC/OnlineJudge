using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class ReviewLikes
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int Phone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
