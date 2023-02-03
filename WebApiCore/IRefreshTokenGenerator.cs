using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken(string username);
    }
}
