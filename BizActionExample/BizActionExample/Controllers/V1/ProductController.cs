using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizActionExample.Services;
using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BizActionExample.Controllers.V1
{
    [AllowAnonymous]
    [Route("/product/v{version:apiVersion}/[controller]")]
    public class ProductController : BaseController
    {
        public ProductController()
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromServices] IActionServiceAsync<IProductAction> action)
        {
            if(await action.RunBizActionAsync<CreatePaymentView>(new CreatePaymentModel()) is null)
            {
                return Ok("result");
            }
            return Ok("result");
        }
    }
}
