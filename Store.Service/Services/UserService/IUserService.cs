using Store.Service.Services.UserService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> Login(LoginDto input);
        Task<UserDto> Register(RegisterDto input);
    }
}
