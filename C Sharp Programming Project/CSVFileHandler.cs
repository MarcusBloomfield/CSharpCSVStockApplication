using System.Data;
using System.IO;
using System.Windows.Forms;

namespace C_Sharp_Programming_Project
{
    public class CSVFileHandler
    {
        string filePath = null;

        public string FilePath { get => filePath; set => filePath = value; }

        public virtual DataTable Import(OpenFileDialog FileExplorerDialog)
        {
            // Check if the result from the dailog box is vaild
            DataTable dataTable = new DataTable();
            // sets file path to the opened file name
            filePath = FileExplorerDialog.FileName;
            StreamReader streamReader = new StreamReader(FileExplorerDialog.OpenFile());
            SetupColumns(dataTable, streamReader);
            SetupRows(dataTable, streamReader);
            streamReader.Close();
            streamReader.Dispose();
            return dataTable;
        }
        public void SetupColumns(DataTable dataTable, StreamReader streamReader)
        {
            string[] lineData = ReadLine(streamReader);
            for (int i = 0; i < lineData.Length; i++)
            {
                // checks the first line in the csv and if they arent null or white space adds a column to the datatable
                if (!string.IsNullOrWhiteSpace(lineData[i])) dataTable.Columns.Add(lineData[i]);
            }
        }
        public void SetupRows(DataTable dataTable, StreamReader streamReader)
        {
            //loops through all of the lines in the csv file 
            while (!streamReader.EndOfStream)
            {
                // sends the line data to a string array
                string[] lineData = ReadLine(streamReader);
                if (CheckIfAllLinesContainData(dataTable,lineData))
                {
                    // imports each row if that rows cell is the same type as 
                    ImportDataThatIsSameTpyeAsColumn(lineData, dataTable);
                }
            }
        }
        bool CheckIfAllLinesContainData(DataTable dataTable, string[] lineData)
        {
            // if the line has less data then the data table has columns return false
            if (lineData.Length < dataTable.Columns.Count) return false;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                // if the line datas current element is null or white space return false
                if (string.IsNullOrWhiteSpace(lineData[i])) return false;
            }
            return true;
        }
        void ImportDataThatIsSameTpyeAsColumn(string[] lineData, DataTable dataTable)
        {
            // creates a new rows and adds it to the data table 
            DataRow dataRow = dataTable.Rows.Add();
            // sets the rows item array to have the same length as the columns array
            dataRow.ItemArray = new object[dataTable.Columns.Count];
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                // if the coulm type is int checks if the data array element is parsable  
                if (!string.IsNullOrWhiteSpace(lineData[i]))
                {
                    if (dataTable.Columns[i].DataType == typeof(int))
                    {
                        // if the array elemt is parsable sets it to k and if not sets it to zero
                        if (int.TryParse(lineData[i], out int k))
                        {
                            dataRow[i] = k;
                        }
                        else dataRow[i] = 0;
                    }
                    // if the column is not of the type int sets it to the array elemnt
                    else dataRow[i] = lineData[i];
                }
            }
        }
        string[] ReadLine(StreamReader streamReader)
        {
            // gets the line data from the streamreader and converts it to a string array
            string[] lineData = streamReader.ReadLine().Split(',');
            return lineData;
        }
        public void Export(DataGridView dataGridView, SaveFileDialog saveFileDialog)
        {
            // gets the file path from the save file dialog
            filePath = saveFileDialog.FileName;
            // Add all the data from the table to a string
            string csv = string.Empty;

            // add the Header row
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                csv += dataGridView.Columns[i].HeaderText + ',';
            }
            // go to next line
            csv += "\r\n";
            // add all of the rows and collumns to the csv string 
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int k = 0; k < dataGridView.Rows[i].Cells.Count; k++)
                {
                    if (dataGridView.Rows[i].Cells[k].Value != null)
                    {
                        // adds the row data cell by cell to the csv spliting each element with a comma
                        csv += dataGridView.Rows[i].Cells[k].Value.ToString() + ',';
                    }
                }
                // creates a new line
                csv += "\r\n";
            }
            // overwrites the selected file
            File.WriteAllText(filePath, csv);
        }
    }
}
