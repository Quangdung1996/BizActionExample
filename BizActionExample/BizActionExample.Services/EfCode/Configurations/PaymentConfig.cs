using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BizActionExample.Services.EfCode.Configurations
{
    public class PaymentConfig : IEntityTypeConfiguration<CreatePaymentView>
    {
        public void Configure
            (EntityTypeBuilder<CreatePaymentView> entity)
        {
        }
    }
}