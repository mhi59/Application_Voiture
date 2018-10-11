using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Places
    {
        private ObservableCollection<PlacesProp> placesVoiture = new ObservableCollection<PlacesProp>();

        public ObservableCollection<PlacesProp> PlacesVoiture
        {
            get
            {
                return placesVoiture;
            }
        }

        public Places()
        {
            placesVoiture.Add(new PlacesProp { NomPlaces = 2, IdPlaces = 1 });
            placesVoiture.Add(new PlacesProp { NomPlaces = 5, IdPlaces = 2 });
            placesVoiture.Add(new PlacesProp { NomPlaces = 7, IdPlaces = 3 });
        }
    }

    public class PlacesProp
    {
        public int NomPlaces { get; set; }
        public int IdPlaces { get; set; }
    }
}
