using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Libraries
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string ReviewMsg { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Introduction { get; set; }
        public string Photos { get; set; }
        public string Qualifications { get; set; }
        public string AdminPhone { get; set; }
        public string AdminName { get; set; }
        public string AdminPassword { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
