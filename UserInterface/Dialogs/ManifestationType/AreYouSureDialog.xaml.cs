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
    /// Interaction logic for AreYouSureDialog.xaml
    /// </summary>
    public partial class AreYouSureDialog : Window
    {
        public List<Manifestation> ManifestationsForDelete { get; set; }
        public ManifestationType result { get; set; }

        public AreYouSureDialog(List<Manifestation> manifestations)
        {
            InitializeComponent();

            this.ManifestationsForDelete = manifestations;
            this.DataContext = this;
        }

        // Choose Another Manifestation Type
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ManifestationsForDelete != null && ManifestationsForDelete.Count > 0)
            {
                var choose = new ChooseNewManifestationType(ManifestationsForDelete[0].Type);
                choose.Show();
                choose.OnNewTypeDataChoose += new ChooseNewManifestationType.NewTypeChooseData(GetAreYouSureManifestationType);
            }
        }

        public delegate void ChooseAreYouSureData(ManifestationType type);
        public event ChooseAreYouSureData OnAreYouSureDataChoose;

        private void OnAreYouSureSendData()
        {
            if (OnAreYouSureDataChoose != null)
                OnAreYouSureDataChoose(this.result);
        }

        // Get type
        private void GetAreYouSureManifestationType(ManifestationType type)
        {
            this.result = type;
            Console.WriteLine(type.Label);

            Uri imageUri = new Uri(type.IconSrc, UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            OnAreYouSureSendData();

            this.Close();
        }

        // Delete anyway
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ManifestationsForDelete != null && ManifestationsForDelete.Count > 0)
            {
                this.result = ManifestationsForDelete[0].Type;
            }
            else
            {
                this.result = null;
            }

            Console.WriteLine("AAAAAA");
            Console.WriteLine(this.result.Label);
            OnAreYouSureSendData();

            this.Close();
        }

        // Cancel
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.result = null;
            OnAreYouSureSendData();

            this.Close();
        }
    }
}
