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
    /// Interaction logic for ShowManifestationsDialog.xaml
    /// </summary>
    public partial class ShowManifestationsDialog : Window
    {
        public ObservableCollection<Manifestation> Manifestations
        {
            get;
            set;
        }

        public ShowManifestationsDialog()
        {
            InitializeComponent();
            this.DataContext = this;

            Manifestations = new ObservableCollection<Manifestation>();
            Manifestations.Add(new Manifestation { Label = "SR4", AlcoholStatus=AlcoholStatusEnum.CAN_BRING });
            Manifestations.Add(new Manifestation { Label = "IA2", AlcoholStatus=AlcoholStatusEnum.CAN_BUY });
            Manifestations.Add(new Manifestation { Label = "RV2", AlcoholStatus=AlcoholStatusEnum.NO_ALCOHOL });
            Manifestations.Add(new Manifestation { Label = "IC1", AlcoholStatus=AlcoholStatusEnum.NO_ALCOHOL });
            Manifestations.Add(new Manifestation { Label = "CR7", AlcoholStatus=AlcoholStatusEnum.CAN_BUY });
        }
    }
}
