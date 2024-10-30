using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTraker.Data.Models;

namespace TaskTraker.Services.Dtos
{
    public record GetUserDto(string UserName, string Email);

    public static class GetUserDtoExtensions
    {
        public static GetUserDto FromUserToGetDto(this User user) => new
         (
             UserName: user.UserName,
             Email: user.Email
         );

        public static void FromGetDtoToUser(this GetUserDto userDto, User user)
        {
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
        }
    }
}
