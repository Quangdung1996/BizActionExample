using GenericBizRunner;
using System;
using System.Threading.Tasks;

namespace BizActionExample.Services
{
    public sealed class ProductAction : BizActionStatus, IProductAction
    {
        public ProductAction()
        {

        }
        public async Task<CreatePaymentView> BizActionAsync(CreatePaymentModel inputData)
        {
            return new CreatePaymentView { Name="Dung"};
        }

   
    }
}
