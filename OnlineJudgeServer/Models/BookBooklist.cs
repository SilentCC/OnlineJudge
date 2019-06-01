using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class BookBooklist
    {
        public int BooklistId { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
