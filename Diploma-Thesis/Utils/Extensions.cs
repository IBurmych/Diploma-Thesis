namespace Diploma_Thesis.Utils
{
    public static class DecimalExtensions 
    {
        public static bool IsBetween(this decimal val, decimal min, decimal max)
        {
            return val >= min && val <= max;
        }
    }
}
