using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace PostgRESTSharp.Core.Managers
{
    public static class StringManager
    {
        public static string ForceStringToStartWithUpper(string value)
        {
            return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
        }

        public static string WithIndent(this string input, int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                input = " " + input;
            }

            return input;
        }
    }
}