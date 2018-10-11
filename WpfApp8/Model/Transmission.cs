using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Transmission
    {
        private ObservableCollection<TransmissionProp> transmissionVoiture = new ObservableCollection<TransmissionProp>();

        public ObservableCollection<TransmissionProp> TransmissionVoiture
        {
            get
            {
                return transmissionVoiture;
            }
        }

        public Transmission()
        {
            transmissionVoiture.Add(new TransmissionProp { NomTransmission = "Automatique", IdTransmission = 1 });
            transmissionVoiture.Add(new TransmissionProp { NomTransmission = "Manuelle", IdTransmission = 2 });            
        }


    }
    public class TransmissionProp
    {
        public string NomTransmission { get; set; }
        public int IdTransmission { get; set; }
    }
}
