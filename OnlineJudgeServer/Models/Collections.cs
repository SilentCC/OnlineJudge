using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Collections
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public int TotalNum { get; set; }
        public int AvailableNum { get; set; }
        public int IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
