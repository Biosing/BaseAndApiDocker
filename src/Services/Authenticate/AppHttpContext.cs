using Microsoft.AspNetCore.Http;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authenticate
{
    public class AppHttpContext : IHttpContext
    {
        private readonly IHttpContextAccessor _http;

        public AppHttpContext(IHttpContextAccessor http)
        {
            _http = http;
        }

        public CurrentUser CurrentUser => new CurrentUser(
            _http.HttpContext?.User
            ?? throw new InvalidOperationException("The Http Context does not exist"));

        public bool Exists => _http.HttpContext != null;
    }
}
