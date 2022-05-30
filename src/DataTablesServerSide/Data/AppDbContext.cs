﻿using DataTablesServerSide.Models;
using Microsoft.EntityFrameworkCore;

namespace DataTablesServerSide.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("MockUser");
        }
    }
}
