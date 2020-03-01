using Book.Services.Rules.Base;
using System.Net;

namespace Book.Services.Rules
{
	public class NotFoundException : BusinessRulesException
	{
		private const string message = "Record not found";

		public NotFoundException() : base(HttpStatusCode.NotFound, message) { }
	}
}
