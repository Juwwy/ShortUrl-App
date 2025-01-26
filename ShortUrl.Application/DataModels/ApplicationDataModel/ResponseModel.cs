
namespace ShortUrl.Application.DataModels.ApplicationDataModel
{
    public class ResponseModel<T> where T : class
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public List<string> Errors { get; set; }

        public static ResponseModel<T> SuccessMessage(string message = "", dynamic data = null, bool status = true)
        {
            return new ResponseModel<T>
            {
                Data = (T)data,
                Message = string.IsNullOrWhiteSpace(message) ? "Successful" : message,
                Success = status,
            };
        }

        public static ResponseModel<T> ErrorMessage(string message = "", List<string> errors = null)
        {
            message = string.IsNullOrWhiteSpace(message) ? "An error occurred while processing your request. Please try again later" : message;
            errors ??= [];
            if (errors.Count == 0) errors.Add(message);
            return new ResponseModel<T>
            {
                Message = string.IsNullOrWhiteSpace(message) ? "An error occurred while processing your request. Please try again later" : message,
                Success = false,
                Errors = errors
            };
        }

    }
}
