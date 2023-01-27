using Microsoft.AspNetCore.Mvc;
using scrapper_perfumes_yves_common.Interfaces;

namespace scrapper_perfumes_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ScrapperController : ControllerBase
    {
        private readonly ILogger<ScrapperController> _logger;
        private readonly IScrapperService _service;

        public ScrapperController(ILogger<ScrapperController> logger, IScrapperService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "bulk-yves")]
        public IResult BulkYvesSiteToDatabase()
        {
            _service.ScrapYvesSites();
            return Results.Ok("bulk process finished succesfully");
        }
    }
}