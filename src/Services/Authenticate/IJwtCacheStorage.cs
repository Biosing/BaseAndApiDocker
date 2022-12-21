using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authenticate
{
    public interface IJwtCacheStorage
    {
        Task SaveJwtAsync(long userId, Jwt jwt);

        Task<bool> IsValidAsync(long userId, string token);
    }
}
