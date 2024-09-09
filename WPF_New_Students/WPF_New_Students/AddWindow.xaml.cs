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

namespace WPF_New_Students
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrWhiteSpace(FNameTextBox.Text)) && !(String.IsNullOrWhiteSpace(LNameTextBox.Text)) && !(String.IsNullOrWhiteSpace(SemesterComboBox.Text)))
            {
                string ConString = Properties.Settings.Default.StudentsConnectionString;
                string sqlcmd = string.Empty;
                SqlConnection cn_connection = new SqlConnection(ConString);
                if (cn_connection.State != System.Data.ConnectionState.Open)
                {
                    cn_connection.Open();
                }

                sqlcmd = "INSERT INTO tbl_Students (FirstName, LastName, Semester) VALUES ('" + FNameTextBox.Text + "','" + LNameTextBox.Text + "','" + SemesterComboBox.Text + "')";

                SqlCommand cmd_Command = new SqlCommand(sqlcmd, cn_connection);
                cmd_Command.ExecuteNonQuery();
                MessageBox.Show("Dodano studenta.");
                Close();
            }
            else
            {
                MessageBox.Show("Należy wypełnić wszystkie pola tekstowe!");
            }
        }
    }
}
