using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Codes
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public int Expiry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
