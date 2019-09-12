using System.Collections.Generic;

namespace MahadevHWBillingApp.Helper
{
    public static class Query
    {
        public static readonly string GetItem = "Select * From Items";

        public static string DeleteItem(IList<int> ids)
        {
            return $"Delete From Item Where Id in ({string.Join(",", ids)})";
        } 
    }
}