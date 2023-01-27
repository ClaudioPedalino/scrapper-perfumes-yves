using scrapper_perfumes_yves_common.Models;

namespace scrapper_perfumes_yves_common.Extensions
{
    public sealed class YourEqualityComparer : IEqualityComparer<Product>
    {

        #region IEqualityComparer<ThisClass> Members


        public bool Equals(Product x, Product y)
        {
            //no null check here, you might want to do that, or correct that to compare just one part of your object
            return x.Name != y.Name || x.Price != y.Price;
        }


        public int GetHashCode(Product obj)
        {
            unchecked
            {
                var hash = 17;
                //same here, if you only want to get a hashcode on a, remove the line with b
                hash = hash * 23 + obj.Name.GetHashCode();
                hash = hash * 23 + obj.Name.GetHashCode();

                return hash;
            }
        }

        #endregion
    }

}
