
namespace ShortUrl.Application.Exceptions
{
    public class ValidationException: Exception
    {
        public Dictionary<string, List<string>> Errors { get; set; }
        public string? ErrorCode { get; set; }
        public ValidationException(ValidationResult validationResult)
        {
            Errors = new Dictionary<string, List<string>>();

            foreach (var validationError in validationResult.Errors)
            {
                if (Errors.ContainsKey(validationError.PropertyName))
                {
                    Errors[validationError.PropertyName].Add(validationError.ErrorMessage);
                }
                else
                {
                    Errors.Add(validationError.PropertyName, new List<string> { validationError.ErrorMessage });
                }
            }
        }
    }
}
