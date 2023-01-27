namespace scrapper_perfumes_yves_common.Extension
{
    internal static class StringExtension
    {
        internal static decimal GetPriceFromHtmlPriceInput(this string htmlTagPrice)
        {
            int index = htmlTagPrice.IndexOf(",");
            if (index >= 0)
                htmlTagPrice = htmlTagPrice.Substring(0, index);

            var price = htmlTagPrice.Replace("$", string.Empty);
            var price2 = price.Replace(".", string.Empty);
            var price3 = Convert.ToDecimal(price2);

            return price3;
        }
    }
}
