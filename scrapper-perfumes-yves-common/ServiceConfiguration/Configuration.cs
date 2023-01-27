namespace scrapper_perfumes_yves_common.ServiceConfiguration
{
    public class Configuration
    {
        public DataConfiguration DataConfiguration { get; set; }
        public bool LogIn { get; set; }
        public bool PrintDataInConsole { get; set; }
        public List<Site> Sites { get; set; }
    }

    public class DataConfiguration
    {
        public bool DatabaseEnabled { get; set; }
        public string DatabaseUrl { get; set; }
        public bool FileEnabled { get; set; }
        public string AritableApiKey { get; set; }
        public string AritableBaseId { get; set; }
    }

    public class Site
    {
        public string Section { get; set; }
        public string Url { get; set; }
    }
}
