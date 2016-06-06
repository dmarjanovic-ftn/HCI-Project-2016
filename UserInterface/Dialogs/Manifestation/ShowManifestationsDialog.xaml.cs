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
        public List<Manifestation> AllManifestations { get; set; }
        public Manifestation SelectedManifestation { get; set; }
        public Boolean ButtonEnabled { get; set; }

        #region Search Parameters
        public ManifestationType manifestationType { get; set; }

        public string ManifestationLabel { get; set; }
        public string ManifestationName { get; set; }
        // public string ManifestationDescription;
        public DateTime ManifestationBeg { get; set; }
        public DateTime ManifestationEnd { get; set; }
        public int GuestsExpectedMin { get; set; }
        public int GuestsExpectedMax { get; set; }

        public bool IsAccessible { get; set; }
        public bool IsSmokingAllowed { get; set; }
        public bool IsOutsideManifestation { get; set; }

        // Price Category
        public bool IsFree { get; set; }
        public bool IsLowPrice { get; set; }
        public bool IsMedPrice { get; set; }
        public bool IsHighPrice { get; set; }

        // Alcohol Status
        public bool CanNoAlcohol { get; set; }
        public bool CanCanBring { get; set; }
        public bool CanCanBuy { get; set; }
        #endregion

        public ShowManifestationsDialog()
        {
            InitializeComponent();

            SelectedManifestation = null;

            this.DataContext = this;

            Manifestations = new ObservableCollection<Manifestation>();
            AllManifestations = new List<Manifestation>();
            foreach (Manifestation m in AppData.GetInstance().Manifestations)
            {
                Manifestations.Add(m);
                AllManifestations.Add(m);
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
            var manifestationEditDialog = new HCI_2016_Project.UserInterface.Dialogs.EditManifestationDialog(SelectedManifestation);
            manifestationEditDialog.Show();
        }

        // View Details Manifestation Button
        private void ViewDetailsManifestation_Click(object sender, RoutedEventArgs e)
        {
            var showDetailDialog = new HCI_2016_Project.UserInterface.Dialogs.ManifestationDetailsWindow(SelectedManifestation);
            showDetailDialog.Show();
        }

        private void ManifestationsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = SelectedManifestation != null;

            EditManifestation.IsEnabled = ButtonEnabled;
            DeleteManifestation.IsEnabled = ButtonEnabled;
            ViewDetailsManifestation.IsEnabled = ButtonEnabled;
        }

        // Show Options for Advanced Search
        private void ShowMore_Click(object sender, RoutedEventArgs e)
        {
            AdvancedSearch.Visibility = (AdvancedSearch.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
            if (AdvancedSearch.Visibility == Visibility.Visible)
            {
                ShowMore.Content = "Sakrij opcije";
            }
            else
            {
                ShowMore.Content = "Prikaži više opcija";
            }
        }

        // Choose Manifestation Type
        private void ChooseManifestationType_Click(object sender, RoutedEventArgs e)
        {
            var allManifestaionTypes = new ShowManifestationsTypeDialog(true);
            allManifestaionTypes.Show();
            allManifestaionTypes.OnDataChoose += new ShowManifestationsTypeDialog.ChooseData(GetManifestationType);
        }

        // Get type
        private void GetManifestationType(ManifestationType type)
        {
            this.manifestationType = type;

            Uri imageUri = new Uri(type.IconSrc, UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);

            ManifestationTypeLabel.Text = type.Label;
            ManifestationTypeName.Text = type.Name;
            ManifestationTypeImageSrc.Source = imageBitmap;
        }

        // Try to find ManifestationType by ID
        private void ManifestationTypeLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (ManifestationType type in AppData.GetInstance().ManifestationTypes)
            {
                if (type.Label.ToLower() == ManifestationTypeLabel.Text.ToLower())
                {
                    this.manifestationType = type;

                    Uri imageUri = new Uri(type.IconSrc, UriKind.Absolute);
                    BitmapImage imageBitmap = new BitmapImage(imageUri);

                    ManifestationTypeName.Text = type.Name;
                    ManifestationTypeImageSrc.Source = imageBitmap;
                    return;
                }
            }

            ManifestationTypeName.Text = null;
            ManifestationTypeImageSrc.Source = null;
            manifestationType = null;
        }

        // Perform search...
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Manifestations.Clear();

            foreach (Manifestation m in AllManifestations)
            {
                if (IfSearchOkay(m))
                {
                    Manifestations.Add(m);
                }
            }
        }

        private bool IfSearchOkay(Manifestation m) {

            if (ManifestationLabel != null && ManifestationLabel != "" && !m.Label.ToLower().Contains(ManifestationLabel.ToLower()))
                return false;
            
            if (ManifestationName != null & ManifestationName != "" && !m.Name.ToLower().Contains(ManifestationName.ToLower()))
                return false;

            if (GuestsExpectedMin > 0 && GuestsExpectedMin > m.GuestsExpected)
                return false;

            if (GuestsExpectedMax > 0 && GuestsExpectedMax < m.GuestsExpected)
                return false;

            if (manifestationType != null && manifestationType.Label != m.Type.Label)
                return false;

            /*if (ManifestationBeg.CompareTo(m.Date) > 0)
                return false;

            if (ManifestationEnd.CompareTo(m.Date) < 0)
                return false;*/

            if (IsAccessible && !m.Accessible)
                return false;

            if (IsSmokingAllowed && !m.SmokingAllowed)
                return false;

            if (IsOutsideManifestation && !m.OutsideManifestation)
                return false;

            if (!((CanNoAlcohol && m.AlcoholStatus == AlcoholStatusEnum.NO_ALCOHOL)
                || (CanCanBring && m.AlcoholStatus == AlcoholStatusEnum.CAN_BRING)
                || (CanCanBuy && m.AlcoholStatus == AlcoholStatusEnum.CAN_BUY))
                && (CanNoAlcohol || CanCanBring || CanCanBuy))
                return false;

            if (!((IsFree && m.PriceCategory == PriceCategoryEnum.FREE)
                || (IsLowPrice && m.PriceCategory == PriceCategoryEnum.LOW_PRICE)
                || (IsMedPrice && m.PriceCategory == PriceCategoryEnum.MEDIUM_PRICE)
                || (IsHighPrice && m.PriceCategory == PriceCategoryEnum.HIGH_PRICE))
                && (IsFree || IsLowPrice || IsMedPrice || IsHighPrice))
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
                HelpProvider.ShowHelp("SearchManifestations", "#", this);
            }
        }


    }
}
