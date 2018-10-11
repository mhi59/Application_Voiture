using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Puissance
    {


        public Puissance()
        {
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 7, IdPuissance = 1 });
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 8, IdPuissance = 2 });
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 9, IdPuissance = 3 });
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 10, IdPuissance = 4 });
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 11, IdPuissance = 5 });
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 12, IdPuissance = 6 });
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 13, IdPuissance = 7 });
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 14, IdPuissance = 8 });
            puissanceVoiture.Add(new PuissanceProp { NomPuissance = 15, IdPuissance = 9 });
        }



        private ObservableCollection<PuissanceProp> puissanceVoiture = new ObservableCollection<PuissanceProp>();

        public ObservableCollection<PuissanceProp> PuissanceVoiture
        {
            get
            {
                return puissanceVoiture;
            }
        }
    }

    public class PuissanceProp
    {
        public int NomPuissance { get; set; }
        public int IdPuissance { get; set; }
    }
}
