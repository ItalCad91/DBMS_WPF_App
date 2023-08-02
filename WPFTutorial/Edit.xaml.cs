using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFTutorial
{
    public partial class Edit : Window
    {
        public bool ChangesMade { get; private set; }
        public Dictionary<string, object> NewValues { get; private set; }
        private DataRowView selectedRow;
        private SqlConnection connection;
        private string connectionString = ConfigurationManager.ConnectionStrings["connectionTable"].ConnectionString;

        public Edit(DataRowView selectedRow, Database database)
        {
            InitializeComponent();

            // Initialize the NewValues dictionary
            NewValues = new Dictionary<string, object>();

            // Store the selected row
            this.selectedRow = selectedRow;

            // Populate the text boxes with the item details from the selected row
            if (selectedRow != null)
            {
                ItemIdTextBox.Text = selectedRow["ID"].ToString();
                NameTextBox.Text = selectedRow["Name"].ToString();
                LotNumberTextBox.Text = selectedRow["LotNumber"].ToString();
                AvailableQuantityTextBox.Text = selectedRow["AvailableQuantity"].ToString();
                PriceTextBox.Text = selectedRow["Price"].ToString();
                QuantitySoldTextBox.Text = selectedRow["QuantitySold"].ToString();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Store the modified values in the NewValues dictionary
            NewValues["Name"] = NameTextBox.Text;
            NewValues["LotNumber"] = LotNumberTextBox.Text;
            NewValues["AvailableQuantity"] = AvailableQuantityTextBox.Text;
            NewValues["Price"] = PriceTextBox.Text;
            NewValues["QuantitySold"] = QuantitySoldTextBox.Text;

            // Update the database
            if (UpdateDatabase())
            {
                // Set ChangesMade to true to indicate that changes were made
                ChangesMade = true;
            }

            // Close the "Edit" window
            Database dashbord = new Database();
            dashbord.Show();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the "Edit" window without saving changes
            Database database = new Database();
            database.Show();
            this.Close();
        }

        private bool UpdateDatabase()
        {
            try
            {
                // Open the connection
                connection = new SqlConnection(connectionString);
                connection.Open();

                // Prepare the update query
                string updateQuery = "UPDATE dbo.ProductInventory " +
                                     "SET Name = @Name, [LotNumber] = @LotNumber, [AvailableQuantity] = @AvailableQuantity, " +
                                     "Price = @Price, [QuantitySold] = @QuantitySold " +
                                     "WHERE ID = @ID";

                // Create and execute the command
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", NewValues["Name"]);
                    command.Parameters.AddWithValue("@LotNumber", NewValues["LotNumber"]);
                    command.Parameters.AddWithValue("@AvailableQuantity", NewValues["AvailableQuantity"]);
                    command.Parameters.AddWithValue("@Price", NewValues["Price"]);
                    command.Parameters.AddWithValue("@QuantitySold", NewValues["QuantitySold"]);
                    command.Parameters.AddWithValue("@ID", ItemIdTextBox.Text);

                    command.ExecuteNonQuery();
                }

                // Close the connection
                connection.Close();

                // Update the DataRowView with the new values
                if (selectedRow != null)
                {
                    selectedRow["Name"] = NewValues["Name"];
                    selectedRow["LotNumber"] = NewValues["LotNumber"];
                    selectedRow["AvailableQuantity"] = NewValues["AvailableQuantity"];
                    selectedRow["Price"] = NewValues["Price"];
                    selectedRow["QuantitySold"] = NewValues["QuantitySold"];
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating the database: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Confirm with the user before deleting the item
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                int itemId;
                if (int.TryParse(ItemIdTextBox.Text, out itemId))
                {
                    // Implement the logic to delete the item from the database
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionTable"].ConnectionString))
                        {
                            conn.Open();

                            string deleteQuery = "DELETE FROM dbo.ProductInventory WHERE ID = @ItemId";
                            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@ItemId", itemId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Close the "Edit" window
                        Database dashbord = new Database();
                        dashbord.Show();
                        this.Close(); ;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while deleting the item: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Item ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddNewItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement the logic to add a new item to the database
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionTable"].ConnectionString))
                {
                    conn.Open();

                    string insertQuery = "INSERT INTO dbo.ProductInventory (Name, [LotNumber], [AvailableQuantity], Price, [QuantitySold]) " +
                                         "VALUES (@Name, @LotNumber, @AvailableQuantity, @Price, @QuantitySold)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);
                        cmd.Parameters.AddWithValue("@LotNumber", LotNumberTextBox.Text);
                        cmd.Parameters.AddWithValue("@AvailableQuantity", AvailableQuantityTextBox.Text);
                        cmd.Parameters.AddWithValue("@Price", PriceTextBox.Text);
                        cmd.Parameters.AddWithValue("@QuantitySold", QuantitySoldTextBox.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Item added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the "Edit" window
                Database dashbord = new Database();
                dashbord.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the new item: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
