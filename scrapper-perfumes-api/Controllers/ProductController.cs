using Microsoft.AspNetCore.Mvc;
using scrapper_perfumes_yves_common.Interfaces;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
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


    [HttpGet("reset-yves")]
    public IResult ResetYves()
    {
        _service.ResetYvesData();

        return Results.Ok("finish");
    }


    [HttpGet("bulk-airtable")]
    public async Task<IResult> BulkFromDatabaseToAirtable()
    {
        await _airtableService.BulkFromDatabaseToAirtable();

        return Results.Ok("bulk finish succesfully");
    }


    [HttpGet("get-airtable")]
    //[OutputCache(Duration = 30)]
    public async Task<IResult> GetAirtable()
    {
        var data = await _airtableService.GetAirtable();

        return Results.Ok(data);
    }
}