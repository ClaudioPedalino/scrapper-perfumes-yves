using AirtableApiClient;
using Microsoft.Extensions.Options;
using scrapper_perfumes_yves_common.Consts;
using scrapper_perfumes_yves_common.Helpers;
using scrapper_perfumes_yves_common.Interfaces;
using scrapper_perfumes_yves_common.ServiceConfiguration;

namespace scrapper_perfumes_yves_common.Services
{
    public sealed class AirtableService : IAirtableService
    {
        private readonly IOptionsSnapshot<Configuration> _configuration;
        private readonly IProductService _productService;

        public AirtableService(IOptionsSnapshot<Configuration> configuration, IProductService productService)
        {
            _configuration = configuration;
            _productService = productService;
        }

        public async Task<List<AirtableRecord>> GetAirtable()
        {
            string? offset = default;
            string? errorMessage;
            var records = new List<AirtableRecord>();

            using (AirtableBase airtableBase = AirtableHelper.GetConnection(_configuration.Value))
            {
                do
                {
                    var response = await airtableBase.ListRecords(
                        tableName: Airtable.ProdcutTableName,
                        offset: offset,
                        fields: default, // new List<string> { "Name", "Notes" },
                        filterByFormula: default,
                        maxRecords: default,
                        pageSize: default,
                        sort: default,
                        view: default);

                    if (response.Success)
                    {
                        records.AddRange(response.Records.ToList());
                        offset = response.Offset;
                    }
                    else if (response.AirtableApiError is not null)
                    {
                        errorMessage = response.AirtableApiError.ErrorMessage;
                        if (response.AirtableApiError is AirtableInvalidRequestException)
                        {
                            errorMessage += "\nDetailed error message: ";
                            errorMessage += response.AirtableApiError.DetailedErrorMessage;
                        }
                        Console.WriteLine(errorMessage);
                        break;
                    }
                    else
                    {
                        errorMessage = "Unknown error";
                        Console.WriteLine(errorMessage);
                        break;
                    }
                } while (offset != null);
            }

            return records;
        }

        public async Task BulkFromDatabaseToAirtable()
        {
            var result = await _productService.GetAll();

            using AirtableBase airtableBase = AirtableHelper.GetConnection(_configuration.Value);

            foreach (var chunckList in result.Chunk(Airtable.MaxSizeCreationBatch))
            {
                Fields[] fields = new Fields[chunckList.Length];
                int counter = 0;

                foreach (var item in chunckList)
                {
                    fields[counter] = new Fields();
                    fields[counter].AddField("Id", item.Id);
                    fields[counter].AddField("Name", item.Name);
                    fields[counter].AddField("Price", item.Price);
                    fields[counter].AddField("PriceReseller", item.PriceReseller);
                    fields[counter].AddField("HasStock", item.HasStock);
                    fields[counter].AddField("DetailUrl", item.DetailUrl);
                    fields[counter].AddField("ImageUrl", item.ImageUrl);
                    fields[counter].AddField("Section", item.Section);
                    fields[counter].AddField("ResellerDiscount", item.ResellerDiscount);
                    fields[counter].AddField("CreatedAt", item.CreatedAt);

                    counter++;
                }

                await airtableBase.CreateMultipleRecords(Airtable.ProdcutTableName, fields, true);
            }
        }

        public async Task ResetAirtableData()
        {
            var data = await GetAirtable();

            using AirtableBase airtableBase = AirtableHelper.GetConnection(_configuration.Value);

            await Parallel.ForEachAsync(data, async (item, _) =>
                await airtableBase.DeleteRecord(Airtable.ProdcutTableName, item.Id));
        }
    }
}


