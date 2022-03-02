﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(string username, string password);
        void SetKey(string key);
        Task CreateUser(string username, string password, string name);
    }
}
