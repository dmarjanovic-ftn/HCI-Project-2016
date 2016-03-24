using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_2016_Project.DataClasses
{
    public class ManifestationType
    {
        public String label { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String iconSrc { get; set; }

        public ManifestationType(String label, String name, String description, String iconSrc)
        {
            this.label = label;
            this.name = name;
            this.description = description;
            this.iconSrc = iconSrc;
        }
    }
}
