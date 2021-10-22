using AutoMapper;
using ProgrammerGrammar.Accounts.Core.Commands.Roles.CreateRole;

namespace ProgrammerGrammar.Accounts.Web.Models.Roles
{
    [AutoMap(typeof(CreateRoleCommand), ReverseMap = true)]
    public class CreateRoleRequest
    {
    }
}