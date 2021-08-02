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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        string BrugerID;
        SqlConnection conn = new SqlConnection("Data Source=10.0.6.118;Persist Security INFO=True;User ID=Admin;Password=Passw0rd;Initial Catalog=MovieContex");
        public Details(string BrugerIdentity)
        {
            InitializeComponent();
            BrugerID = BrugerIdentity;
            Details_ID.Text = BrugerID;

            MessageBox.Show(BrugerID);
            
        }

        // Tilbage. //
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataGridBind.MainWindow Win1 = new DataGridBind.MainWindow();
            Win1.Show();
            this.Close();
        }

        // Edit Knap. //
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WPFApp.MovieEdit win1 = new WPFApp.MovieEdit(BrugerID);
            win1.Show();
            this.Close();
        }

        // Textbox til iD
        private void Details_ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            SqlConnection conn = new SqlConnection("Data Source=10.0.6.118;Persist Security INFO=True;User ID=Admin;Password=Passw0rd;Initial Catalog=MovieContex");
            conn.Open();
            
            if (Details_ID.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Select Title, ReleaseDate, Genre, Rating, Price from Movie where ID =@ID", conn);
                cmd.Parameters.AddWithValue("@ID", BrugerID);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    Details_Title.Text = da.GetValue(0).ToString();
                    Details_ReleaseDate.Text = da.GetValue(1).ToString();
                    Details_Genre.Text = da.GetValue(2).ToString();
                    Details_Price.Text = da.GetValue(3).ToString();
                }
                conn.Close();
            }
        }
    }
}
