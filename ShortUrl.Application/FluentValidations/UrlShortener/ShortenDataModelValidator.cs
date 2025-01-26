
namespace ShortUrl.Application.FluentValidations.UrlShortener
{
    public class ShortenDataModelValidator:AbstractValidator<ShortenDataModel>
    {
        public ShortenDataModelValidator()
        {
            RuleFor(x => x.LongUrl)
                .NotEmpty()
                .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
                .WithMessage("Url provided is invalid");
        }
    }
}
