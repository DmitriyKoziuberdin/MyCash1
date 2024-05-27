using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class TransactionNotFoundException : BusinessLogicExceptionBase
    {
        public TransactionNotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.TransactionNotFound;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
