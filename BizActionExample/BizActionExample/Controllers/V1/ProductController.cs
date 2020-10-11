using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllProduct()
        {
            return Ok("result");
        }
    }
}
