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
            var choose = new ChooseNewManifestationType();
            choose.Show();
            choose.OnDataChoose += new ChooseNewManifestationType.ChooseData(GetManifestationType);
        }

        public delegate void ChooseData(ManifestationType type);
        public event ChooseData OnDataChoose;

        private void OnSendData()
        {
            if (OnDataChoose != null)
                OnDataChoose(result);
        }

        // Get type
        private void GetManifestationType(ManifestationType type)
        {
            this.result = type;

            Uri imageUri = new Uri(type.IconSrc, UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);

            OnSendData();
        }

        // Delete anyway
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ManifestationsForDelete.Count > 0)
            {
                this.result = ManifestationsForDelete[0].Type;
            }
            else
            {
                this.result = null;
            }

            OnSendData();
        }

        // Cancel
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.result = null;
            OnSendData();
        }
    }
}
