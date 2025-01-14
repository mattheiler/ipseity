﻿using AutoMapper;
using ProgrammerGrammar.Accounts.Core.Commands.Users.CreateUser;

namespace ProgrammerGrammar.Accounts.Web.Models.Users
{
    [AutoMap(typeof(CreateUserCommand), ReverseMap = true)]
    public class CreateUserRequest
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}