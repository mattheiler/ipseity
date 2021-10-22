using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammerGrammar.Accounts.Core.Commands.Users.CreateUser;
using ProgrammerGrammar.Accounts.Web.Models.Users;

namespace ProgrammerGrammar.Accounts.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public UsersController(ISender sender, IMapper mapper)
        {
            Sender = sender;
            Mapper = mapper;
        }

        public ISender Sender { get; }

        public IMapper Mapper { get; }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task Register(CreateUserRequest request)
        {
            await Sender.Send(Mapper.Map(request, new CreateUserCommand()));
        }
    }
}