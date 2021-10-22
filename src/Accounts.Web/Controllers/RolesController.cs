using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammerGrammar.Accounts.Core.Commands.Roles.CreateRole;
using ProgrammerGrammar.Accounts.Web.Models.Roles;

namespace ProgrammerGrammar.Accounts.Web.Controllers
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