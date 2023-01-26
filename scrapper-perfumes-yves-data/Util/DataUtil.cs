using scrapper_perfumes_yves_data.Models;

namespace scrapper_perfumes_yves_data.Util
{
    public static class DataUtil
    {
        public static List<Item> GetItems()
        {
            var dataContext = new DataContext();
            return dataContext.Items.ToList();
        }

        public static void SaveItemsBySite(string section, List<Item> scrappedItems)
        {
            var dataContext = new DataContext();

            dataContext.ResetSection(section);

            #region TODO: Upsert (add new, update existing but modified, delete new non-existing
            //var currentDbData = dataContext.Items.ToList();
            //var updated = currentDbData.Intersect(scrappedItems, new YourEqualityComparer());
            //int updatedCount = updated.Count();
            #endregion

            dataContext.Items.AddRange(scrappedItems);
            dataContext.SaveChanges();
        }

        public static void ResetSection(this DataContext dataContext, string section)
        {
            foreach (var entity in dataContext.Items.Where(x => x.Tag == section))
                dataContext.Items.Remove(entity);

            dataContext.SaveChanges();
        }


        public static void ResetData(this DataContext dataContext)
        {
            foreach (var entity in dataContext.Items)
                dataContext.Items.Remove(entity);
            dataContext.SaveChanges();
        }
    }
}
