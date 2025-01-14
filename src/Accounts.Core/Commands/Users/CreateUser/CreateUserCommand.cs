﻿using MediatR;

namespace ProgrammerGrammar.Accounts.Core.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}