using GenericBizRunner;

namespace BizActionExample.Services
{
    public interface IProductAction : IGenericActionWriteDbAsync<CreatePaymentModel, CreatePaymentView>
    {
    }
}