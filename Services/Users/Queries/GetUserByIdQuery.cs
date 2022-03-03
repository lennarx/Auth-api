using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public long Id { get; set; }    
        public GetUserByIdQuery(long id) => Id = id;
    }
}
