using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace OnlineJudgeServer.Models
{
    public class Submit
    {
        public int SubmitId { get; set; }

        public int ProblemId { get; set; }

        public int UserId { get; set; }

        public int JudgeStatus { get; set; }

        public string JudgeResult { get; set; }

        public DateTime SubmitTime { get; set; }

        public int CodeType { get; set; }

        public string CodeSuffix { get; set; }

        public string SourceCode { get; set; }
        
    }
}