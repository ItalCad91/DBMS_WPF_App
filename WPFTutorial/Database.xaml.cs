using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WPFTutorial
{
    public partial class Database : Window
    {
        private DataTable dataT;

        public Database()
        {
            InitializeComponent();
            BindDataGrid();
        }

        private void BindDataGrid()
        {
            SqlConnection connTable = new SqlConnection();
            connTable.ConnectionString = ConfigurationManager.ConnectionStrings["connectionTable"].ConnectionString;
            connTable.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM dbo.ProductInventory";
            cmd.Connection = connTable;
            SqlDataAdapter dataA = new SqlDataAdapter(cmd);
            dataT = new DataTable("ProductInventory");
            dataA.Fill(dataT);

            Inventory.ItemsSource = dataT.DefaultView;
        }

        private void EditButton(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = Inventory.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                // Open the "Edit" window as a dialog and pass the selected row and this Database window reference
                Edit editWindow = new Edit(selectedRow, this);
                editWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select an item to edit.");
            }
        }

        public void UpdateDataGrid()
        {
            // Refresh the DataGrid by fetching the updated data from the database
            dataT.Clear();
            BindDataGrid();
        }
    }
}
