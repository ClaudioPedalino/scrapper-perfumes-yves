using AirtableApiClient;

namespace scrapper_perfumes_yves_common.Interfaces
{
    public interface IAirtableService
    {
        Task<List<AirtableRecord>> GetAirtable();
        Task BulkFromDatabaseToAirtable();
    }
}
