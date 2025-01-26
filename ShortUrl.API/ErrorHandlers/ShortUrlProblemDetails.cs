using Microsoft.AspNetCore.Mvc;

namespace ShortUrl.API.ErrorHandlers
{
    public class ShortUrlProblemDetails: ProblemDetails
    {
        public Dictionary<string, List<string>>? Errors { get; set; }
        public string? TraceIdentifier { get; set; }
        public string? Path { get; set; }
    }
}
