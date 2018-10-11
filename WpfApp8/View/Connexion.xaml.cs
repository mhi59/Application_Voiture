using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
using System.Configuration;

namespace WpfApp8.View
{
    /// <summary>
    /// Logique d'interaction pour Connexion1.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        public Connexion()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void Connexion1(object sender, RoutedEventArgs e)
        {
            string chaineDeConnexion = ConfigurationManager.ConnectionStrings["connexionMySql"].ToString();//Chaine de connexion encryptée dans app.config
            string requete = "identify";//Requête procédure stockée qui récupère les identifiants sur la base de donées qui sont stockées en SHA256 et renvoyés concaténés(login+password) en majuscule
            string username = sha256(login.Text);//Appel de la méthode sha256 permettant de transformer un string en string crypté en SHA256
            string pass = sha256(passwordBox.Password);//Appel de la méthode sha256 permettant de transformer un string en string crypté en SHA256
            bool flag = false;//flag permettant de valider ou non le message d'erreur si aucunes correspondance n'est trouvée          


        List<string> Liste = new List<string> { };
            try
            {
                //Création de la connexion pour récupérer les identifiants sur la base de donées qui sont stockées en SHA256 et renvoyés concaténés(login+password) en majuscule
                MySqlConnection connexion = new MySqlConnection(chaineDeConnexion);
                MySqlCommand command = new MySqlCommand(requete, connexion);
                command.CommandType = CommandType.StoredProcedure;
                connexion.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Liste.Add(reader["identifiant"].ToString());//On remplie la liste avec les user sur la base
                }
                connexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            foreach(string identifiant in Liste)
            {
                if(identifiant.Equals((username+pass).ToUpper())) // Si on a une correspondance entre un couple login+pass de la base et ceux tapés, on ouvre la nouvelle fenêtre
                {
                    MainWindow fenetrePrincipale = new MainWindow();
                    fenetrePrincipale.Show();//On ouvre la fenêtre principal
                    this.Close();//On ferme cette fenêtre
                    flag = true;//On passe flag à true pour ne pas afficher le message d'erreur
                }
            }
            if(flag == false)//Si false c'est qu'aucunes correspondance n'a été trouvé on affiche le message d'erreur
            {
                MessageBox.Show("Le nom d'utilisateur ou le mot de passe que vous avez entré ne sont pas correct.");
            }
        }
        private static string sha256 (string value)//Méthode permettant de transformer un string en son équivalant en SHA256
        {
            StringBuilder Sb = new StringBuilder();
            using(SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));
                foreach(Byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
                return Sb.ToString();
            }
        }
    }
}
