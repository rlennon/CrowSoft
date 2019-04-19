using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using crowsoftmvc.Models;
using crowsoftmvc.Areas.Identity.Data;

namespace crowsoftmvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<CrowsoftUser>
    {
        public string ConnectionString { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Dummy> Dummy { get; set; }
        public DbSet<DefaultFeature> DefaultFeature { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        


    }
}
