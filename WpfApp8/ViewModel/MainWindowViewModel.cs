using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp8.Helpers;
namespace WpfApp8.ViewModel
{
    public class MainWindowViewModel : Helpers, INotifyPropertyChanged
    {
        //Création des ICommand bindés avec les différents boutons du projet
        public ICommand BoutonModifier { get; set; }
        public ICommand BoutonAjouter { get; set; }
        public ICommand BoutonSauvegarder { get; set; }
        public ICommand BoutonSupprimer { get; set; }
        public ICommand BoutonAjoutOptions { get; set; }
        public ICommand BoutonSupprimerOptions { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }//Permet de fermer la fenêtre en récupérant depuis la vue la Window ouverte(ici modifier)



        public MainWindowViewModel()
        {
            //Initialisation des Icommand   
            BoutonModifier = new RelayCommand(o => Modifier(o), o => VerifModifier(o));
            BoutonAjouter = new RelayCommand(o => Ajouter(o), o => true);
            BoutonSauvegarder = new RelayCommand(o => Sauvegarder(o), o => true);
            BoutonSupprimer = new RelayCommand(o => Supprimer(o), o => VerifSupprimer(o));
            BoutonAjoutOptions = new RelayCommand(o => AjoutOptions(o), o => VerifAjoutOptions(o));//fleche verte
            BoutonSupprimerOptions = new RelayCommand(o => SupprimerOptions(o), o => VerifSupprimerOptions(o));//fleche rouge
            CloseWindowCommand = new RelayCommand(Sauvegarder);//On envoie la gestion du bouton qui va gérer la fermeture de fenêtre à la méthode Sauvegarder

        }

        private static Model.Services Model = new Model.Services();

        private static DataView monDataview = Model.MonDataset.Tables["voiture"].DefaultView; // Renvoi le contenu de la table "voiture" vers le datagrid

        public DataView MonDataview
        {
            get
            {
                return monDataview;
            }
            set
            {
                monDataview = value;
            }
        }

        private DataView dataViewOptions = Model.MonDataset.Tables["tableOptions"].DefaultView;

        public DataView DataViewOptions//Renvoi les options des voitures au datagrid des options
        {            
            get
            {
                if (selectedIndex >= 0)
                {
                    dataViewOptions.RowFilter = @"id_voiture =" + Idselectionne;//Si une voiture est séléctionnée on applique un filtre sur la table option qui n'affichera que les options dont l'id de la voiture de la table "voiture" correspond à l'id_voiture de la table"tableOptions"
                }
                return Model.MonDataset.Tables["tableOptions"].DefaultView;
            }            
        }
        
        private string champRecherche;//Bindé avec le textBox pour la recherche

        public string ChampRecherche
        {
            get
            {
                return champRecherche;
            }
            set
            {
                //Paramétrage du filtre appliquer sur le Dataview avec la propriété rowFilter en fonction du radioBox sélectionné pour la recherche
                champRecherche = value;
                if (!(ChampRecherche == null) && !(ChampRecherche == ""))//On lance une recherche qui si le champ recherche est != de null et n'est pas vide
                {
                    switch (RadioButton)
                    {
                        case "ID":
                            monDataview.RowFilter = "Convert(id_voiture, 'System.String') like " + "'%" + champRecherche + "%'";//on lance une recherche sur la colonne ici "id_voiture" avec tous éléments comportant des caractères qui match avec champRecherche au fur et à mesure de la saisie
                            break;//J'ai du convertir ici les éléments int de la colonne id en string pour qu'elles puissent être exploitables avec le LIKE, pareil pour Année en dessous

                        case "Marque":
                            monDataview.RowFilter = @"nom_marque like " + "'%" + champRecherche + "%'";
                            break;
                        case "Modele":
                            monDataview.RowFilter = @"nom_modele like " + "'%" + champRecherche + "%'";
                            break;
                        case "Annee":
                            monDataview.RowFilter = "Convert(nom_annee,'System.String') like " + "'%" + champRecherche + "%'";
                            break;
                        default:
                            monDataview.RowFilter = "";
                            break;
                    }
                }
                else
                {
                    monDataview.RowFilter = "";
                }
                OnpropertyChanged("MonDataView");
                OnpropertyChanged("ChampRecherche");
            }
        }

        private string radioButton;//Récupération sous forme de string de la valeur isChecked du radioButton sélectionné

        public string RadioButton
        {
            get { return radioButton; }
            set { radioButton = value;
            OnpropertyChanged("RadioButton");}
        }

        #region Propriétés Pour Modifier // Regroupe toutes les propriétés bindés avec la vue modifier

        private string infoErreurSaisie;//Champ Label qui informera d'une erreur de saisie

        public string InfoErreurSaisie
        {
            get { return infoErreurSaisie; }
            set
            {
                infoErreurSaisie = value;
                OnpropertyChanged("InfoErreurSaisie");
            }
        }
        private static int selectedIndexOptionsDisponibles;//SelectedIndex du ListBox de la liste d'options préchargées

        public int SelectedIndexOptionsDisponibles
        {
            get { return selectedIndexOptionsDisponibles; }
            set
            {
                selectedIndexOptionsDisponibles = value;
                OnpropertyChanged("SelectedIndexAjoutOptions");
            }
        }

        private static int selectedIndexOptionsAjouter;

        public int SelectedIndexOptionsAjouter
        {
            get { return selectedIndexOptionsAjouter; }
            set
            {
                selectedIndexOptionsAjouter = value;
                OnpropertyChanged("SelectedIndexOptionsAjouter");
            }
        }

        private ObservableCollection<Model.OptionsProp> optionsVoitureAjouter = new ObservableCollection<Model.OptionsProp>();//ListBox des options qui sont ajoutées

        public ObservableCollection<Model.OptionsProp> OptionsVoitureAjouter
        {
            get { return optionsVoitureAjouter; }
            set
            {
                optionsVoitureAjouter = value;
                OnpropertyChanged("OptionsVoitureAjouter");
            }
        }

        private int selectedValueOptionsAjouter;

        public int SelectedValueOptionsAjouter
        {
            get { return selectedValueOptionsAjouter; }
            set
            {
                selectedValueOptionsAjouter = value;
                OnpropertyChanged("SelectedValueOptionsAjouter");
            }
        }


        Model.Options NewOptions = new Model.Options();

        private ObservableCollection<Model.OptionsProp> optionsVoiture;//ListBox des options qui sont préchargées et prêtes à petre ajoutées

        public ObservableCollection<Model.OptionsProp> OptionsVoiture
        {
            get { return NewOptions.OptionsVoiture; }
            set
            {
                NewOptions.OptionsVoiture = value;
                OnpropertyChanged("OptionsVoiture");
            }
        }

        private int selectedValueOptions;

        public int SelectedValueOptions
        {
            get { return selectedValueOptions; }
            set
            {
                selectedValueOptions = value;
                OnpropertyChanged("SelectedValueOptions");
            }
        }
        private static int selectedIndex;//Index de la ligne cliqué dans le dataGrid qui affiche les voitures

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnpropertyChanged("SelectedIndex");
                OnpropertyChanged("Marque");
                OnpropertyChanged("DataViewOptions");//Permet de mettre à jour le dataGrid Options
            }
        }

        public int Idselectionne//Récupère i'ID de la voiture 
        {
            get { return (int)monDataview[selectedIndex][1]; }//Le fait de faire la recherche dans le Dataview et pas directement sur le Datatable permet d'avoir toujours la bonne valeur d'id_voiture même si on change l'ordre de classement du tableau
        }

        private string marque;

        public string Marque
        {
            get { return monDataview[SelectedIndex][12].ToString();} // On récupère la valeur qui se trouve dans la conne 12 de la table à la ligne numéro"selectedIndex" qui est sélectionnée dans le datagrid
            set { marque = value;
                OnpropertyChanged("Marque");
            }
        }

        private string modele;

        public string Modele
        {
            get { return monDataview[SelectedIndex][11].ToString();}
            set { modele = value; }
        }

        Model.Puissance NewPuissance = new Model.Puissance();

        private ObservableCollection<Model.PuissanceProp> puissance;//Alimentation du ComboBox Puissance dans la vue Modifier et Ajouter

        public ObservableCollection<Model.PuissanceProp> Puissance
        {
            get { return  NewPuissance.PuissanceVoiture;}
            set { puissance = value; }
        }

        private int selectedValuePuissance;//Récupération de la valeur de l'id puissance qui nous servira pour la modification et ajout

        public int SelectedValuePuissance
        {
            get { return selectedValuePuissance; }
            set { selectedValuePuissance = value;
                OnpropertyChanged("SelectedValuePuissance");
            }
        }


        private int places;

        public int Places
        {
            get { return (int)monDataview[SelectedIndex][10];}
            set { places = value; }
        }

        Model.Couleur NewCouleur = new Model.Couleur(); // Instance de la classe Couleur

        private ObservableCollection<Model.CouleurProp> couleurVoiture;

        public ObservableCollection<Model.CouleurProp> CouleurVoiture // ObservableCollection qui renvoie le contenu de l'Observablecollection NewCouleur.CouleurVoiture au comboBox associé dans la vue Modifier
        {
            get { return NewCouleur.CouleurVoiture; }
            set { couleurVoiture = value; }
        }

        private int selectedValueCouleur;

        public int SelectedValueCouleur // Récupère l'ID de couleur qui est choisi dans le ComboBox qu'on pourra ensuite consommer pour effectuer une requête update
        {
            get { return selectedValueCouleur; }
            set { selectedValueCouleur = value;
                OnpropertyChanged("SelectedValue");
            }
        }

        Model.Carburant NewCarburant = new Model.Carburant();

        private ObservableCollection<Model.CarburantProp> carburantVoiture;

        public ObservableCollection<Model.CarburantProp> CarburantVoiture
        {
            get { return NewCarburant.CarburantVoiture; }
            set { carburantVoiture = value;
                OnpropertyChanged("CarburantVoiture");
            }
        }

        private int selectedValueCarburant;

        public int SelectedValueCarburant
        {
            get { return selectedValueCarburant; }
            set { selectedValueCarburant = value;
                OnpropertyChanged("SelectedValueCarburant");
            }
        }

        Regex RegexControleSaisieChiffre = new Regex(@"[0-9]");

        private string immatriculation = monDataview[selectedIndex][4].ToString();        

        public string Immatriculation
        {
            get
            {                
                return immatriculation;
            }
            set { immatriculation = value;
                OnpropertyChanged("Immatriculation");
            }
        }

        Model.Annee NewAnnee = new Model.Annee();

        private ObservableCollection<Model.AnneeProp> anneeVoiture;

        public ObservableCollection<Model.AnneeProp> AnneeVoiture
        {
            get { return NewAnnee.AnneeVoiture; }
            set { anneeVoiture = value;
                OnpropertyChanged("AnneeVoiture");
            }
        }

        private int selectedValueAnnee;

        public int SelectedValueAnnee
        {
            get { return selectedValueAnnee; }
            set { selectedValueAnnee = value;
                OnpropertyChanged("SelectedValueAnnee");
            }
        }

        private int prixAchat = (int)monDataview[selectedIndex][2];

        public int PrixAchat
        {
            get { return prixAchat = (int)monDataview[selectedIndex][2]; }
            set { prixAchat = value; }
        }

        private string coteArgus = monDataview[selectedIndex][3].ToString();

        public string CoteArgus
        {
            get
            {
                return coteArgus;
            }
            set
            {
                Regex RegexControleSaisieChiffre = new Regex("^[0-9]*$");//Vérifiacation que les caractères saisies sont bien des chiffres

                if (!RegexControleSaisieChiffre.IsMatch(value))//Si ne contient pas que des chiffres
                {
                    InfoErreurSaisie = "Merci de ne saisir que des chiffres dans le champ côte argus";// On affiche un message d'erreur
                    coteArgus = coteArgus + "";//On n'ajoute pas le nouveau caractère
                    OnpropertyChanged("ArgusVoiture");
                    OnpropertyChanged("InfoErreurSaisie");
                }
                else
                {
                    coteArgus = value;//Sinon on ajoute le nouveau caractère
                    infoErreurSaisie = "";
                    OnpropertyChanged("ArgusVoiture");//On efface le message d'erreur
                    OnpropertyChanged("InfoErreurSaisie");//On efface le message d'erreur
                
            }
            }
        }

        Model.Transmission NewTransmission = new Model.Transmission();

        private ObservableCollection<Model.TransmissionProp> transmissionVoiture;

        public ObservableCollection<Model.TransmissionProp> TransmissionVoiture
        {
            get { return NewTransmission.TransmissionVoiture; }
            set { transmissionVoiture = value; }
        }

        private int selectedValueTransmission;

        public int SelectedValueTransmission
        {
            get { return selectedValueTransmission; }
            set { selectedValueTransmission = value;
                OnpropertyChanged("SelectedValueTransmission");
            }
        }

        Model.Categorie NewCategorie = new Model.Categorie();

        private ObservableCollection<Model.CategorieProp> categorieVoiture;

        public ObservableCollection<Model.CategorieProp> CategorieVoiture
        {
            get { return NewCategorie.CategorieVoiture; }
            set { categorieVoiture = value; }
        }

        private int selectedValueCategorie;

        public int SelectedValueCategorie
        {
            get { return selectedValueCategorie; }
            set { selectedValueCategorie = value;
                OnpropertyChanged("SelectedValueCategorie");
            }
        }

        private string photoVoiture = monDataview[selectedIndex][13].ToString();

        public string PhotoVoiture
        {
            get { return photoVoiture; }            
        }

        #endregion
        #region Gestion des fonctions des ICommand
        private void SupprimerOptions(object o)//Retire une option de la ListBox"Options à ajouter"
        {
            if (SelectedIndexOptionsAjouter >= 0)
            {
                switch (selectedValueOptionsAjouter)
                {
                    case 1:
                        OptionsVoiture.Add(new Model.OptionsProp { NomOptions = "Toit ouvrant", IdOptions = 1 });//On ajoute l'option à la listsBox"Options disponbles"
                        OptionsVoitureAjouter.RemoveAt(SelectedIndexOptionsAjouter);//On supprime l'option en question de la ListBox "Options à ajouter"
                        break;
                    case 2:
                        OptionsVoiture.Add(new Model.OptionsProp { NomOptions = "Radar de recul", IdOptions = 2 });
                        OptionsVoitureAjouter.RemoveAt(SelectedIndexOptionsAjouter);
                        break;
                    case 3:
                        OptionsVoiture.Add(new Model.OptionsProp { NomOptions = "Sièges chauffants", IdOptions = 3 });
                        OptionsVoitureAjouter.RemoveAt(SelectedIndexOptionsAjouter);
                        break;
                    case 4:
                        OptionsVoiture.Add(new Model.OptionsProp { NomOptions = "Système de navigation", IdOptions = 4 });
                        OptionsVoitureAjouter.RemoveAt(SelectedIndexOptionsAjouter);
                        break;
                    case 5:
                        OptionsVoiture.Add(new Model.OptionsProp { NomOptions = "Sellerie cuir", IdOptions = 5 });
                        OptionsVoitureAjouter.RemoveAt(SelectedIndexOptionsAjouter);
                        break;
                    case 6:
                        OptionsVoiture.Add(new Model.OptionsProp { NomOptions = "Régulateur de vitesse", IdOptions = 6 });
                        OptionsVoitureAjouter.RemoveAt(SelectedIndexOptionsAjouter);
                        break;
                    default:
                        break;
                }
            }
        }

        private void AjoutOptions(object o)//Ajoute une option à la ListBox "Options à ajouter"
        {
            if (SelectedIndexOptionsDisponibles >= 0)
            {
                switch (selectedValueOptions)
                {
                    case 1:
                        OptionsVoitureAjouter.Add(new Model.OptionsProp { NomOptions = "Toit ouvrant", IdOptions = 1 });//On ajoute l'option à la listsBox"Options à ajouter"
                        OptionsVoiture.RemoveAt(SelectedIndexOptionsDisponibles);//On supprime l'option en question de la ListBox "Options disponibles"
                        break;
                    case 2:
                        OptionsVoitureAjouter.Add(new Model.OptionsProp { NomOptions = "Radar de recul", IdOptions = 2 });
                        OptionsVoiture.RemoveAt(SelectedIndexOptionsDisponibles);
                        break;
                    case 3:
                        OptionsVoitureAjouter.Add(new Model.OptionsProp { NomOptions = "Sièges chauffants", IdOptions = 3 });
                        OptionsVoiture.RemoveAt(SelectedIndexOptionsDisponibles);
                        break;
                    case 4:
                        OptionsVoitureAjouter.Add(new Model.OptionsProp { NomOptions = "Système de navigation", IdOptions = 4 });
                        OptionsVoiture.RemoveAt(SelectedIndexOptionsDisponibles);
                        break;
                    case 5:
                        OptionsVoitureAjouter.Add(new Model.OptionsProp { NomOptions = "Sellerie cuir", IdOptions = 5 });
                        OptionsVoiture.RemoveAt(SelectedIndexOptionsDisponibles);
                        break;
                    case 6:
                        OptionsVoitureAjouter.Add(new Model.OptionsProp { NomOptions = "Régulateur de vitesse", IdOptions = 6 });
                        OptionsVoiture.RemoveAt(SelectedIndexOptionsDisponibles);
                        break;
                    default:
                        break;

                }
            }
        }

        private bool VerifModifier(object o)//Bouton Modifier ne s'active que si un élément est séléctionné
        {
            if(SelectedIndex >= 0)
            {
                return true;
            }
            return false;
        }

        private bool VerifSupprimer(object o)//Bouton Supprimer ne s'active que si un élément est séléctionné
        {
            if (SelectedIndex >= 0)
            {
                return true;
            }
            return false;
        }

        private void Ajouter(object o)
        {
            View.Ajouter ajouterFen = new View.Ajouter();//Instanciation de la fenêtre Ajouter
            ajouterFen.ShowDialog();//Ouverture de la fenêtre Ajouter
        }

        private void Modifier(object o)
        {
            View.Modifier modifFenetre = new View.Modifier();//Instanciation de la fenêtre Modifier
            modifFenetre.ShowDialog();//Ouverture de la fenêtre Modifier
        }
        private void Supprimer(object o)
        {
            var confirmationSuppression = MessageBox.Show("Vous êtes sur le point de supprimer le véhicule " + Marque + " " + Modele + " correspondant à l'Id : " + Idselectionne,"Supression",MessageBoxButton.YesNo,MessageBoxImage.Warning);///Demande de confirmation avant supression
            if (confirmationSuppression == MessageBoxResult.Yes)
            {
                Model.Services Suppression = new Model.Services(Idselectionne);//J'envoi l'ID à supprimer
                AutoClosingMessageBox.Show("Le véhicule a bien été supprimé.", "Confirmation", 2500);///Confirmation de supression
            }
        }     

        private void Sauvegarder(object o)
        {
            
            var confirmationAjout = MessageBox.Show("Souhaitez-vous vraiment modifier ce véhicule ?", "Modification", MessageBoxButton.YesNo);//MessageBox demandant la confirmation de suppression
            if(confirmationAjout == MessageBoxResult.Yes)//Si confirmation de l'utilisateur, on lance la procédure
            {
                Window win = o as Window;//On instancie win qui représente la fenêtre Ajouter

                //On efface les lignes présentes dans le Dataview correspondant à l'id_voiture en cours de modification car le Dataview ne se met pas seul à jour
                for (int i = 0; dataViewOptions.Count > 0; i++)
                {
                    DataRow currentRow = dataViewOptions[0].Row;//Suprimmer à chaque fois la première ligne
                    if ((int)currentRow["id_voiture"] == Idselectionne)
                    {
                        currentRow.Delete();
                    }
                }
                Model.Services Modification = new Model.Services(Convert.ToInt32(CoteArgus), Immatriculation, SelectedValuePuissance, SelectedValueCouleur, SelectedValueCarburant, SelectedValueTransmission, SelectedValueCategorie, SelectedValueAnnee, Idselectionne,OptionsVoitureAjouter);//appel du constructeur pour modification
                win.Close();//On ferme la fenêtre Modifier
                AutoClosingMessageBox.Show("Votre véhicule " + Modele + " a bien été modifié", "Confirmation", 2500);//Message de confirmation de modification
            }          
        }

        private bool VerifAjoutOptions(object o)//Fleche verte grisée si plus d'options dans ListBox"Options Disponibles"
        {
            if (OptionsVoiture.Count == 0)
            {
                return false;
            }
            return true;
        }

        private bool VerifSupprimerOptions(object o)//Fleche verte grisée si plus d'options dans ListBox"Options à ajouter"
        {
            if (OptionsVoitureAjouter.Count == 0)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
