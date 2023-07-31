using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class Database : Window
    {
        public Database()
        {
            InitializeComponent();
            binddatagrid();
        }

        private void binddatagrid()
        {
            SqlConnection connTable = new SqlConnection(); //CREATES A CONNECTION WITH SQL SERVERS
            connTable.ConnectionString = ConfigurationManager.ConnectionStrings["connectionTable"].ConnectionString;// CONNECTION STRING ACCESED FROM THE App.config FILE
            connTable.Open();//CONNECTION IS OPEN
            SqlCommand cmd = new SqlCommand();//CREATES AN SQL COMMAND
            cmd.CommandText = "SELECT * FROM dbo.ProductInventory";
            cmd.Connection = connTable;
            SqlDataAdapter dataA = new SqlDataAdapter(cmd);
            DataTable dataT = new DataTable("ProductInventory");
            dataA.Fill(dataT);

            Inventory.ItemsSource = dataT.DefaultView;
        }
    }
}
