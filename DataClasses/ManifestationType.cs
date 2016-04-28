using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.Serialization;

namespace HCI_2016_Project.DataClasses
{
    [Serializable]
    public class ManifestationType : INotifyPropertyChanged
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
        public String Label
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
        public String Name
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
        public String Description
        {
            get
            {
                return description;
            }

            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        #endregion

        #region Icon Source Getter and Setter
        public String IconSrc
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

        #region Public Constructor with parameters
        public ManifestationType(String label, String name, String description, String iconSrc)
        {
            this.label = label;
            this.name = name;
            this.description = description;
            this.iconSrc = iconSrc;
        }
        #endregion

        public override string ToString()
        {
            return this.Name;
        }

        #region Attributes
        private String label;
        private String name;
        private String description;
        private String iconSrc;
        #endregion
    }
}
