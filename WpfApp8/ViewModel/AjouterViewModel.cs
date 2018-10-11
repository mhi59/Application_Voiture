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
    class AjouterViewModel : Helpers ,INotifyPropertyChanged
    {
        public ICommand BoutonAjouter { get; set; }//Binding bouton Ajouter 
        public ICommand BoutonAjoutOptions { get; set; }////Binding bouton Ajouter (flèche verte)
        public ICommand BoutonSupprimerOptions { get; set; }////Binding bouton Ajouter (flèche rouge)
        public RelayCommand CloseWindowCommand { get; set; }//Permet de fermer la fenêtre en récupérant depuis la vue la Window ouverte

        public AjouterViewModel()
        {
            BoutonAjouter = new RelayCommand(o => Ajouter(o), o => VerifAjouter(o));
            BoutonAjoutOptions = new RelayCommand(o => AjoutOptions(o), o => VerifAjoutOptions(o));//fleche verte
            BoutonSupprimerOptions = new RelayCommand(o => SupprimerOptions(o), o => VerifSupprimerOptions(o));//fleche rouge
            CloseWindowCommand = new RelayCommand(Ajouter);//On envoie la gestion du bouton qui va gérer la fermeture de fenêtre à la méthode Ajouter
        }
          
        #region Gestion des Boutons Ajouter/Annuler
          

        private void Ajouter(object o)
        {
            var resultat = MessageBox.Show("Etes-vous sûr de vouloir ajouter ce véhicule?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultat == MessageBoxResult.Yes)
            {
                Window win = o as Window;//On instancie win qui représente la fenêtre Ajouter
                //Appelle du constructeur dans Model.Services pour Ajout Véhicule
                Model.Services NouvelAjout = new Model.Services(Convert.ToInt32(PrixVoiture), Convert.ToInt32(ArgusVoiture), ImmatriculationVoiture, SelectedValueTransmission, SelectedValueCouleur, SelectedValuePuissance, SelectedValueCarburant, SelectedValueAnnee, SelectedValuePlaces, SelectedValueModel, SelectedValueCategorie, OptionsVoitureAjouter);
                win.Close();//On ferme la fenêtre
                AutoClosingMessageBox.Show("Votre voiture a bien été ajoutée", "Enregistrement Ok", 2000);//Message confirmation d'ajout qui disparaît après 2.5 secondes                
            }
        }

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
        private bool VerifAjouter(object o)//Bouton ajouter est grisé si tous les champs ne sont pas rempli
        {
            if(SelectedValueModel != 0 && SelectedValuePuissance != 0 && SelectedValuePlaces != 0 && SelectedValueCouleur != 0 && SelectedValueCarburant != 0 && SelectedValueCarburant != 0 && SelectedValueAnnee != 0 && SelectedValuePuissance != 0 && SelectedValueTransmission != 0 && SelectedValueCategorie != 0 && ImmatriculationVoiture != "" && ArgusVoiture != "" && PrixVoiture != "")
            {
                IsEnabledBoutonCacheCloseWindow = "True";
                return true;
            }
            IsEnabledBoutonCacheCloseWindow = "False";            
            return false;
        }

        private bool VerifAjoutOptions(object o)//Fleche verte grisée si plus d'options dans ListBox"Options Disponibles"
        {
            if(OptionsVoiture.Count == 0)
            {
                return false;
            }
            return true;
        }

        private bool VerifSupprimerOptions(object o)//Fleche verte grisée si plus d'options dans ListBox"Options à ajouter"
        {
            if(OptionsVoitureAjouter.Count == 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Propriété des ComboBox et TextBox bindés avec la vue Ajouter
      
        private static int selectedIndexOptionsDisponibles;//SelectedIndex du ListBox de la liste d'options préchargées

        public int SelectedIndexOptionsDisponibles
        {
            get { return selectedIndexOptionsDisponibles; }
            set {
                selectedIndexOptionsDisponibles = value;
                OnpropertyChanged("SelectedIndexAjoutOptions");
            }
        }

        private static int selectedIndexOptionsAjouter;

        public int SelectedIndexOptionsAjouter
        {
            get { return selectedIndexOptionsAjouter; }
            set { selectedIndexOptionsAjouter = value;
                OnpropertyChanged("SelectedIndexOptionsAjouter");
            }
        }

        private string isEnabledBoutonCacheCloseWindow;//Bindé avec un bouton caché derrière le bouton Ajouter qui va déclencher la fermeture de la fenêtre, est désactivé si tous les champs ne sont pas remplis

        public string IsEnabledBoutonCacheCloseWindow
        {
            get { return isEnabledBoutonCacheCloseWindow; }
            set { isEnabledBoutonCacheCloseWindow = value;
                OnpropertyChanged("IsEnabledBoutonCacheCloseWindow");
            }
        }

        private ObservableCollection<Model.OptionsProp> optionsVoitureAjouter = new ObservableCollection<Model.OptionsProp>();//ListBox des options qui sont ajoutées

        public ObservableCollection<Model.OptionsProp> OptionsVoitureAjouter
        {
            get { return optionsVoitureAjouter; }
            set { optionsVoitureAjouter = value;
                OnpropertyChanged("OptionsVoitureAjouter");
            }
        }

        private int selectedValueOptionsAjouter;

        public int SelectedValueOptionsAjouter
        {
            get { return selectedValueOptionsAjouter; }
            set { selectedValueOptionsAjouter = value;
                OnpropertyChanged("SelectedValueOptionsAjouter");
            }
        }


        Model.Options NewOptions = new Model.Options();


        public ObservableCollection<Model.OptionsProp> OptionsVoiture
        {
            get { return NewOptions.OptionsVoiture; }
            set { NewOptions.OptionsVoiture = value;
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

        private string infoErreurSaisie;//Champ Label qui informera d'une erreur de saisie

        public string InfoErreurSaisie
        {
            get { return infoErreurSaisie; }
            set { infoErreurSaisie = value;
                OnpropertyChanged("InfoErreurSaisie");
            }
        }


        private int selectedIndedMarque; // Sert à récupérer le SelectedIndex de Marque pour pouvoir afficher les modèles correspondant à la marque

        public int SelectedIndexMarque
        {
            get { return selectedIndedMarque; }
            set
            {
                selectedIndedMarque = value;
                OnpropertyChanged("SelectedIndexMarque");
                OnpropertyChanged("ModelVoiture");//On signale le changement pour recharger dans le ComboBox
            }
        }

        Model.Modele NewModele = new Model.Modele();

        public ObservableCollection<Model.ModeleProp> ModelVoiture
        {
            get
            {
                switch (SelectedIndexMarque)
                {
                    case 0:
                        return NewModele.ModeleAudi;
                    case 1:
                        return NewModele.ModeleBMW;
                    case 2:
                        return NewModele.ModeleCitroen;
                    case 3:
                        return NewModele.ModeleDS;
                    case 4:
                        return NewModele.ModeleMercedes;
                    case 5:
                        return NewModele.ModelePeugeot;
                    case 6:
                        return NewModele.ModeleRenault;
                    case 7:
                        return NewModele.ModeleTesla;
                    case 8:
                        return NewModele.ModeleToyota;
                    case 9:
                        return NewModele.ModeleVolksWagen;
                }
                OnpropertyChanged("ModelVoiture");
                return NewModele.ModeleAudi;
            }
        }

        private int selectedValueModel;

        public int SelectedValueModel
        {
            get { return selectedValueModel; }
            set
            {
                selectedValueModel = value;
                OnpropertyChanged("SelectedValueModel");
            }
        }

        Model.Puissance NewPuissance = new Model.Puissance();

        public ObservableCollection<Model.PuissanceProp> PuissanceVoiture
        {
            get { return NewPuissance.PuissanceVoiture; }
        }

        private int selectedValuePuissance;

        public int SelectedValuePuissance
        {
            get { return selectedValuePuissance; }
            set
            {
                selectedValuePuissance = value;
                OnpropertyChanged("SelectedValuePuissance");
            }
        }

        Model.Places NewPlaces = new Model.Places();

        public ObservableCollection<Model.PlacesProp> PlacesVoiture
        {
            get { return NewPlaces.PlacesVoiture;}
        }

        private int selectedValuePlaces;

        public int SelectedValuePlaces
        {
            get { return selectedValuePlaces; }
            set
            {
                selectedValuePlaces = value;
                OnpropertyChanged("SelectedValuePlaces");
            }
        }

        Model.Couleur NewCouleur = new Model.Couleur(); 
        
        public ObservableCollection<Model.CouleurProp> CouleurVoiture 
        {
            get { return NewCouleur.CouleurVoiture; }
        }

        private int selectedValueCouleur;

        public int SelectedValueCouleur 
        {
            get { return selectedValueCouleur; }
            set
            {
                selectedValueCouleur = value;
                OnpropertyChanged("SelectedValue");
            }
        }

        Model.Carburant NewCarburant = new Model.Carburant();
        
        public ObservableCollection<Model.CarburantProp> CarburantVoiture
        {
            get { return NewCarburant.CarburantVoiture; }    
        }

        private int selectedValueCarburant;

        public int SelectedValueCarburant
        {
            get { return selectedValueCarburant; }
            set
            {
                selectedValueCarburant = value;
                OnpropertyChanged("SelectedValueCarburant");
            }
        }

        private string immatriculationVoiture;

        public string ImmatriculationVoiture
        {
            get { return immatriculationVoiture; }
            set { immatriculationVoiture = value;
                OnpropertyChanged("ImmatriculationVoiture");
            }
        }


        Model.Annee NewAnnee = new Model.Annee();

        public ObservableCollection<Model.AnneeProp> AnneeVoiture
        {
            get { return NewAnnee.AnneeVoiture; }
            set
            {
                OnpropertyChanged("AnneeVoiture");
            }
        }

        private int selectedValueAnnee;

        public int SelectedValueAnnee
        {
            get { return selectedValueAnnee; }
            set
            {
                selectedValueAnnee = value;
                OnpropertyChanged("SelectedValueAnnee");
            }
        }

        private string prixVoiture;

        public string PrixVoiture
        {
            get
            {
                return prixVoiture;
            }
            set
            {
                Regex RegexControleSaisieChiffre = new Regex("^[0-9]*$");//Vérifiacation que les caractères saisies sont bien des chiffres

                if (!RegexControleSaisieChiffre.IsMatch(value))//Si ne contient pas que des chiffres
                {
                    InfoErreurSaisie = "Merci de ne saisir que des chiffres dans le champ côte Prix";// On affiche un message d'erreur
                    prixVoiture = prixVoiture + "";//On n'ajoute pas le nouveau caractère
                    OnpropertyChanged("PrixVoiture");
                    OnpropertyChanged("InfoErreurSaisie");
                }
                else
                {
                    prixVoiture = value;//Sinon on ajoute le nouveau caractère
                    infoErreurSaisie = "";
                    OnpropertyChanged("ArgusVoiture");//On efface le message d'erreur
                    OnpropertyChanged("InfoErreurSaisie");//On efface le message d'erreur
                }
            }
        }

        private static string argusVoiture;
        public string ArgusVoiture
        {
            get
            {              
                return argusVoiture;
            }
            set
            {
                Regex RegexControleSaisieChiffre = new Regex("^[0-9]*$");//Vérifiacation que les caractères saisies sont bien des chiffres

                if (!RegexControleSaisieChiffre.IsMatch(value))//Si ne contient pas que des chiffres
                {
                    InfoErreurSaisie = "Merci de ne saisir que des chiffres dans le champ côte argus";// On affiche un message d'erreur
                    argusVoiture = argusVoiture + "";//On n'ajoute pas le nouveau caractère
                    OnpropertyChanged("ArgusVoiture");
                    OnpropertyChanged("InfoErreurSaisie");
                }
                else
                {
                    argusVoiture = value;//Sinon on ajoute le nouveau caractère
                    infoErreurSaisie = "";//On efface le message d'erreur
                    OnpropertyChanged("ArgusVoiture");
                    OnpropertyChanged("InfoErreurSaisie");//On efface le message d'erreur
                }
            }
        }

        Model.Transmission NewTransmission = new Model.Transmission();

        public ObservableCollection<Model.TransmissionProp> TransmissionVoiture
        {
            get { return NewTransmission.TransmissionVoiture; }
        }

        private int selectedValueTransmission;

        public int SelectedValueTransmission
        {
            get { return selectedValueTransmission; }
            set
            {
                selectedValueTransmission = value;
                OnpropertyChanged("SelectedValueTransmission");
            }
        }

        Model.Categorie NewCategorie = new Model.Categorie();
        
        public ObservableCollection<Model.CategorieProp> CategorieVoiture
        {
            get { return NewCategorie.CategorieVoiture; }
        }

        private int selectedValueCategorie;

        public int SelectedValueCategorie
        {
            get { return selectedValueCategorie; }
            set
            {
                selectedValueCategorie = value;
                OnpropertyChanged("SelectedValueCategorie");
            }
        }



        #endregion
        
    }
}
