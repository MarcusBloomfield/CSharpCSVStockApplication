using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Sharp_Programming_Project
{
     static class DataViewTableManager
    {
        public static void checkForaer(DataGridViewCellValidatingEventArgs e)
        {
            // on cell validate if contains non numeric value dont set changes
            if (e.ColumnIndex == 2)
            {
                if (!int.TryParse(Convert.ToString(e.FormattedValue), out int i))
                {
                    MessageBox.Show("Entering non-numeric values is not allowed", "Cell data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }
    }
}
