namespace Utils
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);    
        }
    }
}
