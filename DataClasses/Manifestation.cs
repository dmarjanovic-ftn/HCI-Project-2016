using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace HCI_2016_Project.DataClasses
{
    enum AlcoholStatus 
        { NO_ALCOHOL, CAN_BUY, CAN_BRING }

    enum PriceCategory
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
            Console.Write("Test");
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
        private AlcoholStatus alcoholStatus;
        private PriceCategory priceCategory;
        private int guestsExpected;
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

    }
}
