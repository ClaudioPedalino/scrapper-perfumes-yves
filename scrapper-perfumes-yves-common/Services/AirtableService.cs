using AirtableApiClient;
using Microsoft.Extensions.Options;
using scrapper_perfumes_yves_common.Interfaces;
using scrapper_perfumes_yves_common.ServiceConfiguration;

namespace scrapper_perfumes_yves_common.Services
{
    public class AirtableService : IAirtableService
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
            string offset = default;
            string errorMessage = default;
            var records = new List<AirtableRecord>();

            using (AirtableBase airtableBase = AirtableHelper.GetConnection(_configuration.Value))
            {
                do
                {
                    Task<AirtableListRecordsResponse> task = airtableBase.ListRecords(
                    tableName: "productos",
                    offset: offset,
                    fields: default, // new List<string> { "Name", "Notes" },
                    filterByFormula: default,
                    maxRecords: default,
                    pageSize: default,
                    sort: default,
                    view: default);

                    AirtableListRecordsResponse response = await task;

                    if (response.Success)
                    {
                        records.AddRange(response.Records.ToList());
                        offset = response.Offset;
                    }
                    else if (response.AirtableApiError is AirtableApiException)
                    {
                        errorMessage = response.AirtableApiError.ErrorMessage;
                        if (response.AirtableApiError is AirtableInvalidRequestException)
                        {
                            errorMessage += "\nDetailed error message: ";
                            errorMessage += response.AirtableApiError.DetailedErrorMessage;
                        }
                        break;
                    }
                    else
                    {
                        errorMessage = "Unknown error";
                        break;
                    }
                } while (offset != null);
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Error reporting
            }
            else
            {
                // Do something with the retrieved 'records' and the 'offset'
                // for the next page of the record list.
            }

            return records;

        }

        public async Task BulkFromDatabaseToAirtable()
        {
            var result = _productService.GetAll();

            using (AirtableBase airtableBase = AirtableHelper.GetConnection(_configuration.Value))
            {
                foreach (var chunckList in result.Chunk(10))
                {
                    Fields[] fields = new Fields[chunckList.Count()];
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

                    Task<AirtableCreateUpdateReplaceMultipleRecordsResponse> task =
                        airtableBase.CreateMultipleRecords("productos", fields, true);

                    var response = await task;

                    if (!response.Success)
                    {
                        string errorMessage = null;
                        if (response.AirtableApiError is AirtableApiException)
                        {
                            errorMessage = response.AirtableApiError.ErrorMessage;
                            if (response.AirtableApiError is AirtableInvalidRequestException)
                            {
                                errorMessage += "\nDetailed error message: ";
                                errorMessage += response.AirtableApiError.DetailedErrorMessage;
                            }
                        }
                        else
                        {
                            errorMessage = "Unknown error";
                        }
                        // Report errorMessage
                    }
                    else
                    {
                        AirtableRecord[] records = response.Records;
                        // Do something with your created records.
                    }
                }
            }
        }
    }
}

public static class AirtableHelper
{

    public static AirtableBase GetConnection(Configuration _configuration)
        => new AirtableBase(
            _configuration.DataConfiguration.AritableApiKey,
            _configuration.DataConfiguration.AritableBaseId);
}
