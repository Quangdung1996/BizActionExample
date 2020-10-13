using BizActionExample.Domain.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BizActionExample.Infa
{
    public class EfCoreContext : DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }

        public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserInfo>(o =>
          {
              o.HasKey(x => new { x.UserId });

              o.ToTable("UserInfos", "Identity");
          });
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (DbUpdateConcurrencyException concurrencyEx)
            {
                throw new ApplicationException("concurrency", concurrencyEx);
            }
            catch (DbUpdateException ex)
            {
                ex.ThrowIfValidationException();
                throw;
            }
        }
    }
}