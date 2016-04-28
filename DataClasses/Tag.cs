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
    public class Tag : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        public Tag()
        {

        }

        #region Mark Getter and Setter
        public string Mark
        {
            get
            {
                return mark;
            }

            set
            {
                if (value != mark)
                {
                    mark = value;
                    OnPropertyChanged("Mark");
                }
            }
        }
        #endregion 

        #region Color Getter and Setter
        public string Color
        {
            get
            {
                return color;
            }

            set
            {
                if (value != color)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        #endregion 

        #region Description Getter and Setter
        public string Description
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

        #region Attributes
        private String mark;
        private String color;
        private String description;
        #endregion
    }
}
