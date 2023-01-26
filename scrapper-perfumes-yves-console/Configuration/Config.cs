namespace scrapper_perfumes_yves_console.Configuration
{
    internal class Config
    {
        internal static readonly bool PrintDataInConsole = false;


        internal static readonly Dictionary<string, string> Sites = new Dictionary<string, string>()
        {
            { "Perfumes Unisex","https://yvesdorgeval.empretienda.com.ar/perfumes/perfumes-unisex"},
            { "Perfumes masculinos","https://yvesdorgeval.empretienda.com.ar/perfumes/perfumes-masculinos"},
            { "Perfumes femeninos","https://yvesdorgeval.empretienda.com.ar/perfumes/perfumes-femeninos"},
            { "Probadores","https://yvesdorgeval.empretienda.com.ar/perfumes/probadores"},
        };
    }
}
