using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int WechatUserId { get; set; }
        public string Isbn { get; set; }
        public int LibraryId { get; set; }
        public DateTime? ShouldTakeTime { get; set; }
        public string ActualTakeTime { get; set; }
        public int? RenewCount { get; set; }
        public string ShouldReturnTime { get; set; }
        public string ActualReturnTime { get; set; }
        public float Fine { get; set; }
        public int IsFinePaied { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
