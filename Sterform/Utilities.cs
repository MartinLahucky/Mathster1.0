namespace Sterform
{
    public static class Utilities
    {
        public static string FormatNumber(float number, string variable)
        {
            if (number == 0) return " ";

            if (number > 0) return $" +{number}{variable}";

            return $" {number}{variable}";
        }

        public static string FormatNumber(float number)
        {
            if (number == 0) return " ";

            if (number > 0) return $" +{number}";

            return $" {number}";
        }
    }
}