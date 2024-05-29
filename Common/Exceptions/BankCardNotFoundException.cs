using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class BankCardNotFoundException : BusinessLogicExceptionBase
    {
        public BankCardNotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.BankCardNotFound;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
