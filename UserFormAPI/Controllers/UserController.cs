using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserFormAPI.BusinessLayer.DTO;
using UserFormAPI.BusinessLayer.Services;
using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;

namespace UserFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] UserDTO newUser)
        {
            try
            {
                _userService.AddUser(newUser);

                return new OkResult();
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format("Failed to add new user {0}", ex));
            }
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                IEnumerable<UserDTO> allUsers = _userService.GetAllUsers();

                return new OkObjectResult(allUsers);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to add new user {0}", ex));
            }
        }

        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserDTO editedUser)
        {
            try
            {
                _userService.UpdateUser(editedUser);

                return new OkResult();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to add new user {0}", ex));
            }
        }

        [HttpPost("RemoveUser")]
        public IActionResult RemoveUser([FromBody] UserDTO editedUser)
        {
            try
            {
                _userService.DeleteUser(editedUser.userId);

                return new OkResult();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to delete new user {0}", ex));
            }
        }
    }
}
