using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BizActionExample.Controllers.v1
{
    [AllowAnonymous]
    [Route("/Values/v{version:apiVersion}/[controller]")]
    public class ValuesController : BaseController
    {
        /// <summary>
        /// Get alll
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Test";
        }
    }
}