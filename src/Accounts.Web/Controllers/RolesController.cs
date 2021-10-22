using System.Threading.Tasks;
using Accounts.Core.Commands.Roles.CreateRole;
using Accounts.Web.Models.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        public RolesController(ISender sender, IMapper mapper)
        {
            Sender = sender;
            Mapper = mapper;
        }

        public ISender Sender { get; }

        public IMapper Mapper { get; }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task Register(CreateRoleRequest request)
        {
            await Sender.Send(Mapper.Map(request, new CreateRoleCommand()));
        }
    }
}