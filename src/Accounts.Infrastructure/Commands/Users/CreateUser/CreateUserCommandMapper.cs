using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProgrammerGrammar.Accounts.Core.Commands.Users.CreateUser;

namespace ProgrammerGrammar.Accounts.Infrastructure.Commands.Users.CreateUser
{
    public class CreateUserCommandMapper : Profile
    {
        public CreateUserCommandMapper()
        {
            CreateMap<CreateUserCommand, IdentityUser>();
        }
    }
}