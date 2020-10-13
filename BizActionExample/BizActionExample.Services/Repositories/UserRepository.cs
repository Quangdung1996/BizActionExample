using BizActionExample.Domain.Models;
using BizActionExample.Domain.Models.Accounts;
using BizActionExample.Domain.Models.Response;
using BizActionExample.Infa;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BizActionExample.Services.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly EfCoreContext _context;

        public UserRepository(EfCoreContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CheckEmailAsync(string email, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            if (await _context.UserInfos.Select(x => x.Email == email).AnyAsync(cancellationToken))
            {
                result.Error = "Email existed!";
                result.Succeeded = false;
            }
            else
            {
                result.Succeeded = true;
            }
            return result;
        }

        public Task<OperationResult> DeactivateAsync(string userName, UserType userType)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResult<UserInfo>> InsertAsync(UserInfo accountInfo, CancellationToken cancellationToken)
        {
            var result = new ResponseResult<UserInfo>();
            try
            {
                await _context.UserInfos.AddAsync(accountInfo, cancellationToken);
                result.Data = accountInfo;
                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                result.Succeeded = true;
                result.Error = ex.Message.ToString();
            }
            return result;
        }

        public Task<OperationResult> UpdateAsync(UserInfo accountInfo)
        {
            throw new NotImplementedException();
        }
    }
}