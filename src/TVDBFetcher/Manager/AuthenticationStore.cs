using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowReminder.TVDBFetcher.Manager
{
    public static class AuthenticationStore
    {
        private static string _authenticationToken;

        public static string AuthenticationToken
        {
            get { return _authenticationToken; }
            set
            {
                _authenticationToken = value;
                AuthenticationTokenRetrieved = DateTime.Now;
            }
        }

        private static DateTime AuthenticationTokenRetrieved { get; set; }

        public static bool HasAuthenticationToken => !string.IsNullOrWhiteSpace(_authenticationToken);
    }
}
