using BizActionExample.Configs.Swagger.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BizActionExample.Controllers.v1
{
    [ApiVersion("1.0", Deprecated = true)]
    [AuthorizeHeaderRequired]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class ValuesController : BaseController
    {
        [HttpGet]
        //[NonBodyParameter("DeviceToken", "Device token do dispositivo", true)]
        //[NonBodyParameter("OSSmartphone", "Sistema operacional do aparelho", true)]
        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        public string Get()
        {
            return "Test";
        }
    }
}