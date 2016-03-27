using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace HCI_2016_Project.DataClasses
{
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
        private String description;
        #endregion
    }
}
