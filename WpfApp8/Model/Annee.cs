using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Annee
    {
        private ObservableCollection<AnneeProp> anneeVoiture = new ObservableCollection<AnneeProp>();

        public ObservableCollection<AnneeProp> AnneeVoiture
        {
            get
            {
                return anneeVoiture;
            }
        }

        public Annee()
        {
            anneeVoiture.Add(new AnneeProp { NomAnnee = 2010, IdAnnee = 1 });
            anneeVoiture.Add(new AnneeProp { NomAnnee = 2011, IdAnnee = 2 });
            anneeVoiture.Add(new AnneeProp { NomAnnee = 2012, IdAnnee = 3 });
            anneeVoiture.Add(new AnneeProp { NomAnnee = 2013, IdAnnee = 4 });
            anneeVoiture.Add(new AnneeProp { NomAnnee = 2014, IdAnnee = 5 });
            anneeVoiture.Add(new AnneeProp { NomAnnee = 2015, IdAnnee = 6 });
            anneeVoiture.Add(new AnneeProp { NomAnnee = 2016, IdAnnee = 7 });
            anneeVoiture.Add(new AnneeProp { NomAnnee = 2017, IdAnnee = 8 });

        }
    }
    public class AnneeProp
    {
        public int NomAnnee { get; set; }
        public int IdAnnee { get; set; }
    }
}
