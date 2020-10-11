using GenericBizRunner;
using System;
using System.Threading.Tasks;

namespace BizActionExample.Services
{
    internal sealed class ProdcutAction : BizActionStatus, IProductAction
    {
        public ProdcutAction()
        {

        }
        public async Task<CreatePaymentView> BizActionAsync(CreatePaymentModel inputData)
        {
            return default;
        }

   
    }
}
