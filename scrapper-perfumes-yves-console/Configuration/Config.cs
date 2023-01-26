namespace scrapper_perfumes_yves_console.Configuration
{
    internal class Config
    {
        internal static readonly bool PrintDataInConsole = true;


        internal static readonly Dictionary<string, string> Sites = new Dictionary<string, string>()
        {
            //{ "Perfumes Masculinos","https://yvesdorgeval.empretienda.com.ar/perfumes/perfumes-masculinos"},
            //{ "Perfumes Femeninos","https://yvesdorgeval.empretienda.com.ar/perfumes/perfumes-femeninos"},
            { "Perfumes Unisex","https://yvesdorgeval.empretienda.com.ar/perfumes/perfumes-unisex"},
            { "Probadores","https://yvesdorgeval.empretienda.com.ar/perfumes/probadores"},
        };
    }
}
