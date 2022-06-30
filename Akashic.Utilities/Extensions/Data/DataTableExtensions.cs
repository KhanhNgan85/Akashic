/*****************************************************************************/
/* Build : 10-Jun-2015                                                       */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.Data;

namespace Akashic.Utilities.Extensions.Data
{
    public static class DataTableExtensions
    {
        public static bool IsValid(this DataTable table)
        {
            var result = (table != null) && (table.Rows.Count > 0);
            return result;
        }

        public static DataTable Sort(this DataTable table, string column, bool isDesc = false)
        {
            var view = table.DefaultView;
            view.Sort = string.Format("{0} {1}", column, (isDesc ? "desc" : "asc"));

            var result = view.ToTable();
            return result;
        }

        public static DataTable Filter(this DataTable table, string filter)
        {
            var view = table.DefaultView;
            view.RowFilter = filter;

            var result = view.ToTable();
            return result;
        }
    }
}
