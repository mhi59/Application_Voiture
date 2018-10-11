using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Couleur
    {
        private ObservableCollection<CouleurProp> couleurVoiture = new ObservableCollection<CouleurProp>();

        public ObservableCollection<CouleurProp> CouleurVoiture
        {
            get
            {
                return couleurVoiture;
            }
        }

        public Couleur()
        {
            couleurVoiture.Add(new CouleurProp { NomCouleur = "Noir", IdCouleur = 1 });
            couleurVoiture.Add(new CouleurProp { NomCouleur = "Gris", IdCouleur = 2 });
            couleurVoiture.Add(new CouleurProp { NomCouleur = "Rouge", IdCouleur = 3 });
            couleurVoiture.Add(new CouleurProp { NomCouleur = "Jaune", IdCouleur = 4 });
            couleurVoiture.Add(new CouleurProp { NomCouleur = "Bleu", IdCouleur = 5 });
            couleurVoiture.Add(new CouleurProp { NomCouleur = "Vert", IdCouleur = 6 });
            couleurVoiture.Add(new CouleurProp { NomCouleur = "Blanc", IdCouleur = 7 });
            couleurVoiture.Add(new CouleurProp { NomCouleur = "Marron", IdCouleur = 8 });

        }
    }
    public class CouleurProp
    {
        public string NomCouleur { get; set; }
        public int IdCouleur  { get; set; }
    }
}
