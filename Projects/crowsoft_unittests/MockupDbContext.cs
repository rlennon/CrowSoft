using crowsoftmvc.Areas.Identity.Data;
using crowsoftmvc.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace crowsoft_unittests
{
    /// <summary>
    /// This is a Mockup Db Context class to mimic the ApplicationDbContext
    /// </summary>
    public class MockupDbContext : ApplicationDbContext
    {
        public string ConnectionString { get; set; }

        public MockupDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}
