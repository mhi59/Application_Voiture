using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Categorie
    {
        private ObservableCollection<CategorieProp> categorieVoiture = new ObservableCollection<CategorieProp>();

        public ObservableCollection<CategorieProp> CategorieVoiture
        {
            get
            {
                return categorieVoiture;
            }
        }

        public Categorie()
        {
            categorieVoiture.Add(new CategorieProp { NomCategorie = "Luxe", IdCategorie = 1 });
            categorieVoiture.Add(new CategorieProp { NomCategorie = "Sport", IdCategorie = 2 });
            categorieVoiture.Add(new CategorieProp { NomCategorie = "Coupé", IdCategorie = 3 });
            categorieVoiture.Add(new CategorieProp { NomCategorie = "Break", IdCategorie = 4 });
            categorieVoiture.Add(new CategorieProp { NomCategorie = "SUV", IdCategorie = 5 });
            categorieVoiture.Add(new CategorieProp { NomCategorie = "Citadine", IdCategorie = 6 });
            categorieVoiture.Add(new CategorieProp { NomCategorie = "Berline", IdCategorie = 7 });
            categorieVoiture.Add(new CategorieProp { NomCategorie = "Monospace", IdCategorie = 8 });

        }
    }
    public class CategorieProp
    {
        public string NomCategorie { get; set; }
        public int IdCategorie { get; set; }
    }
}
