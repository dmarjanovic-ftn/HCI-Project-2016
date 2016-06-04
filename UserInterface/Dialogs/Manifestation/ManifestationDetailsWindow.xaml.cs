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

using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for ManifestationDetailsWindow.xaml
    /// </summary>
    public partial class ManifestationDetailsWindow : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public Manifestation Manifestation { get; set;}
        }

        public ManifestationDetailsWindow(Manifestation manifestation)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Manifestation = manifestation;

            this.DataContext = vm;
        }
    }
}
