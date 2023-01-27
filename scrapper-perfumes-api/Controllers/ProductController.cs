using MethodTimer;
using Microsoft.AspNetCore.Mvc;
using scrapper_perfumes_yves_common.Interfaces;

namespace scrapper_perfumes_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;
        private readonly IAirtableService _airtableService;

        public ProductController(ILogger<ProductController> logger, IProductService service, IAirtableService airtableService)
        {
            _logger = logger;
            _service = service;
            _airtableService = airtableService;
        }


        [HttpGet]
        [Time]
        //[OutputCache(Duration = 30)]
        public IResult GetAll()
        {
            var result = _service.GetAll();

            return Results.Ok(result);
        }


        [HttpGet("overview")]
        public IResult GetOverview()
        {
            var result = _service.GetOverview();

            return Results.Ok(result);
        }


        [HttpGet("reset-database")]
        public IResult ResetDatabase()
        {
            _service.ResetDatabase();

            return Results.Ok("finish");
        }


        [HttpGet("reset-airtable")]
        [Time]
        public async Task<IResult> ResetAirtableData()
        {
            await _airtableService.ResetAirtableData();

            return Results.Ok("finish");
        }


        [HttpGet("bulk-airtable")]
        [Time]
        public async Task<IResult> BulkFromDatabaseToAirtable()
        {
            await _airtableService.BulkFromDatabaseToAirtable();

            return Results.Ok("bulk finish succesfully");
        }


        [HttpGet("get-airtable")]
        [Time]
        //[OutputCache(Duration = 30)]
        public async Task<IResult> GetAirtable()
        {
            var data = await _airtableService.GetAirtable();

            return Results.Ok(data);
        }
    }
}