using System.Windows.Forms;
using System.IO;

namespace C_Sharp_Programming_Project
{
    class StockListController
    {
        StockListCSVHandler stockListCSVHandler = new StockListCSVHandler();
        FileExplorerDialogManager fileExplorerDialogManager = new FileExplorerDialogManager();
        // arrays are used to store the readonly coulmn and editable column indexs for easy expandability
        int[] ReadOnlyColumnIndexes = new int[] { 0, 1, 3 };
        int[] EditableColumnIndexes = new int[] { 2 };
        public void Import(DataGridView dataGridView)
        {
            // imports the cv file and if there are any errors reports it to the user
            try
            {
                if (fileExplorerDialogManager.FileExplorerDialog.ShowDialog() == DialogResult.OK)
                {
                    dataGridView.DataSource = stockListCSVHandler.Import(fileExplorerDialogManager.FileExplorerDialog);
                    fileExplorerDialogManager.SaveFileDialog.FileName = fileExplorerDialogManager.FileExplorerDialog.FileName;
                    // also sets the apropiate columns to read only
                    DataGridViewUtilities.SetColumsToReadOnly(dataGridView, ReadOnlyColumnIndexes);
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show($"Importing Failed: '{e}'", "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show($"Importing Failed: '{e}'", "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Importing Failed", "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Export(DataGridView dataGridView)
        {
            //  exports the csv and if there are any errors reports it to the user
            try
            {
                if (dataGridView.DataSource != null)
                {
                    if (fileExplorerDialogManager.SaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        stockListCSVHandler.Export(dataGridView, fileExplorerDialogManager.SaveFileDialog);
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show($"Exporting Failed: '{e}'", "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                 MessageBox.Show("Exporting Failed", "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ValidateData(DataGridViewCellValidatingEventArgs cellEvent)
        {
            DataGridViewUtilities.CheckIfDataIsInteger(cellEvent, EditableColumnIndexes);
        }
        public void OnExit(FormClosingEventArgs closeEvent, DataGridView dataGridView)
        {
            // asks the user if they are sure they want to exit and save on exit
            if (dataGridView.DataSource != null)
            {
                DialogResult confirmationResult = MessageBox.Show("Do you want to save?", "Exit Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (confirmationResult)
                {
                    case DialogResult.Cancel:
                        //stops the application from closing
                        closeEvent.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        Export(dataGridView);
                        break;
                    default:
                        break;
                }
            }
        }
        public StockListController()
        {
            // sets up the file explorers to csv
            fileExplorerDialogManager.SetOpenFileDialogToCSV();
            fileExplorerDialogManager.SetSaveFileDialogToCSV();
        }
    }
}
