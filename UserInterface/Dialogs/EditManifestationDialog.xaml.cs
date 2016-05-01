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
            public Manifestation Manifestation   { get; set; }
            public List<ManifestationType> Types { get; set; }
            public String ManifestationOldLabel  { get; set; }
            public List<Tag> AvailableTags       { get; set; }
            public List<CheckBox> AllTags        { get; set; }
        }

        public EditManifestationDialog(Manifestation manifestation)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Types = AppData.GetInstance().ManifestationTypes;
            vm.Manifestation = manifestation;
            vm.AvailableTags = AppData.GetInstance().Tags;
            vm.AllTags = new List<CheckBox>();
            vm.ManifestationOldLabel = manifestation.Label;

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
                foreach (Tag markedTag in vm.Manifestation.Tags)
                {
                    if (markedTag.Mark == tag.Mark)
                    {
                        cb.IsChecked = true;
                        break;
                    }
                }
                vm.AllTags.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, tagNo % 6);
                Grid.SetRow(sp, tagNo / 6);
                ListOfTags.Children.Add(sp);
                ++tagNo;
            }
        }

        // Save Edited Manifestation
        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (null == vm.Manifestation.IconSrc)
            {
                vm.Manifestation.IconSrc = vm.Manifestation.Type.IconSrc;
            }

            vm.Manifestation.Tags.Clear();
            for (int i = 0; i < vm.AllTags.Count; ++i)
            {
                if (vm.AllTags[i].IsChecked == true)
                {
                    vm.Manifestation.Tags.Add(vm.AvailableTags[i]);
                }
            }

            List<Manifestation> manifestations = new List<Manifestation>();
            foreach (Manifestation manifestation in AppData.GetInstance().Manifestations)
            {
                if (manifestation.Label == vm.ManifestationOldLabel)
                {
                    manifestations.Add(vm.Manifestation);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < vm.Types.Count; ++i)
            {
                if (vm.Types[i].Label == vm.Manifestation.Type.Label)
                {
                    ManifestationTypeCb.SelectedItem = ManifestationTypeCb.Items.GetItemAt(i);
                    break;
                }
            }
        }
    }
}
