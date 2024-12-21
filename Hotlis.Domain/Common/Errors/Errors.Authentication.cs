using System.ComponentModel;
using ErrorOr;

namespace KS.Domain.Common.Errors;
public static partial class Errors
{
    public static class Authentication
    {
        public static Error NotFound=>Error.NotFound(code:"User.UserNotFound",
        description:"User does not exist");

        public static Error InvalidPassword=>Error.NotFound(code:"InvalidPassword",
        description:"Password was invalid");
    }
}