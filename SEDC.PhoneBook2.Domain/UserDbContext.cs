using Microsoft.EntityFrameworkCore;
using SEDC.PhoneBook2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.Domain
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<ContactEntry> ContactEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Address)
                .HasMaxLength(50)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
