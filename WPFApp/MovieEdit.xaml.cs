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
using System.Data.SqlClient;


namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MovieEdit.xaml
    /// </summary>
    public partial class MovieEdit : Window
    {
        private string BrugerID;
        SqlConnection conn = new SqlConnection("Data Source=10.0.6.118;Persist Security INFO=True;User ID=Admin;Password=Passw0rd;Initial Catalog=MovieContex");
        public MovieEdit(string BrugerIdentity)
        {
            InitializeComponent();
            BrugerID = BrugerIdentity;
            Rediger_ID.Text = BrugerID;

            // Denne del henter information fra databasen ud fra hvilket id filmen har. //
            conn.Open();
            if (Rediger_ID.Text != "")
            {
                SqlCommand cmd1 = new SqlCommand("Select Title, ReleaseDate, Genre, Rating, Price from Movie where ID =@ID", conn);
                cmd1.Parameters.AddWithValue("@ID", BrugerID);
                SqlDataReader da = cmd1.ExecuteReader();
                while (da.Read())
                {
                    Rediger_Title.Text = da.GetValue(0).ToString();
                    Rediger_ReleaseDate.Text = da.GetValue(1).ToString();
                    Rediger_Genre.Text = da.GetValue(2).ToString();
                    Rediger_Price.Text = da.GetValue(4).ToString();
                    Rediger_Rating.Text = da.GetValue(3).ToString();
                }
                conn.Close();
            }
        }

        // Denne her metode clear alle, felterne i Rediger vinduet. //
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rediger_Title.Clear();
            Rediger_ReleaseDate.Clear();
            Rediger_Genre.Clear();
            Rediger_Price.Clear();
            Rediger_Rating.Clear();
        }

        // Denne her metode gør at, hvis der er blevet ændret i nogle af felterne bliver dataen opdateret til den redigeret. //
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Update Movie set Title = @RT, ReleaseDate = @RRD, Genre = @RG, Price = @RP, Rating = @RR where ID= " + BrugerID, conn);
            // Rediger Title. //
            cmd.Parameters.AddWithValue("@RT", Rediger_Title.Text);
            // Rediger Realease Date. //
            cmd.Parameters.AddWithValue("@RRD", Rediger_ReleaseDate.Text);
            // Rediger Genre. //
            cmd.Parameters.AddWithValue("@RG", Rediger_Genre.Text);
            // Rediger Price. //
            cmd.Parameters.AddWithValue("@RP", Rediger_Price.Text);
            // Rediger Rating. //
            cmd.Parameters.AddWithValue("@RR", Rediger_Rating.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Data'en er indsat i databasen!");
            conn.Close();
        }

        // Tilbage knap. //
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataGridBind.MainWindow Win1 = new DataGridBind.MainWindow();
            Win1.Show();
            this.Close();
        }
    }
}
