using scrapper_perfumes_yves_api;
using scrapper_perfumes_yves_data.Util;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.MapGet("/get-products", () =>
{
    return DataUtil.GetItems();
})
.WithOpenApi();


app.MapGet("/get-overview", () =>
{
    var data = DataUtil.GetItems();

    return data
        .GroupBy(c => new { c.Tag, })
        .Select(gcs => new Overview()
        {
            Section = gcs.Key.Tag,
            Stock = gcs.Count(x => x.HasStock),
            NoStock = gcs.Count(x => !x.HasStock)
        });
})
.WithOpenApi();

app.Run();