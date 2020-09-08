using BizActionExample.Configs.Swagger.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BizActionExample.Controllers.V1
{
    [ApiVersion("1")]
    [AuthorizeHeaderRequired]
    [Produces("application/json")]
    [Route("BizActionExample/v1/values")]
    public class ValuesController : BaseController
    {
        [HttpGet]
        //[NonBodyParameter("DeviceToken", "Device token do dispositivo", true)]
        //[NonBodyParameter("OSSmartphone", "Sistema operacional do aparelho", true)]
        [AllowAnonymous]
        public string Get()
        {
            return "Test";
        }
    }
}