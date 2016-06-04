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
using HCI_2016_Project.Utils;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for ShowManifestationsTypeDialog.xaml
    /// </summary>
    public partial class ShowManifestationsTypeDialog : Window
    {

        public ObservableCollection<ManifestationType> ManifestationTypes { get; set; }
        public ManifestationType SelectedManifestationType { get; set; }
        public Boolean ButtonEnabled { get; set; }

        public ShowManifestationsTypeDialog()
        {
            InitializeComponent();

            SelectedManifestationType = null;

            this.DataContext = this;

            ManifestationTypes = new ObservableCollection<ManifestationType>();
            foreach (ManifestationType mt in AppData.GetInstance().ManifestationTypes)
            {
                ManifestationTypes.Add(mt);
            }
        }

        /*private void ManifestationsTable_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn dgtc = e.Column as DataGridTextColumn;
            if (dgtc != null)
            {
                StringToImageConverter con = new StringToImageConverter();
                (dgtc.Binding as Binding).Converter = con;
            }
        }*/

        private void ViewDetailsManifestationType_Click(object sender, RoutedEventArgs e)
        {
            var showDetailDialog = new HCI_2016_Project.UserInterface.Dialogs.ManifestationTypeDetailsWindow(SelectedManifestationType);
            showDetailDialog.Show();
        }

        private void EditManifestationType_Click(object sender, RoutedEventArgs e)
        {
            var manifestationTypeEditDialog = new HCI_2016_Project.UserInterface.Dialogs.EditManifestationTypeDialog(SelectedManifestationType);
            manifestationTypeEditDialog.Show();
        }

        private void DeleteManifestationType_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show(
                "Da li ste sigurni da želite da obrišete odabrani tip manifestacije ("
                + SelectedManifestationType.Label + " " + SelectedManifestationType.Name + ")?",
                "Potvrda brisanja", System.Windows.MessageBoxButton.YesNo);

            if (messageBox == MessageBoxResult.Yes)
            {
                ManifestationTypes.Remove(SelectedManifestationType);

                List<ManifestationType> manifestationTypes = new List<ManifestationType>();
                foreach (ManifestationType manifestationType in ManifestationTypes)
                {
                    manifestationTypes.Add(manifestationType);
                }
                AppData.GetInstance().ManifestationTypes = manifestationTypes;
                Serialization.SerializeManifestationTypes();
            }
        }

        private void ManifestationTypesTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = SelectedManifestationType != null;

            EditManifestationType.IsEnabled = ButtonEnabled;
            DeleteManifestationType.IsEnabled = ButtonEnabled;
            ViewDetailsManifestationType.IsEnabled = ButtonEnabled;
        }
    }
}
