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
    /// Interaction logic for AddManifestationDialog.xaml
    /// </summary>
    /// 

    public partial class AddManifestationDialog : Window
    {
        private Manifestation manifestation;

        public AddManifestationDialog()
        {
            InitializeComponent();

            manifestation = new Manifestation();
            this.DataContext = manifestation;

            Loaded += delegate
            {
                Tokenizer.Focus();
            };

            Tokenizer.TokenMatcher = text =>
            {
                if (text.EndsWith(" "))
                {
                    return text.Substring(0, text.Length - 1).Trim().ToLower();
                }

                return null;
            };
        }

        // Save Manifestation Button
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            AppData.GetInstance().Manifestations.Add(manifestation);
            Serialization.SerializeManifestations();
            this.Close();
        }

        // Cancel Manifestation Button
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovu manifestaciju. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        // Button for Manifestation Icon Selection
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
