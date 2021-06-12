  
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DbContext = SecretSanta.Data.DbContext;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
            : base(new DbContextOptionsBuilder<DbContext>().UseSqlite($"Data Source=D:/School/CSCD379/SecondaryBranch/SecretSanta/src/SecretSanta.Data/main.db").Options)
        { }

        public DbSet<Gift> Gifts => Set<Gift>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Assignment> Assignments => Set<Assignment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            modelBuilder.Entity<Gift>()
                .HasIndex(item => new { item.Title, item.UserId, item.Desc }).IsUnique();
            modelBuilder.Entity<Group>()
                .HasAlternateKey(c => new {c.Name});
            modelBuilder.Entity<User>()
                .HasAlternateKey(c => new {c.FirstName, c.LastName});
        }
    }
}