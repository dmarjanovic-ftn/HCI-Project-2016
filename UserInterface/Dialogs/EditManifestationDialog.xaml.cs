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
using HCI_2016_Project.Utils;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for EditManifestationDialog.xaml
    /// </summary>
    public partial class EditManifestationDialog : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public Manifestation Manifestation { get; set; }
            public List<ManifestationType> Types { get; set; }
            public String ManifestationOldLabel { get; set; }
        }

        public EditManifestationDialog(Manifestation manifestation)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Types = AppData.GetInstance().ManifestationTypes;
            vm.Manifestation = manifestation;
            vm.ManifestationOldLabel = manifestation.Label;

            this.DataContext = vm;
        }

        // Save Edited Manifestation
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<Manifestation> manifestations = new List<Manifestation>();
            foreach (Manifestation manifestation in AppData.GetInstance().Manifestations)
            {
                if (manifestation.Label == vm.ManifestationOldLabel)
                {
                    manifestations.Add(vm.Manifestation);
                    break;
                }
                else
                {
                    manifestations.Add(manifestation);
                }
            }
            AppData.GetInstance().Manifestations = manifestations;
            Serialization.SerializeManifestations();
            this.Close();
        }

        // Cancel Edit Mode
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovu manifestaciju. Podaci će ostati nepromijenjeni. Da li ste sigurni?", "Odustani od izmjene", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        // Choose Icon Source file
        private void ChooseIcon_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dialog.DefaultExt = ".png";
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dialog.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dialog.FileName;
                IconPath.Text = filename;
            }
        }
    }
}
