namespace scrapper_perfumes_yves_common.Configuration
{
    public class Config
    {
        public bool PrintDataInConsole { get; set; }
        public string DatabaseUrl { get; set; }
        public List<Site> Sites { get; set; }
    }

    public class Site
    {
        public string Section { get; set; }
        public string Url { get; set; }
        public bool Enable { get; set; }
    }
}
