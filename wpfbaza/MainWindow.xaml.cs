using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Policy;
using System.Windows.Controls.Primitives;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;

namespace wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

        }

        bool Person(string warunek)
        {


            string conn = "SERVER=localhost; DATABASE=baza131; UID=root; PASSWORD=;";
            MySqlConnection co = new MySqlConnection(conn);
            co.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT Nazwa_uzytkownika FROM uzytkownik WHERE Nazwa_uzytkownika='" + mnazwa.Text + "' AND " + warunek + "='1'", co);
            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            if (myReader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        void Update()
        {
            string conn = "SERVER=localhost; DATABASE=baza131; UID=root; PASSWORD=;";
            MySqlConnection connection = new MySqlConnection(conn);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM uzytkownik", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            dtGrid.DataContext = dt;
        }

        void Updatep()
        {

            string warunek = "wyswietlanie";

            if (Person(warunek))
            {

                MessageBox.Show("udalo sie");
                dtGrid.Visibility = Visibility.Visible;
                Update();

            }
            else
            {
                MessageBox.Show("nie udalo sie" + this.mnazwa.Text);

            }
        }
        void Mess()
        {
            MessageBox.Show("Pyrzkro nam , wygląda na to że nie masz uprawnień");
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string imie = nazwa.Text;
            string haslo = haslo1.Text;
            string emaill = email.Text;
            string potwierdzhaslo = haslo2.Text;


            if (haslo == potwierdzhaslo && haslo != "" && haslo != null)
            {
                if (email.Text.Contains('@'))
                {

                    string warunekk = "dodawanie";

                    if (Person(warunekk))
                    {

                        string conn = "SERVER=localhost; DATABASE=baza131; UID=root; PASSWORD=;";
                        MySqlConnection co = new MySqlConnection(conn);
                        String Query = "INSERT INTO uzytkownik(nazwa_uzytkownika, haslo,email) values('" + imie + "', '" + haslo + "', '" + emaill + "');";
                        MySqlCommand zapytanie = new MySqlCommand(Query, co);
                        MySqlDataReader MyReader;
                        co.Open();
                        MyReader = zapytanie.ExecuteReader();

                        co.Close();
                        string conn1 = "SERVER=localhost; DATABASE=baza131; UID=root; PASSWORD=;";
                        MySqlConnection co1 = new MySqlConnection(conn1);
                        String Query2 = "ALTER TABLE uzytkownik AUTO_INCREMENT = 1";
                        MySqlCommand zapytanie1 = new MySqlCommand(Query2, co1);
                        MySqlDataReader MyReader1;
                        co1.Open();
                        MyReader1 = zapytanie1.ExecuteReader();
                        co1.Close();
                        Update();

                    }
                    else
                    {
                        Mess();

                    }
                }
                else
                {
                    MessageBox.Show("Proszę wpisać e-mail poprawnie!");

                }
            }
            else
            {
                MessageBox.Show("Hasła nie są podobne lub hasło zostało nie wpisane!");
            }
        }



        private void button2_Click(object sender, RoutedEventArgs e)
        {

            string warunekk = "usuwanie";

            if (Person(warunekk))
            {
                string conn = "SERVER=localhost; DATABASE=baza131; UID=root; PASSWORD=;";
                MySqlConnection co = new MySqlConnection(conn);
                String Query = "DELETE FROM uzytkownik WHERE id=" + (dtGrid.SelectedIndex + 1) + ";";
                MySqlCommand zapytanie = new MySqlCommand(Query, co);
                MySqlDataReader MyReader;
                co.Open();
                MyReader = zapytanie.ExecuteReader();
                co.Close();
                   string conn1 = "SERVER=localhost; DATABASE=baza131; UID=root; PASSWORD=;";
                MySqlConnection co1 = new MySqlConnection(conn1);
                String Query2 = "ALTER TABLE uzytkownik AUTO_INCREMENT = 1";
                MySqlCommand zapytanie1 = new MySqlCommand(Query2, co1);
                MySqlDataReader MyReader1;
                co1.Open();
                MyReader1= zapytanie1.ExecuteReader();
                co1.Close();



                Update();
            }
            else
            {
                Mess();
            }

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string warunekk = "edycja";

            if (Person(warunekk))
            {
                string conn = "SERVER=localhost; DATABASE=baza131; UID=root; PASSWORD=;";
                MySqlConnection co = new MySqlConnection(conn);

                String Query = "UPDATE uzytkownik SET Nazwa_uzytkownika = '" + nazwa.Text + "' ,email = '" + email.Text + "' , haslo = '" + haslo1.Text + "'  ";
                if (wys.IsChecked == true)
                    Query += ",wyswietlanie=true ";
                if (dod.IsChecked == true)
                    Query += ",dodawanie=true ";
                if (usu.IsChecked == true)
                    Query += ",usuwanie=true ";
                if (edy.IsChecked == true)
                    Query += ",edycja=true ";

                Query += "WHERE id='" + (dtGrid.SelectedIndex + 1) + "';";
                MessageBox.Show(Query);

                MySqlCommand zapytanie = new MySqlCommand(Query, co);
                MySqlDataReader MyReader;
                co.Open();
                MyReader = zapytanie.ExecuteReader();
                co.Close();
                Update();
            }
            else
            {
                Mess();
            }


        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (che.IsChecked == true)
            {
                dodaj.Visibility = Visibility.Visible;
                usun.Visibility = Visibility.Visible;


            }
            else
            {
                dodaj.Visibility = Visibility.Hidden;
                usun.Visibility = Visibility.Hidden;

            }
        }

        private void dtGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {

            string conn = "SERVER=localhost; DATABASE=baza131; UID=root; PASSWORD=;";
            MySqlConnection co = new MySqlConnection(conn);
            co.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT Nazwa_uzytkownika FROM uzytkownik WHERE haslo = '" + this.mhaslo1.Text + "' AND Nazwa_uzytkownika = '" + this.mnazwa.Text + "'; ", co);
            MySqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            if (myReader.Read())
            {
                myReader.Close();
                MessageBox.Show("poprawne dane logownie");

             
                main.Visibility = Visibility.Hidden;
                sec.Visibility = Visibility.Visible;
                Updatep();

                co.Close();

            }
            else
            {
                MessageBox.Show("Niepoprawne dane logownie");
                co.Close();

            }


        }


    }
}



