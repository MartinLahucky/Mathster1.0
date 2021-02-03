namespace Mathster.Resources.Helpers
{
    public static class Utilities
    {
        public static string FormartNumber(float number, string variable)
        {
            if (number == 0)
            {
                return "";
            }

            if (number > 0)
            {
                return $" +{number}{variable}";
            }

            return $" {number}{variable}";
        }
        
        public static string FormartNumber(float number)
        {
            if (number == 0)
            {
                return "";
            }

            if (number > 0)
            {
                return $" +{number}";
            }

            return $" {number}";
        }
    }
}