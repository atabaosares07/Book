using Book.Services.Rules.Base;
using System.Net;

namespace Book.Services.Rules
{
    public class PasswordIsRequiredException : BusinessRulesException
    {
        private const string message = "Username is required";

        public PasswordIsRequiredException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
