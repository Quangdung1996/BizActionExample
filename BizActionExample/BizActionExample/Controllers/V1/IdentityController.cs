using BizActionExample.Domain.Models.Accounts;
using BizActionExample.Domain.Models.MetaModels;
using BizActionExample.Domain.Models.Response;
using BizActionExample.Services.BizActions;
using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BizActionExample.Controllers.V1
{
    [AllowAnonymous]
    [Route("/identity/v{version:apiVersion}/[controller]")]
    public class IdentityController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> RegisterAccountAsync([FromBody] RegisterAccountMetaModel registerAccountMeta, [FromServices] IActionServiceAsync<IRegisterUserAction> action)
        {
            var result = await action.RunBizActionAsync<ResponseResult<UserInfo>>(registerAccountMeta);
            if (result.Data != null)
            {
                result.Data.Password = registerAccountMeta.Password;
            }
            return Ok(result);
        }

        //[HttpPost("SignIn")]
        //public async Task<IActionResult> SignInAsync([FromBody] SignInMetaModel signMetaModel, [FromServices] IActionServiceAsync<IRegisterUserAction> action))
        //{
           
        //}
    }
}