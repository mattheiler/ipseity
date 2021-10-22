using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProgrammerGrammar.Accounts.Core.Commands.Roles.CreateRole;

namespace ProgrammerGrammar.Accounts.Infrastructure.Commands.Roles.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            RoleManager = roleManager;
            Mapper = mapper;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        public IMapper Mapper { get; }

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            await RoleManager.CreateAsync(Mapper.Map(request, new IdentityRole()));
            return default;
        }
    }
}