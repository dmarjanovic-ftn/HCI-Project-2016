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
        private ViewModel vm;

        public class ViewModel
        {
            public Manifestation Manifestation   { get; set; }
            public List<ManifestationType> Types { get; set; }
            public List<Tag> AvailableTags       { get; set; }
            public List<CheckBox> AllTags        { get; set; }
        }

        public AddManifestationDialog()
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Types = AppData.GetInstance().ManifestationTypes;
            vm.Manifestation = new Manifestation();
            vm.Manifestation.Date = DateTime.Now;
            vm.AvailableTags = AppData.GetInstance().Tags;
            vm.AllTags = new List<CheckBox>();

            this.DataContext = vm;

            int tagNo = 0;
            foreach (Tag tag in vm.AvailableTags)
            {
                if (tagNo % 6 == 0)
                    ListOfTags.RowDefinitions.Add(new RowDefinition());

                // Define StackPanel to CheckBox
                StackPanel sp = new StackPanel();
                System.Drawing.Color BackColor = System.Drawing.ColorTranslator.FromHtml(tag.Color);
                sp.Background = new SolidColorBrush(Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B));
                sp.MinHeight = 28;
                sp.MaxHeight = 28;
                sp.Margin = new System.Windows.Thickness(5, 2, 5, 2);

                // Define tag which is CheckVox
                CheckBox cb = new CheckBox();
                // cb.Name = tagNo.ToString();
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5); 
                cb.Content = tag.Mark;
                cb.Foreground = Brushes.White;
                vm.AllTags.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, tagNo % 6);
                Grid.SetRow(sp, tagNo / 6);
                ListOfTags.Children.Add(sp);
                ++tagNo;
            }
        }

        // Save Manifestation Button
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (null == vm.Manifestation.IconSrc)
            {
                vm.Manifestation.IconSrc = vm.Manifestation.Type.IconSrc;
            }

            for (int i = 0; i < vm.AllTags.Count; ++i)
            {
                if (vm.AllTags[i].IsChecked == true)
                {
                    vm.Manifestation.Tags.Add(vm.AvailableTags[i]);
                }
            }

            AppData.GetInstance().Manifestations.Add(vm.Manifestation);
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
