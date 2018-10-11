using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8.Model
{
    public class Modele
    {
        #region Initialisation et return des ObservableCollection

        ObservableCollection<ModeleProp> modeleAudi = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modeleBMW = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modeleMercedes = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modeleVolksWagen = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modeleToyota = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modelePeugeot = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modeleRenault = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modeleCitroen = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modeleTesla = new ObservableCollection<ModeleProp>();
        ObservableCollection<ModeleProp> modeleDS = new ObservableCollection<ModeleProp>();
        
        public ObservableCollection<ModeleProp> ModeleAudi
        {
            get { return modeleAudi; }            
        }

        public ObservableCollection<ModeleProp> ModeleBMW
        {
            get { return modeleBMW; }           
        }

        public ObservableCollection<ModeleProp> ModeleMercedes
        {
            get { return modeleMercedes; }            
        }

        public ObservableCollection<ModeleProp> ModeleVolksWagen
        {
            get { return modeleVolksWagen; }
        }

        public ObservableCollection<ModeleProp> ModeleToyota
        {
            get { return modeleToyota; }
        }

        public ObservableCollection<ModeleProp> ModelePeugeot
        {
            get { return modelePeugeot; }
        }

        public ObservableCollection<ModeleProp> ModeleRenault
        {
            get { return modeleRenault; }
        }

        public ObservableCollection<ModeleProp> ModeleCitroen
        {
            get { return modeleCitroen; }
        }

        public ObservableCollection<ModeleProp> ModeleTesla
        {
            get { return modeleTesla; }
        }

        public ObservableCollection<ModeleProp> ModeleDS
        {
            get { return modeleDS; }
        }
        #endregion

        public Modele()
        {
            #region Alimentation des différents ObservableCollecton avec le nom des modèles et leur ID respectifs
            modeleAudi.Add(new ModeleProp { NomModele = "RS3", IdModel = 1 });
            modeleAudi.Add(new ModeleProp { NomModele = "S1", IdModel = 2 });
            modeleAudi.Add(new ModeleProp { NomModele = "R8", IdModel = 3 });
            modeleAudi.Add(new ModeleProp { NomModele = "A5", IdModel = 4 });
            modeleAudi.Add(new ModeleProp { NomModele = "A7", IdModel = 5 });
            modeleAudi.Add(new ModeleProp { NomModele = "A3", IdModel = 6 });
            modeleAudi.Add(new ModeleProp { NomModele = "S8", IdModel = 7 });
            modeleAudi.Add(new ModeleProp { NomModele = "RSQ3", IdModel = 8 });
            modeleAudi.Add(new ModeleProp { NomModele = "RS6", IdModel = 9 });
            modeleAudi.Add(new ModeleProp { NomModele = "SQ7", IdModel = 10 });

            modeleBMW.Add(new ModeleProp { NomModele = "I8", IdModel = 11 });
            modeleBMW.Add(new ModeleProp { NomModele = "M6", IdModel = 12 });
            modeleBMW.Add(new ModeleProp { NomModele = "X6", IdModel = 13 });
            modeleBMW.Add(new ModeleProp { NomModele = "Serie 1", IdModel = 14 });
            modeleBMW.Add(new ModeleProp { NomModele = "M4", IdModel = 15 });
            modeleBMW.Add(new ModeleProp { NomModele = "Serie 5", IdModel = 16 });
            modeleBMW.Add(new ModeleProp { NomModele = "Serie 2 Cab", IdModel = 17 });
            modeleBMW.Add(new ModeleProp { NomModele = "Serie 7", IdModel = 18 });
            modeleBMW.Add(new ModeleProp { NomModele = "M3", IdModel = 19 });
            modeleBMW.Add(new ModeleProp { NomModele = "Serie 2", IdModel = 20 });

            modeleMercedes.Add(new ModeleProp { NomModele = "A45 AMG", IdModel = 21});
            modeleMercedes.Add(new ModeleProp { NomModele = "GLA", IdModel = 22 });
            modeleMercedes.Add(new ModeleProp { NomModele = "GLC Coupé", IdModel = 23 });
            modeleMercedes.Add(new ModeleProp { NomModele = "Classe S coupé", IdModel = 24 });
            modeleMercedes.Add(new ModeleProp { NomModele = "AMG Roadster", IdModel = 25 });
            modeleMercedes.Add(new ModeleProp { NomModele = "GLC 350", IdModel = 26 });
            modeleMercedes.Add(new ModeleProp { NomModele = "Classe S", IdModel = 27 });
            modeleMercedes.Add(new ModeleProp { NomModele = "Classe B", IdModel = 28 });
            modeleMercedes.Add(new ModeleProp { NomModele = "Classe E", IdModel = 29});
            modeleMercedes.Add(new ModeleProp { NomModele = "GLE AMG", IdModel = 30 });


            modeleVolksWagen.Add(new ModeleProp { NomModele = "Golf GTE", IdModel = 31 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "Passat GTE", IdModel = 32 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "Coccinelle Cab", IdModel = 33 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "Arteon", IdModel = 34 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "Golf R", IdModel = 35 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "Tiguan", IdModel = 36 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "Touareg", IdModel = 37 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "Touran", IdModel = 38 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "Golf GTI", IdModel = 39 });
            modeleVolksWagen.Add(new ModeleProp { NomModele = "E-Up", IdModel = 40 });


            modeleToyota.Add(new ModeleProp { NomModele = "GT 86", IdModel = 41 });
            modeleToyota.Add(new ModeleProp { NomModele = "Avensis", IdModel = 42 });
            modeleToyota.Add(new ModeleProp { NomModele = "Auris", IdModel = 43 });
            modeleToyota.Add(new ModeleProp { NomModele = "RAV 4", IdModel = 44 });
            modeleToyota.Add(new ModeleProp { NomModele = "Land Cruiser", IdModel = 45 });
            modeleToyota.Add(new ModeleProp { NomModele = "Mirai", IdModel = 46 });
            modeleToyota.Add(new ModeleProp { NomModele = "Aygo", IdModel = 47 });
            modeleToyota.Add(new ModeleProp { NomModele = "Hilux", IdModel = 48 });
            modeleToyota.Add(new ModeleProp { NomModele = "Prius", IdModel = 49 });
            modeleToyota.Add(new ModeleProp { NomModele = "Yaris", IdModel = 50 });

            modelePeugeot.Add(new ModeleProp { NomModele = "208", IdModel = 51 });
            modelePeugeot.Add(new ModeleProp { NomModele = "208 GTI", IdModel = 52 });
            modelePeugeot.Add(new ModeleProp { NomModele = "308", IdModel = 53 });
            modelePeugeot.Add(new ModeleProp { NomModele = "308 GTI", IdModel = 54 });
            modelePeugeot.Add(new ModeleProp { NomModele = "508", IdModel = 55 });
            modelePeugeot.Add(new ModeleProp { NomModele = "2008", IdModel = 56 });
            modelePeugeot.Add(new ModeleProp { NomModele = "3008", IdModel = 57 });
            modelePeugeot.Add(new ModeleProp { NomModele = "5008", IdModel = 58 });
            modelePeugeot.Add(new ModeleProp { NomModele = "208 Business", IdModel = 59 });
            modelePeugeot.Add(new ModeleProp { NomModele = "508 Business", IdModel = 60 });

            modeleRenault.Add(new ModeleProp { NomModele = "Captur", IdModel = 61 });
            modeleRenault.Add(new ModeleProp { NomModele = "Clio RS", IdModel = 62 });
            modeleRenault.Add(new ModeleProp { NomModele = "Kadjar", IdModel = 63 });
            modeleRenault.Add(new ModeleProp { NomModele = "Talisman", IdModel = 64 });
            modeleRenault.Add(new ModeleProp { NomModele = "ZOE", IdModel = 65 });
            modeleRenault.Add(new ModeleProp { NomModele = "Scenic", IdModel = 66 });
            modeleRenault.Add(new ModeleProp { NomModele = "Mégane RS", IdModel = 67 });
            modeleRenault.Add(new ModeleProp { NomModele = "Koléos", IdModel = 68 });
            modeleRenault.Add(new ModeleProp { NomModele = "Alaskan", IdModel = 69 });
            modeleRenault.Add(new ModeleProp { NomModele = "Twingo", IdModel = 70 });

            modeleCitroen.Add(new ModeleProp { NomModele = "C-Zero", IdModel = 71 });
            modeleCitroen.Add(new ModeleProp { NomModele = "C4", IdModel = 72 });
            modeleCitroen.Add(new ModeleProp { NomModele = "C5", IdModel = 73 });
            modeleCitroen.Add(new ModeleProp { NomModele = "C3", IdModel = 74 });
            modeleCitroen.Add(new ModeleProp { NomModele = "C4 Cactus", IdModel = 75 });
            modeleCitroen.Add(new ModeleProp { NomModele = "C3 AirCross", IdModel = 76 });
            modeleCitroen.Add(new ModeleProp { NomModele = "C1", IdModel = 77 });
            modeleCitroen.Add(new ModeleProp { NomModele = "C5 AirCross", IdModel = 78 });
            modeleCitroen.Add(new ModeleProp { NomModele = "C4 Picasso", IdModel = 79 });
            modeleCitroen.Add(new ModeleProp { NomModele = "E-Mehari", IdModel = 80 });

            modeleTesla.Add(new ModeleProp { NomModele = "Model S", IdModel = 81 });
            modeleTesla.Add(new ModeleProp { NomModele = "Model X", IdModel = 82 });
            modeleTesla.Add(new ModeleProp { NomModele = "Model 3", IdModel = 83 });
            modeleTesla.Add(new ModeleProp { NomModele = "Roadster", IdModel = 84 });

            modeleDS.Add(new ModeleProp { NomModele = "DS3", IdModel = 85 });
            modeleDS.Add(new ModeleProp { NomModele = "DS3 Cab", IdModel = 86 });
            modeleDS.Add(new ModeleProp { NomModele = "DS4", IdModel = 87 });
            modeleDS.Add(new ModeleProp { NomModele = "DS4 Crossback", IdModel = 88 });
            modeleDS.Add(new ModeleProp { NomModele = "DS5", IdModel = 89 });
            modeleDS.Add(new ModeleProp { NomModele = "DS7 Crossback", IdModel = 90 });
            #endregion            
        }
    }

    //
    public class ModeleProp
    {
        public string NomModele { get; set; }
        public int IdModel { get; set; }
    }
}
