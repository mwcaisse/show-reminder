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

        // TimeSpan of 23 hours
        private static readonly TimeSpan AuthenticationTokenValidSpan = new TimeSpan(23, 0, 0);

        //Authentication token is valid if we have an authentication token, and it is less
        //than AuthenticationTokenValidSpan old 
        public static bool HasValidAuthenticationToken => 
            !string.IsNullOrWhiteSpace(_authenticationToken) &&
            (DateTime.Now - AuthenticationTokenRetrieved) <= AuthenticationTokenValidSpan;
    }
}
