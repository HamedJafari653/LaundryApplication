using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaundryApplication.Models;

namespace LaundryApplication.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<IssueReports> IssueReports { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Vi anropar variabeln från Secrets-klassen istället för att skriva ut den här
            optionsBuilder.UseSqlServer(Secrets.AzureConnectionString);
        }
    }
}
