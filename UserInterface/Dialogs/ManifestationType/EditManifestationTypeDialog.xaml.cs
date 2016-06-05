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

using HCI_2016_Project.Utils;
using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for EditManifestationType.xaml
    /// </summary>
    public partial class EditManifestationTypeDialog : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public ManifestationType ManifestationType { get; set; }
            public String ManifestationTypeOldLabel { get; set; }
        }

        public EditManifestationTypeDialog(ManifestationType manifestationType)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.ManifestationType = manifestationType;
            vm.ManifestationTypeOldLabel = manifestationType.Label;

            this.DataContext = vm;
        }

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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<ManifestationType> manifestationTypes = new List<ManifestationType>();
            foreach (ManifestationType manifestationType in AppData.GetInstance().ManifestationTypes)
            {
                if (manifestationType.Label == vm.ManifestationTypeOldLabel)
                {
                    manifestationTypes.Add(vm.ManifestationType);
                    break;
                }
                else
                {
                    manifestationTypes.Add(manifestationType);
                }
            }
            AppData.GetInstance().ManifestationTypes = manifestationTypes;
            Serialization.SerializeManifestationTypes();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovaj tip manifestacije. Podaci će ostati nepromijenjeni. Da li ste sigurni?", "Odustani od izmjene", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            Console.WriteLine(focusedControl);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                string sec = HelpProvider.GetHelpSection((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, sec, this);
            }
            else
            {
                HelpProvider.ShowHelp("ManifestationTypeEdit", "#", this);
            }
        }
    }
}
