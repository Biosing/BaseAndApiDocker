using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Services.Authenticate
{
    public class Authorization : IAuthorization
    {
        private readonly IHttpContext _http;
        private readonly bool _withinBackgroundJob;

        private CurrentUser _user;

        public Authorization(IHttpContext http)
        {
            _http = http;
            _withinBackgroundJob = !_http.Exists;
        }

        public CurrentUser CurrentUser()
        {
            if (_withinBackgroundJob)
            {
                throw new InvalidOperationException("The current user is not available within background class");
            }

            return _user ??= _http.CurrentUser;
        }
    }
}
