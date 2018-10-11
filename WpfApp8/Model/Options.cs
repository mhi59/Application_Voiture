using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Options
    {
        private ObservableCollection<OptionsProp> optionsVoiture = new ObservableCollection<OptionsProp>();

        public ObservableCollection<OptionsProp> OptionsVoiture
        {
            get
            {
                return optionsVoiture;
            }
            set
            {
                optionsVoiture = value;
            }
        }

        public Options()
        {
            optionsVoiture.Add(new OptionsProp { NomOptions = "Toit ouvrant", IdOptions = 1 });
            optionsVoiture.Add(new OptionsProp { NomOptions = "Radar de recul", IdOptions = 2 });
            optionsVoiture.Add(new OptionsProp { NomOptions = "Sièges chauffants", IdOptions = 3 });
            optionsVoiture.Add(new OptionsProp { NomOptions = "Système de navigation", IdOptions = 4 });
            optionsVoiture.Add(new OptionsProp { NomOptions = "Sellerie cuir", IdOptions = 5 });
            optionsVoiture.Add(new OptionsProp { NomOptions = "Régulateur de vitesse", IdOptions = 6 });
        }
    }

    public class OptionsProp
    {
        public string NomOptions { get; set; }
        public int IdOptions { get; set; }
    }
}
