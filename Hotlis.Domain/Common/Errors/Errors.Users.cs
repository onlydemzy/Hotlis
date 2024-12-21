using ErrorOr;

namespace KS.Domain.Common.Errors;
public static partial class Errors
{
    public static class Users
    {
        public static Error DuplicateUser=>Error.Conflict(code:"User.DuplicateUser",
        description:"User already exist");
    }
}