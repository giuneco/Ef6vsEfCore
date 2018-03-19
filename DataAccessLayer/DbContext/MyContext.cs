using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContext
{
    public class MyContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<DataAccessLayer.Model.Library> Libraries { get; set; }
        public DbSet<DataAccessLayer.Model.Book> Books { get; set; }
        public DbSet<DataAccessLayer.Model.Category> Category { get; set; }

        public MyContext()
        {
            
        }
        // The base constructor of DbContext also accepts the non-generic version of DbContextOptions, 
        // but using the non-generic version is not recommended for applications with multiple context types.
        public MyContext(DbContextOptions<MyContext> options) : base(options)

        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=EfCoreDb;Integrated Security=true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });
        }
    }
}
