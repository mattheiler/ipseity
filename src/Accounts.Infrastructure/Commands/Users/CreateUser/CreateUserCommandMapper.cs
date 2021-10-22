using Accounts.Core.Commands.Users.CreateUser;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Accounts.Infrastructure.Commands.Users.CreateUser
{
    public class CreateUserCommandMapper : Profile
    {
        public CreateUserCommandMapper()
        {
            CreateMap<CreateUserCommand, IdentityUser>();
        }
    }
}