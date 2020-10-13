using AutoMapper;
using BizActionExample.Domain.Models.Accounts;
using BizActionExample.Domain.Models.MetaModels;
using BizActionExample.Domain.Models.Response;
using BizActionExample.Services.Helpers;
using BizActionExample.Services.Repositories;
using System.Threading.Tasks;

namespace BizActionExample.Services.BizActions.Identity
{
    internal sealed class RegisterUserAction : BizActionAsync, IRegisterUserAction
    {
        private readonly IUserRepository _repository;

        public RegisterUserAction(IMapper mapper, IUserRepository repository) : base(mapper)
        {
            _repository = repository;
        }

        public async Task<ResponseResult<UserInfo>> BizActionAsync(RegisterAccountMetaModel registerAccountMetaModel)
        {
            var checkExistEmail = await _repository.CheckEmailAsync(registerAccountMetaModel.Email, default);
            if (!checkExistEmail.Succeeded)
            {
                return new ResponseResult<UserInfo>
                {
                    Data = null,
                    Succeeded = checkExistEmail.Succeeded,
                    Error = checkExistEmail.Error
                };
            }

            var salt = UserHelper.GeneraSalt();
            var accountInfo = new UserInfo
            {
                Email = registerAccountMetaModel.Email,
                Password = UserHelper.HashPassword(registerAccountMetaModel.Password, salt),
                Refno = registerAccountMetaModel.Refno,
                UserName = registerAccountMetaModel.UserName,
                UserType = registerAccountMetaModel.UserType,
                HashSalt = salt
            };

            var result = await _repository.InsertAsync(accountInfo, default);

            return new ResponseResult<UserInfo>
            {
                Data = result.Data,
                Succeeded = result.Succeeded,
                Error = result.Error
            };
        }
    }
}