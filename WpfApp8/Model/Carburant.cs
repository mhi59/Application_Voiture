using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Carburant
    {
        private ObservableCollection<CarburantProp> carburantVoiture = new ObservableCollection<CarburantProp>();

        public ObservableCollection<CarburantProp> CarburantVoiture
        {
            get
            {
                return carburantVoiture;
            }
        }

        public Carburant()
        {
            carburantVoiture.Add(new CarburantProp { NomCarburant = "Essence", IdCarburant = 1 });
            carburantVoiture.Add(new CarburantProp { NomCarburant = "Diesel", IdCarburant = 2 });
            carburantVoiture.Add(new CarburantProp { NomCarburant = "Hybrid", IdCarburant = 3 });
            carburantVoiture.Add(new CarburantProp { NomCarburant = "Electrique", IdCarburant = 4 });
        }

       
    }
    public class CarburantProp
    {
        public string NomCarburant { get; set; }
        public int IdCarburant { get; set; }
    }
}
