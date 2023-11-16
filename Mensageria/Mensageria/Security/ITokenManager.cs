using Mensageria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mensageria.Security
{
   public interface ITokenManager
    {
        Task<AuthResponse> GenerateTokenAsync(UserModel user);
    }
}
