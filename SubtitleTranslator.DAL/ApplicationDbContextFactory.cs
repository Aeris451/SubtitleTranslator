using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace SubtitleTranslator.DAL
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args = null)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException("Arguments are empty.");
            var di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var dataDir = $@"{di.Parent?.Parent?.Parent?.FullName}\Database";
            var connectionString = args.FirstOrDefault();
            connectionString = connectionString?.Replace($"|DataDirectory|", dataDir);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer(connectionString!);
            return new ApplicationDbContext(options.Options);
        }
    }
}
