using crowsoftmvc.Data;
using Microsoft.EntityFrameworkCore;


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
