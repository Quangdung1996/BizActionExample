using BizActionExample.Services;
using GenericBizRunner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllProduct([FromQuery] CreatePaymentModel createPaymentModel, [FromServices] IActionServiceAsync<IProductAction> action)
        {
            return Ok(await action.RunBizActionAsync<CreatePaymentView>(createPaymentModel));
        }
    }
}