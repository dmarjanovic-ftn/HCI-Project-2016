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
    /// Interaction logic for ChooseNewManifestationType.xaml
    /// </summary>
    public partial class ChooseNewManifestationType : Window
    {

        public ManifestationType type { get; set; }
        private ManifestationType cantBeSelected;

        public ChooseNewManifestationType(ManifestationType t)
        {
            InitializeComponent();
            this.DataContext = this;
            this.cantBeSelected = t;
        }

        private void ManifestationTypeLabelField_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (ManifestationType type in AppData.GetInstance().ManifestationTypes)
            {
                if (type.Label.ToLower() == ManifestationTypeLabelField.Text.ToLower() && type.Label != cantBeSelected.Label)
                {
                    this.type = type;

                    Uri imageUri = new Uri(type.IconSrc, UriKind.Absolute);
                    BitmapImage imageBitmap = new BitmapImage(imageUri);
                    ManifestationTypeName.Text = type.Name;
                    ManifestationTypeImageSrc.Source = imageBitmap;
                    return;
                }
            }

            ManifestationTypeName.Text = null;
            ManifestationTypeImageSrc.Source = null;
            this.type = null;
        }

        private void ChooseManifestationType_Click(object sender, RoutedEventArgs e)
        {
            var allManifestaionTypes = new ShowManifestationsTypeDialog(true);
            allManifestaionTypes.Show();
            allManifestaionTypes.OnDataChoose += new ShowManifestationsTypeDialog.ChooseData(GetMyManifestationType);
        }

        // Get type
        private void GetMyManifestationType(ManifestationType type)
        {

            if (type.Label == cantBeSelected.Label)
            {
                return;
            }

            this.type = type;

            Uri imageUri = new Uri(type.IconSrc, UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);

            ManifestationTypeLabelField.Text = type.Label;
            ManifestationTypeName.Text = type.Name;
            ManifestationTypeImageSrc.Source = imageBitmap;
        }

        private void ReturnType_Click(object sender, RoutedEventArgs e)
        {
            OnNewTypeSendData();
            this.Close();
        }

        public delegate void NewTypeChooseData(ManifestationType type);
        public event NewTypeChooseData OnNewTypeDataChoose;

        private void OnNewTypeSendData()
        {
            Console.WriteLine("Saljem...");
            Console.WriteLine(this.type.Label);
            if (OnNewTypeDataChoose != null)
                OnNewTypeDataChoose(this.type);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
