using Accounts.Core.Commands.Roles.CreateRole;
using AutoMapper;

namespace Accounts.Web.Models.Roles
{
    [AutoMap(typeof(CreateRoleCommand), ReverseMap = true)]
    public class CreateRoleRequest
    {
    }
}