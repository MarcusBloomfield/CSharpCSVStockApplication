using System.Windows.Forms;

namespace C_Sharp_Programming_Project
{
    class FileExplorerDialogManager
    {
        OpenFileDialog openFileDialog = new OpenFileDialog(); 
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public OpenFileDialog FileExplorerDialog { get => openFileDialog; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog; }

        public void SetOpenFileDialogToCSV()
        {
            // sets the windows open dialog to csv
            openFileDialog.Title = "Open Csv Files";
            openFileDialog.DefaultExt = "csv";
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 2;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
        }
        public void SetSaveFileDialogToCSV()
        {
            // sets the windows save dialog to csv
            saveFileDialog.Title = "Save Csv Files";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 2;
        }
    }
}
