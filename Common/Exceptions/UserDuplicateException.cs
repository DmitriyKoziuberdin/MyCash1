using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class UserDuplicateException : BusinessLogicExceptionBase
    {
        public UserDuplicateException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.UserDuplicate;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
