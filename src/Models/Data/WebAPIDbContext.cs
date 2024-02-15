using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPIModels.Models.V1;
using Microsoft.EntityFrameworkCore.Design;
using System.Data.Entity.Migrations.Infrastructure;
using System.Configuration;

namespace WebAPIModels.Data
{
    public class WebAPIDbContext : DbContext
    {  
        public WebAPIDbContext(DbContextOptions<WebAPIDbContext> options)
        : base(options)
        {
           // Database.EnsureDeleted();
           // Database.EnsureCreated();    
        }     
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }        



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQLSERVER2022;Initial Catalog=WebAPI;Persist Security Info=True;TrustServerCertificate=True;User ID=SA; Password=Sql123123..;");         
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            //ForeignKey Settings
            modelBuilder.Entity<Company>()
                 .HasMany(x => x.Employees)
                 .WithOne(x => x.Company)
                 .HasForeignKey(x => x.CompanyId)
                 .IsRequired();
        }

    }

    //If you make Project Models with Class Library, this following code is run Dotnet Migration Process  
    public class BloggingContextFactory : IDesignTimeDbContextFactory<WebAPIDbContext>
    {
        public WebAPIDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebAPIDbContext>();
            optionsBuilder.UseSqlServer("Data Source=SQLSERVER2022;Initial Catalog=WebAPI;Persist Security Info=True;TrustServerCertificate=True;User ID=SA; Password=Sql123123..;");

            return new WebAPIDbContext(optionsBuilder.Options);
        }
    }
}
