using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for ShowTagsDialog.xaml
    /// </summary>
    public partial class ShowTagsDialog : Window
    {
        public ObservableCollection<Tag> Tags
        {
            get;
            set;
        }

        public ShowTagsDialog()
        {
            InitializeComponent();

            this.DataContext = this;
            Tags = new ObservableCollection<Tag>();
            Tags.Add(new Tag { Mark = "SR4", Description = "Description" });
            Tags.Add(new Tag { Mark = "IA2", Description = "Description" });
            Tags.Add(new Tag { Mark = "RV2", Description = "Description" });
            Tags.Add(new Tag { Mark = "IC1", Description = "Description" });
            Tags.Add(new Tag { Mark = "CR7", Description = "Description" });
        }
    }
}
