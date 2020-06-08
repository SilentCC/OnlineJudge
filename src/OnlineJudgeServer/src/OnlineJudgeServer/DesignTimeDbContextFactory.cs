using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OnlineJudgeServer.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace OnlineJudgeServer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OnlineJudgeContext>
    {
        public OnlineJudgeContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder( )
                .SetBasePath(Directory.GetCurrentDirectory( ))
                .AddJsonFile("appsettings.json")
                .Build( );

            var builder = new DbContextOptionsBuilder<OnlineJudgeContext>( );

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseMySql(connectionString,mysqlOptions =>
            {
                mysqlOptions.ServerVersion(new Version(5, 7, 26), ServerType.MySql);
            });

            return new OnlineJudgeContext(builder.Options);
        }
    }
}
