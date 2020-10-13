using BizActionExample.Domain.Models;
using BizActionExample.Domain.Models.Accounts;
using BizActionExample.Domain.Models.Response;
using System.Threading;
using System.Threading.Tasks;

namespace BizActionExample.Services.Repositories
{
    public interface IUserRepository
    {
        Task<ResponseResult<UserInfo>> InsertAsync(UserInfo accountInfo, CancellationToken cancellationToken);

        Task<OperationResult> DeactivateAsync(string userName, UserType userType);

        Task<OperationResult> UpdateAsync(UserInfo accountInfo);

        Task<OperationResult> CheckEmailAsync(string email, CancellationToken cancellationToken);
    }
}