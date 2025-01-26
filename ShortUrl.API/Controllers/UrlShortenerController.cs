
namespace ShortUrl.API.Controllers
{
    /// <summary>
    /// Url Shortener Controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UrlShortenerController:ControllerBase
    {
        private readonly IUrlShortenerService urlShortenerService;

        public UrlShortenerController(IUrlShortenerService urlShortenerService)
        {
            this.urlShortenerService = urlShortenerService;
        }


        /// <summary>
        /// Access Url
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        [HttpGet("{shortUrl}")]
        //[Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async ValueTask<ActionResult> Get([FromRoute] string shortUrl)
        {
            var response = await urlShortenerService.GetLongUrlAsync(shortUrl);

            return Redirect(response.Data);
        }

        /// <summary>
        /// Shorten Url
        /// </summary>
        /// <param name="shorten"></param>
        /// <returns></returns>
        [HttpPost("Shorten")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async ValueTask<ActionResult> Post([FromBody] ShortenDataModel shorten)
        { 
            var response =  await urlShortenerService.ShortenUrlAsync(shorten);

            return Ok(response);
        }

        /// <summary>
        /// Get Request Count
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        [HttpGet("Stats/{shortUrl}")]
        [Produces("application/json", Type = typeof(ResponseModel<dynamic>))]
        public async ValueTask<ActionResult> GetStats([FromRoute] string shortUrl)
        {
            var response = await urlShortenerService.GetAccessCountAsync(shortUrl);

            return Ok(response);
        }
    }
}
