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

using HCI_2016_Project.Binders;
using HCI_2016_Project.Utils;
using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for ShowManifestationsDialog.xaml
    /// </summary>
    public partial class ShowManifestationsDialog : Window
    {
        public ObservableCollection<Manifestation> Manifestations { get; set; }
        public Manifestation SelectedManifestation { get; set; }
        public Boolean ButtonEnabled { get; set; }

        public ShowManifestationsDialog()
        {
            InitializeComponent();

            SelectedManifestation = null;

            this.DataContext = this;

            Manifestations = new ObservableCollection<Manifestation>();
            foreach (Manifestation m in AppData.GetInstance().Manifestations)
            {
                Manifestations.Add(m);
            }

            
        }

        private void ManifestationsTable_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn dgtc = e.Column as DataGridTextColumn;
            if (dgtc != null)
            {
                StringToImageConverter con = new StringToImageConverter();
                (dgtc.Binding as Binding).Converter = con;
            }
        }

        // Delete Manifestation Button
        private void DeleteManifestation_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show(
                "Da li ste sigurni da želite da obrišete odabranu manifestaciju (" 
                + SelectedManifestation.Label + " " + SelectedManifestation.Name + ")?", 
                "Potvrda brisanja", System.Windows.MessageBoxButton.YesNo);

            if (messageBox == MessageBoxResult.Yes)
            {
                Manifestations.Remove(SelectedManifestation);

                List<Manifestation> manifestations = new List<Manifestation>();
                foreach (Manifestation manifestation in Manifestations)
                {
                    manifestations.Add(manifestation);
                }
                AppData.GetInstance().Manifestations = manifestations;
                Serialization.SerializeManifestations();
            }
        }

        // Edit Manifestation Button
        private void EditManifestation_Click(object sender, RoutedEventArgs e)
        {

        }

        // View Details Manifestation Button
        private void ViewDetailsManifestation_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(SelectedManifestation.Name);
            Console.WriteLine(SelectedManifestation.Label);
        }

        private void ManifestationsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = SelectedManifestation != null;

            EditManifestation.IsEnabled = ButtonEnabled;
            DeleteManifestation.IsEnabled = ButtonEnabled;
            ViewDetailsManifestation.IsEnabled = ButtonEnabled;
        }
    }
}
