using System.Linq;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes
{
    public static class ErrorHandlingModes
    {
        public const string NONE = "none";
        public const string STANDARD = "standard";
        public const string DEFAULT = NONE;

        private static string[] validErrorHandlingModes = { NONE, STANDARD };

        public static bool IsValid(string lowerCaseMode)
        {
            return validErrorHandlingModes.Contains(lowerCaseMode);
        }
    }
}