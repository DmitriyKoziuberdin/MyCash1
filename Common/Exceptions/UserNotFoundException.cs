using Common.Enum;
using System.Net;

namespace Common.Exceptions
{
    public class UserNotFoundException : BusinessLogicExceptionBase
    {
        public UserNotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.UserNotFound;
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
