﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApi.Application.Interface
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
    }
}
