using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users.Commands
{
    public class InsertUserCommand : IRequest<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public InsertUserCommand(string userName, string password, string name)
        {
            UserName = userName;
            Password = password;
            Name = name;
        }
    }
}
