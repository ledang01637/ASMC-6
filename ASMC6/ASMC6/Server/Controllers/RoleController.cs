using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _RoleService;

        public RoleController(RoleService _Role)
        {
            _RoleService = _Role;
        }

        [HttpGet("GetRoles")]
        public ActionResult<List<Role>> GetRoles()
        {
            try
            {
                return Ok(_RoleService.GetRoles());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddRole")]
        public Role AddRole(Role Role)
        {
            return _RoleService.AddRole(new Role
            {
                Name = Role.Name,

            });
        }

        [HttpGet("{id}")]
        public ActionResult<Role> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(_RoleService.GetIdRole(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = _RoleService.DeleteRole(id);
            if (deletedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(deletedCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Role updatedRole)
        {
            var updatedLoai = _RoleService.UpdateRole(id, updatedRole);
            if (updatedLoai == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedLoai);
        }
    }
}
