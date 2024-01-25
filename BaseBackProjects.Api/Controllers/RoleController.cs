using Application.Cqrs.Role.Commands;
using Application.Cqrs.Role.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseBackProjects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiControllerBase
    {
        /// <summary>
        /// Agrega un nuevo rol en la base de datos
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] PostRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Trae todos los roles de la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await Mediator.Send(new GetRolesQuery()));
        }

        /// <summary>
        /// Actualiza los Roles
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateRole([FromBody] PutRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Eliminar Roles
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRole(Guid Id)
        {
            return Ok(await Mediator.Send(new DeleteRoleCommand() { Id = Id }));
        }

        /// <summary>
        /// Obtener Rol por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> GetRoleById(Guid Id)
        {
            return Ok(await Mediator.Send(new GetRoleQueryById() { Id = Id }));
        }
    }
}
