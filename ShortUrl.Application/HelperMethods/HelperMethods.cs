
namespace ShortUrl.Application.HelperMethods
{
    public static class HelperMethods
    {
        public static string GenerateShortUrl(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RemoveWhiteSpaces(this string input)
        {
            var newInput = input.Replace(" ", "");
            return newInput;
        }
    }
}
