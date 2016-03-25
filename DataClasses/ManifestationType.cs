using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_2016_Project.DataClasses
{
    public class ManifestationType
    {
        public String label;
        public String name;
        public String description;
        public String iconSrc;

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
                    //OnPropertyChanged("Label");
                }
            }
        }

        public ManifestationType(String label, String name, String description, String iconSrc)
        {
            this.label = label;
            this.name = name;
            this.description = description;
            this.iconSrc = iconSrc;
        }
    }
}
