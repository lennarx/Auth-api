﻿using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}
