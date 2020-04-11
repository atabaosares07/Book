using Book.Services.Rules.Base;
using System.Net;

namespace Book.Services.Rules
{
	public class UsernameIsRequiredException : BusinessRulesException
	{
		private const string message = "Username is required";

		public UsernameIsRequiredException() : base(HttpStatusCode.BadRequest, message) { }
	}
}
