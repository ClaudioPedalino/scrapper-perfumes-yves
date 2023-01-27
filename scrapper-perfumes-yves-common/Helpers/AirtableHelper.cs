using AirtableApiClient;
using scrapper_perfumes_yves_common.ServiceConfiguration;

namespace scrapper_perfumes_yves_common.Helpers
{
    public static class AirtableHelper
    {
        public static AirtableBase GetConnection(Configuration _configuration)
            => new AirtableBase(
                _configuration.DataConfiguration.AritableApiKey,
                _configuration.DataConfiguration.AritableBaseId);


    }
}
