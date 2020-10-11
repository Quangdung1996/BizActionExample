using System.Threading.Tasks;

namespace BizActionExample.Services
{
    public interface IProductAction: IGenericActionInOnlyWriteDbAsync
    {
        Task<string> GetAllAsync();
    }
}