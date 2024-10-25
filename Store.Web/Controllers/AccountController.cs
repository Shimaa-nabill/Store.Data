using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.HandleResponse;
using Store.Service.Services.UserService;
using Store.Service.Services.UserService.DTOs;

namespace Store.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task <ActionResult<UserDto>> Login(LoginDto input)
        {
            var user = await _userService.Login(input);
            if (user == null)
                return BadRequest(new CustomeException(400, "Email Does Not Found"));
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = await _userService.Register(input);
            if (user == null)
                return BadRequest(new CustomeException(400, "Email Already Exists"));
            return Ok(user);
        }

    }
}
