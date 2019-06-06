using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemiZaUpravljanjeBazamaPodatka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connetionString = null;
            SqlConnection cnn;

            //koriscenjem SqlConnectionStringBuilder validiramo connection string (sprecavano unos SQL Injection pri konekciji). Takodje mozemo lako menjati vrednost connection stringa
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(GetConnectionStringWindows());

            Console.WriteLine("Windows connection string:");
            Console.WriteLine(builder.ConnectionString);

            //SQL Server login nije siguran jer podaci idu preko mreze, cime login moze da bude narusen.
            //Console.WriteLine("SQL Server connection string:");
            //builder.ConnectionString = GetConnectionStringSQLServerLogin();

            builder["initial catalog"] = "SZUBP;Server=192.168.1.2";
            //Ovde klijent trazi bazu podataka pod imenom "AdventureWorks2014;Server=192.168.1.2" za razliku od prethodnog resenja gde klijent gadja drugi server
            connetionString = builder.ConnectionString;

            cnn = new SqlConnection(connetionString);


            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open");

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
         
        }

        private static string GetConnectionStringWindows()
        {
            return "Server=(local);Integrated Security=SSPI;Initial Catalog=SZUBP;";
        }
        private static string GetConnectionStringSQLServerLogin()
        {
            return "server=(local);user id=aca;password= acaaca;initial catalog=SZUBP";
        }
    

        private void button3_Click(object sender, EventArgs e)
        {
            string connetionString = null;
            SqlConnection cnn;
            SqlCommand command;
            string parametar;


            connetionString = "Server=(local);Integrated Security=SSPI;Initial Catalog=SZUBP";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connetionString);

            connetionString = builder.ConnectionString;

            cnn = new SqlConnection(connetionString);


            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open");

                parametar = "'Acimir'";
                command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE FirstName = " + parametar + ";", cnn);
                var reader = command.ExecuteReader();
                reader.Read();
                MessageBox.Show((string)reader["FirstName"]);
                reader.Close();
                //po izvrsenju programa, sql server nece da izvrsi dodatnu sql naredbu
                parametar = "'Acimir';INSERT INTO  [User] (FirstName, Location) VALUES ('Zika', 'ObalaSlonovace');--";
                command = new SqlCommand("SELECT * FROM [User] WHERE FirstName = @parameter;", cnn);
                command.Parameters.AddWithValue("parameter", parametar);
                reader = command.ExecuteReader();

                reader.Close();

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connetionString = null;
            SqlConnection cnn;

            string database = "SZUBP;Server=192.168.1.2";
            connetionString = "Server=(local);Integrated Security=SSPI;Initial Catalog=" + database + "; ";
            cnn = new SqlConnection(connetionString);
            //Ovde klijent trazi drugi server sa imenom (IP adresom) "192.168.1.2" za razliku od resenja koje koristi SqlConnectionStringBuilder


            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open");

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connetionString = null;
            SqlConnection cnn;
            SqlCommand command;
            SqlDataReader reader;
            string parametar;


            connetionString = "Server=(local);Integrated Security=SSPI;Initial Catalog=SZUBP";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connetionString);

            connetionString = builder.ConnectionString;

            cnn = new SqlConnection(connetionString);

            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open");

                parametar = "'Acimir'";
                command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE FirstName = " + parametar + ";", cnn);
                reader = command.ExecuteReader();
                MessageBox.Show(reader.FieldCount.ToString());
                reader.Close();
                //po izvrsenju programa, sql server ce da izvrsi obe sql naredbe
                parametar = "'Acimir';DROP TABLE [Table_1];--";
                command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE FirstName = " + parametar + ";", cnn);
                reader = command.ExecuteReader();
                MessageBox.Show(reader.FieldCount.ToString());
                reader.Close();

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
           
        }
    }
}
