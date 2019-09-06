using DevExpress.XtraGrid.Views.Grid;

namespace Fittings.UI
{
    public static class GridControlUIModifications
    {
        public static int GetIndexRowOfItem(object id, GridView gridView)
        {
            int result = -1;
            int rowCount = gridView.DataRowCount;

            for (int i = 0; i < rowCount; i++)
            {
                object b = gridView.GetRowCellValue(i, "Id");
                if (b != null && b.Equals(id))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public static int GetIndexRowOfItem(int id, GridView gridView)
        {
            int result = -1;
            int rowCount = gridView.DataRowCount;

            for (int i = 0; i < rowCount; i++)
            {
                object b = gridView.GetRowCellValue(i, "Id");
                if (b != null && b.Equals(id))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public static int GetIndexRowOfItem(object value, string fieldName, GridView gridView)
        {
            int result = -1;
            int rowCount = gridView.DataRowCount;

            for (int i = 0; i < rowCount; i++)
            {
                object b = gridView.GetRowCellValue(i, fieldName);
                if (b != null && b.Equals(value))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
    }
}
