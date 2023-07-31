using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFTutorial
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login dashbord = new Login();
            dashbord.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseManagementSystemApp;Integrated Security=True";

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }

                    string query = "INSERT INTO userAuth (UserName, Email, Password) VALUES (@Username, @Email, @Password)";

                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.Text;
                        sqlCmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                        sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        int rowsAffected = sqlCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration Successful!");
                            Login dashboard = new Login();
                            dashboard.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Registration Failed. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



    }
}
