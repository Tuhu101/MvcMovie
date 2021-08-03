using System;
using System.Collections.Generic;
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
using System.Data.Sql;
using System.Data.SqlClient;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MovieCreate.xaml
    /// </summary>
    public partial class MovieCreate : Window
    {
        // Det her er min Connection String. //
        SqlConnection conn = new SqlConnection("Data Source=10.0.6.118;Persist Security INFO=True;User ID=Admin;Password=Passw0rd;Initial Catalog=MovieContex");
        public MovieCreate()
        {
            InitializeComponent();
        }

        // Den her del er til at oprette/inputte i Databasen. //
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into Movie(Title, ReleaseDate, Genre, Rating, Price) values(@CRT,@CRRD,@CRG,@CRR,@CRP)", conn);
            // Create Title. //
            cmd.Parameters.AddWithValue("@CRT", Create_Title.Text);
            // Create Realease Date. //
            cmd.Parameters.AddWithValue("@CRRD", Create_ReleaseDate.Text);
            // Create Genre. //
            cmd.Parameters.AddWithValue("@CRG", Create_Genre.Text);
            // Create Price. //
            cmd.Parameters.AddWithValue("@CRP", Create_Price.Text);
            // Create Rating. //
            cmd.Parameters.AddWithValue("@CRR", Create_Rating.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Data'en er indsat i databasen!");
            conn.Close();
        }

        // Denne her metode clear alle, felterne i Create vinduet. //
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Create_Title.Clear();
            Create_ReleaseDate.Clear();
            Create_Genre.Clear();
            Create_Price.Clear();
            Create_Rating.Clear();
        }

        // Denne her metode sender brugeren tilbage til Mainvinduet/hovedvinduet. //
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataGridBind.MainWindow Win1 = new DataGridBind.MainWindow();
            Win1.Show();
            this.Close();
        }
        
    }
}
