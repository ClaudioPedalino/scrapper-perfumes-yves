using Microsoft.AspNetCore.Mvc;
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
}