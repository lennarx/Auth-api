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
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly AuthContext _authContext;

        public GetAllUsersHandler(AuthContext context)
        {
            _authContext = context;
        }
        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _authContext.Users.ToListAsync(cancellationToken);
        }
    }
}
