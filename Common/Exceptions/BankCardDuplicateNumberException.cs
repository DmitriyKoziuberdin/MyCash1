using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class BankCardDuplicateNumberException : BusinessLogicExceptionBase
    {
        public BankCardDuplicateNumberException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.BankCardDuplicateNumber;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
