using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class TransactionDuplicateCategoryNameException : BusinessLogicExceptionBase
    {
        public TransactionDuplicateCategoryNameException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.TransactionDuplicateCategory;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
