using Application.Cqrs.Auth.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BaseBackProjects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthTokenController : ApiControllerBase
    {
        /// <summary>
        /// Método que se encarga de generar el token de acceso.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] PostLoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
