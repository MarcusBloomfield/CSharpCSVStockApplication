using System.Data;
using System.IO;
using System.Windows.Forms;

namespace C_Sharp_Programming_Project
{
    class StockListCSVHandler : CSVFileHandler
    {
        public override DataTable Import(OpenFileDialog fileExplorerDialog)
        {
            // overrides the import method from csvfilehanlder for the purpose of setting columns to the correct data type
            DataTable dataTable = new DataTable();
            FilePath = fileExplorerDialog.FileName;
            StreamReader streamReader = new StreamReader(fileExplorerDialog.OpenFile());
            SetupColumns(dataTable, streamReader);
            SetColumnTypes(dataTable);
            SetupRows(dataTable, streamReader);
            streamReader.Close();
            streamReader.Dispose();
            return dataTable;
        }
        public virtual void SetColumnTypes(DataTable dataTable)
        {
            dataTable.Columns[0].DataType = typeof(string);
            dataTable.Columns[1].DataType = typeof(string);
            dataTable.Columns[2].DataType = typeof(int);
            dataTable.Columns[3].DataType = typeof(string);
        }
    }
}
