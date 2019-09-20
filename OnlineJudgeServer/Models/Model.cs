using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineJudgeServer.Models
{
    public class OnlineJudgeContext : DbContext
    {
        public OnlineJudgeContext(DbContextOptions<OnlineJudgeContext> options)
            : base(options)
        {
        }

        public DbSet<Problem> Problems { get; set; }

        public DbSet<Submit> Submits { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ProblemCategory> ProgramCategories { get; set; }
        
    }
}