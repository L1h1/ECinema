using ECinema.Data.Entities;
using ECinema.Data.MediatR.Role;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAllRoles()
        {
            var roles = await _mediator.Send(new SelectAllRolesRequest());
            if (roles == null || roles.Count == 0)
            {
                return NotFound("No roles found.");
            }
            return Ok(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> InsertRole([FromBody] string name)
        {
            var result = await _mediator.Send(new InsertRoleRequest
            {
                RoleName = name
            });

            return CreatedAtAction(nameof(GetAllRoles), new { id = result }, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteRoleByIdRequest { RoleId = id });

            if (!result)
            {
                return NotFound("Role not found.");
            }

            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("/name/{name}")]
        public async Task<ActionResult> DeleteRoleByName([FromRoute] string name)
        {
            var result = await _mediator.Send(new DeleteRoleByNameRequest { RoleName = name });

            if (!result)
            {
                return NotFound("Role not found.");
            }

            return NoContent();
        }

    }
}
