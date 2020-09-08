using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BizActionExample.Controllers
{
    //  [Authorize]
    [ApiController]
    [Produces("application/json")]

    ////[Route("[controller]")]


    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Token de acesso enviado no Header da requisição.
        /// </summary>
        protected string ApplicationId
        {
            get
            {
                if (Request.Headers.ContainsKey(HttpRequestHeader.Authorization.ToString()))
                    return Request.Headers[HttpRequestHeader.Authorization.ToString()].ToString();

                return null;
            }
        }

        /// <summary>
        /// Identificador da cultura desejada para a resposta de requisição da API.
        /// </summary>
        protected string AcceptLanguage
        {
            get
            {
                if (Request.Headers.ContainsKey(HttpRequestHeader.AcceptLanguage.ToString()))
                    return Request.Headers[HttpRequestHeader.AcceptLanguage.ToString()].ToString();

                return "pt-BR";
            }
        }

        /// <summary>
        /// DeviceToken para envio de push enviado no Header da requisição.
        /// </summary>
        protected string DeviceToken
        {
            get
            {
                if (Request.Headers.ContainsKey("DeviceToken"))
                    return Request.Headers["DeviceToken"].ToString();

                return null;
            }
        }

        protected string Authorization
        {
            get
            {
                if (Request.Headers.ContainsKey("Authorization"))
                    return Request.Headers["Authorization"].ToString();

                return null;
            }
        }
    }
}