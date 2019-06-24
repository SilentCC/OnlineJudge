using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineJudgeServer.Models
{
    public class Problem
    {
        public int ProblemId { get; set; }

        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public string Note { get; set; }

        public string ExampleInput { get; set; }

        public string ExampleOutPut { get; set; }

        public int PushlishId { get; set; }

        public DateTime PublishTime { get; set; }

        public ICollection<Submit> Submits { get; set; }
    }
}
