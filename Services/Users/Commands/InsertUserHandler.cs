using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Users.Commands
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, User>
    {
        private readonly AuthContext _authContext;
        public InsertUserHandler(AuthContext authContext) => _authContext = authContext;
        public async Task<User> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName,
                Password = Encoding.ASCII.GetBytes(request.Password),
                Name = request.Name
            };

            _authContext.Users.Add(user);
            await _authContext.SaveChangesAsync(cancellationToken);

            return new User
            {
                Id = user.Id,
                UserName = request.UserName,
                Name = request.Name
            };
        }
    }
}
