using System.Threading;
using System.Threading.Tasks;
using Accounts.Core.Commands.Users.CreateUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Accounts.Infrastructure.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        public CreateUserCommandHandler(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            UserManager = userManager;
            Mapper = mapper;
        }

        public UserManager<IdentityUser> UserManager { get; }

        public IMapper Mapper { get; }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await UserManager.CreateAsync(Mapper.Map(request, new IdentityUser()), request.Password);
            return default;
        }
    }
}