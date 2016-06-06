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

        public delegate void ChooseData(ManifestationType type);
        public event ChooseData OnDataChoose;

        private void OnSendData()
        {
            if (OnDataChoose != null)
                OnDataChoose(SelectedManifestationType);
        }

        public ManifestationType DeleteType { get; set; }

        public ObservableCollection<ManifestationType> ManifestationTypes { get; set; }
        public List<ManifestationType> AllManifestationTypes { get; set; }
        public ManifestationType SelectedManifestationType { get; set; }
        public Boolean ButtonEnabled { get; set; }

        public Boolean ReturnType { get; set; }

        #region Search Options
        public string ManifestationTypeLabel { get; set; }
        public string ManifestationTypeName { get; set; }
        public string ManifestationTypeDescription { get; set; }
        #endregion

        public ShowManifestationsTypeDialog(bool find = false)
        {
            InitializeComponent();

            SelectedManifestationType = null;
            ReturnType = find;

            this.DataContext = this;

            ManifestationTypes = new ObservableCollection<ManifestationType>();
            AllManifestationTypes = new List<ManifestationType>();
            foreach (ManifestationType mt in AppData.GetInstance().ManifestationTypes)
            {
                ManifestationTypes.Add(mt);
                AllManifestationTypes.Add(mt);
            }

            if (ReturnType)
            {
                ChooseType.Visibility = Visibility.Visible;
            }
        }

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
            List<Manifestation> mans = new List<Manifestation>();
            foreach (Manifestation m in AppData.GetInstance().Manifestations)
            {
                if (m.Type.Label == SelectedManifestationType.Label)
                {
                    mans.Add(m);
                }
            }

            AreYouSureDialog dialog = new AreYouSureDialog(mans);
            dialog.Show();
            dialog.OnAreYouSureDataChoose += new AreYouSureDialog.ChooseAreYouSureData(GetAnswerManifestationType);

            Console.WriteLine(DeleteType);
        }

        // Get type
        private void GetAnswerManifestationType(ManifestationType type)
        {
            DeleteType = type;
            Console.WriteLine("On answer.");
            // Null znaci da smo odustali od brisanja
            if (DeleteType != null)
            {
                // Znaci da brisemo i manifestacije
                if (DeleteType.Label == SelectedManifestationType.Label)
                {
                    List<Manifestation> NewManifestations = new List<Manifestation>();
                    foreach (Manifestation m in AppData.GetInstance().Manifestations)
                    {
                        if (m.Type.Label != SelectedManifestationType.Label)
                        {
                            NewManifestations.Add(m);
                        }
                    }

                    AppData.GetInstance().Manifestations = NewManifestations;
                    Serialization.SerializeManifestations();
                }
                // Prevezujemo manifestacije
                else
                {
                    List<Manifestation> NewManifestations = new List<Manifestation>();
                    foreach (Manifestation m in AppData.GetInstance().Manifestations)
                    {
                        if (m.Type.Label == SelectedManifestationType.Label)
                        {
                            m.Type = DeleteType;
                            NewManifestations.Add(m);
                        }
                        else
                        {
                            NewManifestations.Add(m);
                        }
                    }

                    AppData.GetInstance().Manifestations = NewManifestations;
                    Serialization.SerializeManifestations();
                }

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

        // Return Selected Type
        private void ChooseType_Click(object sender, RoutedEventArgs e)
        {
            OnSendData();
            this.Close();
        }

        // Perform Search
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ManifestationTypes.Clear();

            foreach (ManifestationType mt in AllManifestationTypes)
            {
                if (IfSearchOkay(mt))
                {
                    ManifestationTypes.Add(mt);
                }
            }
        }

        private bool IfSearchOkay(ManifestationType mt)
        {

            if (ManifestationTypeLabel != null && ManifestationTypeLabel != "" && !mt.Label.ToLower().Contains(ManifestationTypeLabel.ToLower()))
                return false;

            if (ManifestationTypeName != null & ManifestationTypeName != "" && !mt.Name.ToLower().Contains(ManifestationTypeName.ToLower()))
                return false;

            if (ManifestationTypeDescription != null & ManifestationTypeDescription != "" && !mt.Description.ToLower().Contains(ManifestationTypeDescription.ToLower()))
                return false;

            return true;
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
                HelpProvider.ShowHelp("SearchManifestationTypes", "#", this);
            }
        }
    }
}
