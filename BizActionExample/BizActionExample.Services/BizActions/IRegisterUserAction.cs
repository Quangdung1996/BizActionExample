using BizActionExample.Domain.Models.Accounts;
using BizActionExample.Domain.Models.MetaModels;
using BizActionExample.Domain.Models.Response;
using GenericBizRunner;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizActionExample.Services.BizActions
{
    public interface IRegisterUserAction: IGenericActionWriteDbAsync<RegisterAccountMetaModel, ResponseResult<UserInfo>>
    {
    }
}
