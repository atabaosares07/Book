using Book.Services.Rules.Base;
using System.Net;

namespace Book.Services.Rules
{
    public class DuplicateConstraintException : BusinessRulesException
    {
        private const string message = "Duplicate constraint";

        public DuplicateConstraintException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
