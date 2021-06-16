using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using C_Sharp_Programming_Project;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace C_Sharp_Programming_Project_Test
{
    [TestClass]
    public class CSVFileHandlingTest
    {
        [TestMethod]
        public void ImportFileNameDoesExist()
        {
            CSVFileHandler fileHandler = new CSVFileHandler();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // location of needed test csv
            openFileDialog.FileName = @"C:\Users\marcu\Downloads\stocklist.csv";
            var file = fileHandler.Import(openFileDialog);
            Assert.IsTrue(file != null);
        }
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ImportFileNameDoesNotExist()
        {
            CSVFileHandler fileHandler = new CSVFileHandler();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // fake csv location
            openFileDialog.FileName = @"C:\Users\marcu\Downloads\fake.csv";
            fileHandler.Import(openFileDialog);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ImportFileNameNullOrEmpty()
        {
            CSVFileHandler fileHandler = new CSVFileHandler();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // fake csv location
            openFileDialog.FileName = "";
            fileHandler.Import(openFileDialog);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExportFileLocationIsNullOrEmpty()
        {
            CSVFileHandler fileHandler = new CSVFileHandler();
            DataGridView dataGridView = new DataGridView();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // empty file location
            saveFileDialog.FileName = "";
            fileHandler.Export(dataGridView, saveFileDialog);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ExportDataGridViewIsNull()
        {
            CSVFileHandler fileHandler = new CSVFileHandler();
            // null data grid view
            DataGridView dataGridView = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // empty filename aswell so we dont export a file of nothing some where
            saveFileDialog.FileName = "";
            fileHandler.Export(dataGridView, saveFileDialog);
        }
    }
}
