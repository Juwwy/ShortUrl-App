

using ValidationException = ShortUrl.Application.Exceptions.ValidationException;

namespace ShortUrl.API.ErrorHandlers
{
    public static class GlobalExceptionHandler
    {
        public static void UseGlobalException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>();
                    if (exception != null)
                    {
                        var response = new ResponseModel<dynamic>();
                        if (exception.Error.GetType() == typeof(ValidationException))
                        {
                            ValidationException err = exception.Error as ValidationException;
                            if (err != null)
                            {
                                var foo = err.Errors.Select(x => x.Value).ToList();
                                var foos = foo.Select(x => x).ToList();
                                response = err.Errors == null || err.Errors.Count == 0 ? ResponseModel<dynamic>.ErrorMessage("An error occurred processing your request") : ResponseModel<dynamic>.ErrorMessage(GetErrors(err).FirstOrDefault(), GetErrors(err));
                            }
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            response = ResponseModel<dynamic>.ErrorMessage("Internal server error occurred");
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        }

                        await context.Response.WriteAsJsonAsync(response);
                    }

                });
            });
        }

        private static List<string> GetErrors(ValidationException err)
        {
            var finalResult = new List<string>();
            var errorList = err.Errors.Select(x => x.Value).ToList();
            foreach (var item in errorList)
            {
                finalResult.AddRange(item);
            }

            return finalResult;
        }
    }
}
