using Book.Services.Rules.Base;
using System.Net;

namespace Book.Services.Rules
{
    public class RecordAlreadyExistException : BusinessRulesException
    {
        private const string message = "The record already exist";

        public RecordAlreadyExistException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
