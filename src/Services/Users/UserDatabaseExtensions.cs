using Database;
using Microsoft.EntityFrameworkCore;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public static class UserDatabaseExtensions
    {
        public static async Task<User> UserOrNullAsync(this DatabaseContext context, string iin)
        {
            return await context.Query()
                .FirstOrDefaultAsync(x => x.IIN == iin);
        }

        private static IQueryable<User> Query(this DatabaseContext context)
        {
            return context.Users;
        }
    }
}
