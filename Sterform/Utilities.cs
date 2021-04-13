namespace Sterform
{
    public static class Utilities
    {
        public static string FormatNumber(float number, string variable)
        {
            if (number == 0) return " ";
            return number > 0 ? $" +{number}{variable}" : $" {number}{variable}";
        }

        public static string FormatNumber(float number)
        {
            if (number == 0) return " ";
            return number > 0 ? $" +{number}" : $" {number}";
        }
    }
}