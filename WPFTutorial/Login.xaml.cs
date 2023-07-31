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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseManagementSystemApp;Integrated Security=True");
            try 
            {
                if(sqlCon.State == System.Data.ConnectionState.Closed) 
                {
                    sqlCon.Open();
                }

                String query = "SELECT COUNT (1) FROM userAuth WHERE Username=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtUserName.Text);
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if(count == 1) 
                {
                 MainWindow dashbord = new MainWindow();
                    dashbord.Show();
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("Incorrect Username or Password!");
                }


            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                sqlCon.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register dashbord = new Register();
            dashbord.Show();
            this.Close();
        }
    }
}
