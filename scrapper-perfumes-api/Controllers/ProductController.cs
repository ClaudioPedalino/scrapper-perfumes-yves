using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using scrapper_perfumes_yves_common.Interfaces;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _service;

    public ProductController(ILogger<ProductController> logger, IProductService service)
    {
        _logger = logger;
        _service = service;
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
}