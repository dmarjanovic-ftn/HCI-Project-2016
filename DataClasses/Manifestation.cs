using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace HCI_2016_Project.DataClasses
{
    public enum AlcoholStatusEnum 
        { NO_ALCOHOL, CAN_BUY, CAN_BRING }

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
            Console.Write("Test");
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

        #region Attributes
        private string label;
        private string name;
        private string descripition;
        private ManifestationType type;
        private DateTime date;
        private string iconSrc;

        private bool accessible;
        private AlcoholStatusEnum alcoholStatus;
        private PriceCategoryEnum priceCategory;
        private int guestsExpected;
        #endregion

    }
}
