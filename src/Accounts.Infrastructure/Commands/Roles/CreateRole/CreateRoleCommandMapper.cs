using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProgrammerGrammar.Accounts.Core.Commands.Roles.CreateRole;

namespace ProgrammerGrammar.Accounts.Infrastructure.Commands.Roles.CreateRole
{
    public class CreateRoleCommandMapper : Profile
    {
        public CreateRoleCommandMapper()
        {
            CreateMap<CreateRoleCommand, IdentityRole>();
        }
    }
}