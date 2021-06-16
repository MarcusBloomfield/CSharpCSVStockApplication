using System;
using System.Windows.Forms;

namespace C_Sharp_Programming_Project
{
    public partial class StockApplication : Form
    {
        StockListController StockListController = new StockListController();
        public StockApplication()
        {
            InitializeComponent();
        }
        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs cellEvent)
        {
            StockListController.ValidateData(cellEvent);
        }
        private void SaveButton_Click(object sender, EventArgs buttonEvent)
        {
            StockListController.Export(dataGridView);
        }
        private void OpenButton_Click(object sender, EventArgs buttonEvent)
        {
            StockListController.Import(dataGridView);
        }
        protected override void OnFormClosing(FormClosingEventArgs closeEvent)
        {
            StockListController.OnExit(closeEvent, dataGridView);
        }
    }
}
