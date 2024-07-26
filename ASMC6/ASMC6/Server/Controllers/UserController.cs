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
    public class UserController : ControllerBase
    {
        private readonly UserService _UserService;

        public UserController(UserService _User)
        {
            _UserService = _User;
        }

        [HttpGet("GetUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            try
            {
                var users = _UserService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddUser")]
        public User AddUser(User User)
        {
            return _UserService.AddUser(new User
            {
                RoleId = User.RoleId,
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                Phone = User.Phone,
                Address = User.Address,
                IsDelete = User.IsDelete,
            });
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(_UserService.GetIdUser(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = _UserService.DeleteUser(id);
            if (deletedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(deletedCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User updatedUser)
        {
            var updatedLoai = _UserService.UpdateUser(id, updatedUser);
            if (updatedLoai == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedLoai);
        }
    }
}
