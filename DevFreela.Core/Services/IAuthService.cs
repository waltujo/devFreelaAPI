using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Core.Services
{
    public interface IAuthService
    {
        string GenerateToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
