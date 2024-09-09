using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WPF_New_Students
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindowLoaded;

        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadDataToGrid();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToGrid();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            string ConString = Properties.Settings.Default.StudentsConnectionString;
            string sqlcmd = string.Empty;
            SqlConnection cn_connection = new SqlConnection(ConString);
            if(cn_connection.State != System.Data.ConnectionState.Open)
            {
                cn_connection.Open();
            }
            sqlcmd = "SELECT * FROM tbl_Students";
            DataTable dt = new DataTable("Students");
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd, cn_connection);
            sda.Fill(dt);
            grdStudents.ItemsSource = dt.DefaultView;
        }

        private void grdStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           /* DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected != null)
            {
                FNameTextBox.Text = row_selected["FirstName"].ToString();
                LNameTextBox.Text = row_selected["LastName"].ToString();
                int comboindex = Convert.ToInt32(row_selected["Semester"].ToString());
                SemesterComboBox.SelectedIndex = --comboindex;
            }*/
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string ConString = Properties.Settings.Default.StudentsConnectionString;
            string sqlcmd = string.Empty;
            SqlConnection cn_connection = new SqlConnection(ConString);
            if (cn_connection.State != System.Data.ConnectionState.Open)
            {
                cn_connection.Open();
            }

            DataRowView row = grdStudents.SelectedItem as DataRowView;
            if (row != null)
            {
                MessageBoxResult boxResult = MessageBox.Show("Jesteś pewien?", "Potwierdzenie usunięcia", MessageBoxButton.YesNo);
                if (boxResult == MessageBoxResult.Yes)
                {
                    string IdtoDelete = row["Id"].ToString();
                    sqlcmd = "DELETE FROM tbl_Students WHERE(Id = " + IdtoDelete + ")";
                    SqlCommand cmd_Command = new SqlCommand(sqlcmd, cn_connection);
                    cmd_Command.ExecuteNonQuery();
                    LoadDataToGrid();
                    ResetButtons();
                }
            }
            else
            {
                MessageBox.Show("Należy wybrać rząd!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = grdStudents.SelectedItem as DataRowView;
            if (row != null)
            {
                string IdtoDelete = row["Id"].ToString();
                var updateWindow = new UpdateWindow(IdtoDelete, row["FirstName"].ToString(), row["LastName"].ToString(), row["Semester"].ToString());
                updateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Należy wybrać rząd!");
            }
        }

        private void ResetButtons()
        {
            /*FNameTextBox.Text = "";
            LNameTextBox.Text = "";
            SemesterComboBox.SelectedIndex = 0;*/
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }
    }
}
