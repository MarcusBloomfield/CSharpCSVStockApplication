using System;
using System.Windows.Forms;

namespace C_Sharp_Programming_Project
{
    public static class DataGridViewUtilities
    {
        public static void CheckIfDataIsInteger(DataGridViewCellValidatingEventArgs cellEvent, int[] columnIndexes)
        {
            // on cell validate if contains non numeric value dont set changes
            for (int k = 0; k < columnIndexes.Length; k++)
            {
                if (cellEvent.ColumnIndex == columnIndexes[k])
                {
                    string cellData = Convert.ToString(cellEvent.FormattedValue);
                    // trys to parse the cell data to int and if not prevents the entery of the invalid data
                    if (!int.TryParse(cellData, out int i))
                    {
                        MessageBox.Show("Entering non-interger values is not allowed", "Cell data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cellEvent.Cancel = true;
                    }
                }
            }
        }
        public static void SetColumsToReadOnly(DataGridView dataGridView, int[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                dataGridView.Columns[columns[i]].ReadOnly = true;
            }
        }
    }
}
