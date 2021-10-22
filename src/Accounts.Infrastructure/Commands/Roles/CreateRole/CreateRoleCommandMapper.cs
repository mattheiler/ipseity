using Accounts.Core.Commands.Roles.CreateRole;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Accounts.Infrastructure.Commands.Roles.CreateRole
{
    public class CreateRoleCommandMapper : Profile
    {
        public CreateRoleCommandMapper()
        {
            CreateMap<CreateRoleCommand, IdentityRole>();
        }
    }
}