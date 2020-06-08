using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineJudgeServer.Models
{
    public class ProblemCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalProblemNum { get; set; }

        public ICollection<Problem> Problems { get; set; }
        
    }
}