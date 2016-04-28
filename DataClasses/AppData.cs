using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using HCI_2016_Project.Utils;

namespace HCI_2016_Project.DataClasses
{
    public class AppData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            Console.WriteLine("Changed list");
        }
        #endregion

        private AppData()
        {
           
        }

        public static AppData GetInstance()
        {
            if (instance == null)
            {
                instance = new AppData();
            }
            return instance;
        }

        #region Manifestations Getter and Setter
        public List<Manifestation> Manifestations
        {
            get
            {
                return manifestations;
            }

            set
            {
                if (value != manifestations)
                {
                    manifestations = value;
                    OnPropertyChanged("Manifestations");
                }
            }
        }
        #endregion 

        #region Tags Getter and Setter
        public List<Tag> Tags
        {
            get
            {
                return tags;
            }

            set
            {
                if (value != tags)
                {
                    tags = value;
                    OnPropertyChanged("Tags");
                }
            }
        }
        #endregion 

        #region Manifestation Types Getter and Setter
        public List<ManifestationType> ManifestationTypes
        {
            get
            {
                return manifestationTypes;
            }

            set
            {
                if (value != manifestationTypes)
                {
                    manifestationTypes = value;
                    OnPropertyChanged("ManifestationTypes");
                }
            }
        }
        #endregion 

        private static AppData instance = null;

        #region Lists of Manifestations, Tags and Manifestation Types
        private List<Manifestation> manifestations;
        private List<Tag> tags;
        private List<ManifestationType> manifestationTypes;
        #endregion
    }
}
