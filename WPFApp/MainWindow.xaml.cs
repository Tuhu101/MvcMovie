using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

//Using namespaces  
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace DataGridBind
{
    /// <summary>  
    /// Interaction logic for MainWindow.xaml  
    /// </summary>  
    public partial class MainWindow : Window
    {
        string BrugerID;
        // Det her er min Connection String. //
        SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        public MainWindow()
        {
            InitializeComponent();
            // Den her del genlæser mit grid med dataen fra databasen. //
            SqlConnection conn = new SqlConnection("Data Source=10.0.6.118;Persist Security INFO=True;User ID=Admin;Password=Passw0rd;Initial Catalog=MovieContex");
            // WIN-B0N5VFTLT1R\SQLEXPRESS 
            SqlCommand cmd = new SqlCommand("Select Id, Title, ReleaseDate, Genre, Price, Rating from Movie", conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            DataGridMoveList.DataContext = ds;
        }

       

        // Her henter jeg data'en ud fra databasen og indsætter det i gridet jeg har. //
        private void btnloaddata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=10.0.6.118;Persist Security INFO=True;User ID=Admin;Password=Passw0rd;Initial Catalog=MovieContex");
                // WIN-B0N5VFTLT1R\SQLEXPRESS 
                SqlCommand cmd = new SqlCommand("Select Id, Title, ReleaseDate, Genre, Price, Rating from Movie", conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                DataGridMoveList.DataContext = ds;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally {
                conn.Close();
            }
        }


        // Denne her metode er til Edit knappen. //
        private void Edit(object sender, RoutedEventArgs e)
        {
            // Her finder jeg ID'et på den gældende film. // 
            object item = DataGridMoveList.SelectedItem;
            string BrugerID = (DataGridMoveList.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            // Her åbner jeg et nyt vindue, hvor man kan redigere dataen om filmen. //
            WPFApp.MovieEdit win2 = new WPFApp.MovieEdit(BrugerID);
            win2.Show();
            this.Close();
        }

        // Denne her metode er til Delete knappen. //
        private void Delete(object sender, RoutedEventArgs e)
        {
            object item = DataGridMoveList.SelectedItem;
            string BrugerID = (DataGridMoveList.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            
            SqlConnection conn = new SqlConnection("Data Source=10.0.6.118;Persist Security INFO=True;User ID=Admin;Password=Passw0rd;Initial Catalog=MovieContex");
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Movie where Id= " + BrugerID, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bruger er blevet slettet");
            conn.Close();   
        }

        // Denne her metode er til Details knappen. //
        private void Details(object sender, RoutedEventArgs e)
        {
            object item = DataGridMoveList.SelectedItem;
            string BrugerID = (DataGridMoveList.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            
            WPFApp.Details win4 = new WPFApp.Details(BrugerID);
            win4.Show();
            this.Close();
            
        }

        // This method is for the Create window. // 
        private void Create(object sender, RoutedEventArgs e)
        {
            WPFApp.MovieCreate win3 = new WPFApp.MovieCreate();
            win3.Show();
            this.Close();
        }
    }
}