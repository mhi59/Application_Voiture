using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace WpfApp8.Model
{
    public class Services
    {
        
        #region Chargement des tables
        public Services() // Chargement de la liste
        {
                adaptChargement.MissingSchemaAction = MissingSchemaAction.AddWithKey;//Récupération clé primaire
                adaptChargementTableId.MissingSchemaAction = MissingSchemaAction.AddWithKey;//Récupération clé primaire            
                adaptChargement.Fill(monDataset, "voiture");//Remplissage de la table voiture  servant à l'affichage/lecture seule
                adaptChargementTableId.Fill(monDataset, "tableId");//remplissage table ID servant à l'ajout et modification des véhicules
                adaptOptions.Fill(monDataset, "tableOptions");//Remplissage table Options 
                adaptChargementIdOptions.Fill(monDataset, "tableOptionsId");
               // int idMax = (int)returnParameter.Value;

        }
        #endregion
        #region DataAdapter, requêtes ainsi que mon DataSet me servant pour toutes mes actions sur la BDD
        static string chaineDeConnexion = ConfigurationManager.ConnectionStrings["connexionMySql"].ToString();//Chaine de connexion encrypté dans app.config
        static string reqChargement = "chargement"; // Procédure stockée pour le chargement de la table "voiture"
        static string reqChargementTableId = "chargement_table_id";// Requête Procédure stockée pour le chargement de la table "tableId" qui me sert pour l'ajout ou la modification de voiture
        static string reqAjouter = "Ajouter";// Requête Procédure stockée pour l'ajout
        static string reqSuppression = "Suppression";//Requête Procédure stockée pour la supression
        static string reqModification = "Modification";//Requête Procédure stockée pour la modification
        static string reqChargementOptions = "chargementOptions";//Requête procédure stockée pour les options
        static string reqChargementIdOptions = "chargementIdOptions";//Requête procédure stockée pour chargement table option qu'avec les iD
        static string reqIdMax = "idMax";//Requête procédure stockée récupération du plus grand id_voiture de ma table
        static string reqAjoutOptions = "ajoutOptions";//Requête procédure stockée pour ajout d'options
        static string reqSupressionOptions = "SupressionOptions";//Requête procédure stockée pour supression option 
         




        static MySqlConnection con = new MySqlConnection(chaineDeConnexion);
        MySqlDataAdapter adaptChargement = new MySqlDataAdapter(reqChargement, chaineDeConnexion);//DataAdapter pour le chargement de la table voiture
        MySqlDataAdapter adaptChargementTableId = new MySqlDataAdapter(reqChargementTableId, chaineDeConnexion);//Dataadapter pour le chargement de la table Id
        MySqlDataAdapter adaptSuppression = new MySqlDataAdapter();//DataAdapter pour la suppression
        MySqlDataAdapter adapteurAjout = new MySqlDataAdapter();//DataAdapter pour l'ajout
        MySqlDataAdapter adapterModificationOptions = new MySqlDataAdapter(); //DataAdapter pour l'ajout d'options
        MySqlDataAdapter adapteurModification = new MySqlDataAdapter();//DataAdapter pour la modification
        MySqlDataAdapter adaptRecupIdMax = new MySqlDataAdapter(reqIdMax, chaineDeConnexion);//DataAdapter pour Récupération plus grand iD de ma table voiture
        MySqlDataAdapter adaptChargementIdOptions = new MySqlDataAdapter(reqChargementIdOptions, chaineDeConnexion);//DataAdapter pour récupération table option avec que les iD
        MySqlDataAdapter adaptSupressionOption = new MySqlDataAdapter();//DataAdapter pour supression d'options        
        MySqlDataAdapter adaptOptions = new MySqlDataAdapter(reqChargementOptions, chaineDeConnexion);//DataAdapter pour le chargement des options des voitures
        DataTable maTableOptions = monDataset.Tables["tableOptionsId"]; // Je récupère ma table "tableOptionsId" qui est dans "monDataset" pour pouvoir la modifier        
        DataRow ligneOption;//Initialisation d'une DataRow "nouvelleLigneOption"
        MySqlCommand commandAjoutOptions = new MySqlCommand(reqAjoutOptions, con);//Paramétrage de ma commande AjoutOptions prenant en paramètre ma requête ajoutOptions qui fait appel à une procédure stockée et à ma requête de connexion

        private static DataSet monDataset = new DataSet();//Dataset qui contiendra mes deux tables "voiture" et "tableId"
        public DataSet MonDataset
        {
            get
            {
                return monDataset;
            }
        }
        #endregion
        #region Suppression d'un véhicule
        public Services(int primaryKey)
        {
            monDataset.Tables["voiture"].Rows.Find(primaryKey).Delete();//Supression de la ligne sélectionnée dans le DataSet avec comme indice sa primaryKey
            MySqlConnection con = new MySqlConnection(chaineDeConnexion);//Connexion1 qui va nous servir à paramétrer la commande
            MySqlCommand commandSuppression = new MySqlCommand(reqSuppression, con);//paramétrage de la commande de supression que le DataAdapter utilisera
            commandSuppression.CommandType = CommandType.StoredProcedure;
            commandSuppression.Parameters.AddWithValue("primaryKey", primaryKey);//Je passe l'Id de la clé primation id_voiture à supprimer que j'envoie à ma procédure stockée          
            adaptSuppression.DeleteCommand = commandSuppression;//Liaison de notre commande de supression avec le DeleteCommand de mon DataAdapter
            adaptSuppression.Update(monDataset, "voiture");
        }
        #endregion
        #region Ajout d'un véhicule
        public Services(int prixAchat, int argus, string immat, int transmi, int couleur, int puissance, int carb, int annee, int places, int modele, int categorie, ObservableCollection<OptionsProp> options)
        {
            DataTable maTable = monDataset.Tables["tableId"];// Je récupère ma table "tableId" qui est dans "monDataset" pour pouvoir la modifier
            DataRow nouvelleLigne = maTable.NewRow();
            nouvelleLigne.ItemArray = new Object[] { null, prixAchat, argus, null, immat, puissance, places, couleur, carb, transmi, categorie, annee, modele };
            maTable.Rows.Add(nouvelleLigne); // Ajout du véhicule dans ma DataTable "tableId"            
            MySqlCommand commandAjout = new MySqlCommand(reqAjouter, con);//Paramétrage de ma commande Ajout prenant en paramètre ma requête Ajouter qui fait appel à une procédure stockée et à ma requête de connexion
            commandAjout.CommandType = CommandType.StoredProcedure;//Je précise que la commande Ajout est de "type" procédure stockée
            //Ajout des paramètres à la commande Ajout qui vont être envoyés à la procédure stockée
            commandAjout.Parameters.AddWithValue("prix", prixAchat);
            commandAjout.Parameters.AddWithValue("argus", argus);
            commandAjout.Parameters.AddWithValue("immatriculation", immat);
            commandAjout.Parameters.AddWithValue("transmission", transmi);
            commandAjout.Parameters.AddWithValue("couleur", couleur);
            commandAjout.Parameters.AddWithValue("puissance", puissance);
            commandAjout.Parameters.AddWithValue("carburant", carb);
            commandAjout.Parameters.AddWithValue("annee", annee);
            commandAjout.Parameters.AddWithValue("places", places);
            commandAjout.Parameters.AddWithValue("modele", modele);
            commandAjout.Parameters.AddWithValue("categorie", categorie);

            adapteurAjout.InsertCommand = commandAjout;//On ajoute la commande "commandeAjout" qui sert d'"insert" pour le DataAdapter
            adapteurAjout.Update(monDataset, "tableId");//On lance l'update pour ajout du nouveau véhicule
            adaptRecupIdMax.Fill(monDataset, "idMax");//Récupération du dernier id crée pour ajouter les options au bon véhicule

            #region Partie AjoutOptions
            commandAjoutOptions.CommandType = CommandType.StoredProcedure;//Je précise que la commande est de type procédure stockée
            adapterModificationOptions.InsertCommand = commandAjoutOptions;//On ajoute la commande "commandAjoutOptions" qui ser d'"insert" pour la DataAdapter
            

            for (int i = 0; i < options.Count; i++)//Je crée une boucle pour parcourir l'ObservableCollection "option"
            {
                ligneOption = maTableOptions.NewRow();//Affectation de la DataRow"nouvelleLigneOption"
                ligneOption.ItemArray = new Object[] { (int)monDataset.Tables["idMax"].Rows[0]["id_max"], (int)options[i].IdOptions };//Paramétrage de la nouvelle ligne à ajouter dans maTableOption prenant en paramètre l'id de ma voiture qui vient d'être crée et l'option qu'on lui ajoute
                maTableOptions.Rows.Add(ligneOption);//Ajout de la ligne nouvelleLigneOption à maTableOptions
                //Ajout des paramètres à la commande AjoutOptions qui vont être envoyés à la procédure stockée
                commandAjoutOptions.Parameters.AddWithValue("id_voiture", (int)monDataset.Tables["idMax"].Rows[0]["id_max"]);
                commandAjoutOptions.Parameters.AddWithValue("id_options", (int)options[i].IdOptions);
                adapterModificationOptions.Update(monDataset, "tableOptionsId");//On lance l'update pour ajouter l'option
                commandAjoutOptions.Parameters.Clear();//On efface les paramètres associés à commandAjoutOptions pour refaire un tour de boucle
            }
            #endregion

            //Partie mis à jour
            monDataset.Tables["idMax"].Clear();//On vide la table idMax pour ne pas avoir de doublon par la suite
            monDataset.Tables["voiture"].Clear();
            monDataset.Tables["tableOptions"].Clear();
            adaptChargement.Fill(monDataset, "voiture");//On met à jour la table voiture pour que la vue soit répercutée
            adaptOptions.Fill(monDataset, "tableOptions");//On met à jour la table option pour que la vue soit répercutée
        }
        #endregion
        #region Modification d'un véhicule
        public Services(int argus, string immatriculation, int puissance, int couleur, int carburant, int transmission, int categorie, int annee, int id_voiture, ObservableCollection<OptionsProp> options)
        {
            MySqlCommand commandModification = new MySqlCommand(reqModification, con);//Paramétrage de ma commande Modification prenant en paramètre ma requête Modification qui fait appel à une procédure stockée et à requête de connextion
            commandModification.CommandType = CommandType.StoredProcedure;//Je précise que la commande Modification est de "type" procédure stockée
            DataRow updateRow = monDataset.Tables["tableId"].Rows.Find(id_voiture);//selection de la bonne Row à modifier dans la table "tableId" par sa clé primaire

            //Mis à jour des colonnes nécessaires dans la Datarow précédemment sélectionnée
            updateRow["cote_argus"] = argus;
            updateRow["immatriculation"] = immatriculation;
            updateRow["id_puissance"] = puissance;
            updateRow["id_couleur"] = couleur;
            updateRow["id_carburant"] = carburant;
            updateRow["id_transmission"] = transmission;
            updateRow["id_categorie"] = categorie;
            updateRow["id_annee"] = annee;

            //Ajout des paramètres à la commande Modification qui vont être envoyés à la procédure stockée
            commandModification.Parameters.AddWithValue("argus", argus);
            commandModification.Parameters.AddWithValue("immatriculation", immatriculation);
            commandModification.Parameters.AddWithValue("puissance", puissance);
            commandModification.Parameters.AddWithValue("couleur", couleur);
            commandModification.Parameters.AddWithValue("carburant", carburant);
            commandModification.Parameters.AddWithValue("transmission", transmission);
            commandModification.Parameters.AddWithValue("categorie", categorie);
            commandModification.Parameters.AddWithValue("annee", annee);
            commandModification.Parameters.AddWithValue("id_voiture", id_voiture);


            #region Partie modification des options
            MySqlCommand commandAjoutOptions = new MySqlCommand(reqAjoutOptions, con);//Paramétrage de ma commande AjoutOptions prenant en paramètre ma requête ajoutOptions qui fait appel à une procédure stockée et à ma requête de connexion
            commandAjoutOptions.CommandType = CommandType.StoredProcedure;//Je précise que la commande est de type procédure stockée


            adaptSupressionOption.ContinueUpdateOnError = true;//On continue l'update en cas d'erreur pour contourner une restriction du à l'update de supression
            MySqlCommand commandSupressionOptions = new MySqlCommand(reqSupressionOptions, con);//Paramétrage de ma commande SupressionOptions prenant en paramètre ma requête SupressionOptions qui fait appel à une procédure stockée et à ma requête de connexion
            commandSupressionOptions.CommandType = CommandType.StoredProcedure;
            commandSupressionOptions.Parameters.AddWithValue("id_voiture", id_voiture);//On ajoute comme paramètre à la commande supressionOption l'id_voiture qui permettra de supprimer de notre BB les options correspondants à cet id
            adaptSupressionOption.DeleteCommand = commandSupressionOptions;//assignation de la deletecommand à l'adapter ModificationOptions



            //On supprimer de la table "tableOptionsId" toutes les options correspondant à l'id_voiture en cours de traitement
            for (int i = 0; i < monDataset.Tables["tableOptionsId"].Rows.Count; i++)
            {
                if((int)monDataset.Tables["tableOptionsId"].Rows[i]["id_voiture"] == id_voiture)//si id_voiture == id_voiture de la ligne parcourue, on supprime cette ligne
                {
                    monDataset.Tables["tableOptionsId"].Rows[i].Delete();
                }
           }

            adaptSupressionOption.Update(monDataset, "tableOptionsId"); // On supprime sur la BDD les options associés à l'id_voiture en cours de traitement
            monDataset.Tables["tableOptionsId"].Clear();//On vide la table tableOptionsId
            adaptOptions.Fill(monDataset, "tableOptionsId");//On rempli la tableOptionsId

            adapterModificationOptions.InsertCommand = commandAjoutOptions;////assignation de la insertcommand à l'adapter ModificationOptions

            for (int i = 0; i < options.Count; i++)//Je crée une boucle pour parcourir l'ObservableCollection "option"
            {                
                ligneOption = maTableOptions.NewRow();//Affectation de la DataRow"nouvelleLigneOption"
                ligneOption.ItemArray = new Object[] { id_voiture, (int)options[i].IdOptions };//Paramétrage de la nouvelle ligne à ajouter dans maTableOption prenant en paramètre l'id de ma voiture qui vient d'être crée et l'option qu'on lui ajoute
                maTableOptions.Rows.Add(ligneOption);//Ajout de la ligne nouvelleLigneOption à maTableOptions
                //Ajout des paramètres à la commande AjoutOptions qui vont être envoyés à la procédure stockée

                commandAjoutOptions.Parameters.AddWithValue("id_voiture", id_voiture);
                commandAjoutOptions.Parameters.AddWithValue("id_options", (int)options[i].IdOptions);
                adapterModificationOptions.Update(monDataset, "tableOptionsId");//On lance l'update pour ajouter les options sur la BDD grâce aux command paramétrées précedemment
                commandAjoutOptions.Parameters.Clear();//On efface les paramètres associés à commandAjoutOptions pour refaire un tour de boucle
            }
            #endregion

            adapteurModification.UpdateCommand = commandModification;//On ajoute la commande "commandModification" qui sert d'"Updtate" pour le DataAdapter
            adapteurModification.Update(monDataset, "tableId");//On lance l'update pour modification du véhicule

            monDataset.Tables["tableOptions"].Clear();//On vide la table option avant de la remplir en dessous

            adaptChargement.Fill(monDataset, "voiture");//On met à jour la table voiture pour que la vue soit répercutée.
            adaptOptions.Fill(monDataset, "tableOptions");//On met à jour la table option pour que la vue soit répercutée
        }
        #endregion
    }
}
