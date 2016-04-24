using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace HCI_2016_Project.DataClasses
{
    // Enumeracija koja opisuje moguce vrijednosti za status alkohola
    public enum AlcoholStatusEnum 
        { NO_ALCOHOL, CAN_BUY, CAN_BRING }

    // Enumeracija koja opisuje moguce kategorije cijena
    public enum PriceCategoryEnum
        { FREE, LOW_PRICE, MEDIUM_PRICE, HIGH_PRICE }

    public class Manifestation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            Console.WriteLine("Changed" + name);
        }
        #endregion

        #region Label Getter and Setter
        public string Label
        {
            get
            {
                return label;
            }

            set
            {
                if (value != label)
                {
                    label = value;
                    OnPropertyChanged("Label");
                }
            }
        }
        #endregion 

        #region Name Getter and Setter
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        #endregion 

        #region Description Getter and Setter
        public string Description
        {
            get
            {
                return descripition;
            }

            set
            {
                if (value != descripition)
                {
                    descripition = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        #endregion 

        #region ManifestationType Getter and Setter
        public ManifestationType Type
        {
            get
            {
                return type;
            }

            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        #endregion 

        #region Date Getter and Setter
        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }
        #endregion 

        #region IconSource Getter and Setter
        public string IconSrc
        {
            get
            {
                return iconSrc;
            }

            set
            {
                if (value != iconSrc)
                {
                    iconSrc = value;
                    OnPropertyChanged("IconSrc");
                }
            }
        }
        #endregion 

        #region Accessible Getter and Setter
        public bool Accessible
        {
            get
            {
                return accessible;
            }

            set
            {
                if (value != accessible)
                {
                    accessible = value;
                    OnPropertyChanged("Accesible");
                }
            }
        }
        #endregion 

        #region SmokingAllowed Getter and Setter
        public bool SmokingAllowed
        {
            get
            {
                return smokingAllowed;
            }

            set
            {
                if (value != smokingAllowed)
                {
                    smokingAllowed = value;
                    OnPropertyChanged("SmokingAllowed");
                }
            }
        }
        #endregion 

        #region OutsideManifestation Getter and Setter
        public bool OutsideManifestation
        {
            get
            {
                return outsideManifestation;
            }

            set
            {
                if (value != outsideManifestation)
                {
                    outsideManifestation = value;
                    OnPropertyChanged("OutsideManifestation");
                }
            }
        }
        #endregion 

        #region AlcoholStatus Getter and Setter
        public AlcoholStatusEnum AlcoholStatus
        {
            get
            {
                return alcoholStatus;
            }

            set
            {
                if (value != alcoholStatus)
                {
                    alcoholStatus = value;
                    OnPropertyChanged("AlcoholStatus");
                }
            }
        }
        #endregion

        #region PriceCategory Getter and Setter
        public PriceCategoryEnum PriceCategory
        {
            get
            {
                return priceCategory;
            }

            set
            {
                if (value != priceCategory)
                {
                    priceCategory = value;
                    OnPropertyChanged("PriceCategory");
                }
            }
        }
        #endregion 

        #region GuestsExpected Getter and Setter
        public int GuestsExpected
        {
            get
            {
                return guestsExpected;
            }

            set
            {
                if (value != guestsExpected)
                {
                    guestsExpected = value;
                    OnPropertyChanged("GuestsExpected");
                }
            }
        }
        #endregion

        #region X Coordinate Getter and Setter
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                if (value != x)
                {
                    x = value;
                    OnPropertyChanged("X");
                }
            }
        }
        #endregion

        #region Y Coordinate Getter and Setter
        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                if (value != y)
                {
                    y = value;
                    OnPropertyChanged("Y");
                }
            }
        }
        #endregion

        #region Attributes
        private string label;
        private string name;
        private string descripition;
        private ManifestationType type;
        private DateTime date;
        private string iconSrc;
        private bool accessible;
        private bool smokingAllowed;
        private bool outsideManifestation;
        private AlcoholStatusEnum alcoholStatus;
        private PriceCategoryEnum priceCategory;
        private int guestsExpected;
        private int x;
        private int y;
        #endregion

    }
}
