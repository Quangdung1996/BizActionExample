using BizActionExample.Services.EfCode.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BizActionExample.Services
{
    public class EfCoreContext : DbContext
    {
        public DbSet<CreatePaymentView> CreatePaymentView { get; set; }

        public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PaymentConfig());
        }
    }
}