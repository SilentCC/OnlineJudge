using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Classifications
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string SonNumber { get; set; }
        public string ParentNumber { get; set; }
        public string NextNumber { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
