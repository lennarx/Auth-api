using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Users.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly AuthContext _authContext;

        public GetUserByIdHandler(AuthContext authContext) => _authContext = authContext;
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _authContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
