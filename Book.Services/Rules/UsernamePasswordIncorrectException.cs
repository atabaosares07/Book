using Book.Services.Rules.Base;
using System.Net;

namespace Book.Services.Rules
{
    public class UsernamePasswordIncorrectException : BusinessRulesException
    {
        private const string message = "Username or password is incorrect";

        public UsernamePasswordIncorrectException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
