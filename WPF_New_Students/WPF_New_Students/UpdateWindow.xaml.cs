using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System;

namespace WPF_New_Students
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        string idToDelete;
        string name;
        string lastName;
        string semester;
        public UpdateWindow()
        {
            InitializeComponent();
        }

        public UpdateWindow(string idToDelete, string name, string lastName, string semester)
        {
            InitializeComponent();
            this.idToDelete = idToDelete;

            this.name = name;
            this.lastName = lastName;
            this.semester = semester;

            Loaded += UpdateWindowLoaded;
        }

        private void UpdateWindowLoaded(object sender, RoutedEventArgs ev)
        {
            //Console.WriteLine($"name {name}, lastname {lastName}, semester {semester}");
            CurrentStudentData.Text = $"Imię: {name}, Nazwisko: {lastName}, Semestr: {semester}";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string ConString = Properties.Settings.Default.StudentsConnectionString;
            string sqlcmd = string.Empty;
            SqlConnection cn_connection = new SqlConnection(ConString);
            if (cn_connection.State != ConnectionState.Open)
            {
                cn_connection.Open();
            }


            sqlcmd = "UPDATE tbl_Students set FirstName='" + FNameTextBox.Text + "',LastName='" + LNameTextBox.Text + "',Semester='" + SemesterComboBox.Text + "' WHERE Id ='" + idToDelete + "'";

            SqlCommand cmd_Command = new SqlCommand(sqlcmd, cn_connection);
            cmd_Command.ExecuteNonQuery();
            MessageBox.Show("Rekord zaktualizowant!", "Powiadomienie", MessageBoxButton.OK);
            Close();
        }
    }
}
