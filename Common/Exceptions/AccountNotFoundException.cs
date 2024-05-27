using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class AccountNotFoundException : BusinessLogicExceptionBase
    {
        public AccountNotFoundException(string message) : base(message) 
        {
            ErrorCode = ErrorCodes.AccountNotFound;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
