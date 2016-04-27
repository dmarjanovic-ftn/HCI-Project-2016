using HCI_2016_Project.UserInterface.Dialogs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using HCI_2016_Project.DataClasses;
using HCI_2016_Project.Utils;

namespace HCI_2016_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region Deserialization of Data
            Serialization.DeserializeManifestationTypes();
            Serialization.DeserializeTags();
            Serialization.DeserializeManifestations();
            #endregion

        }

        private void MenuItem_Click_0(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.AddManifestationDialog();
            s.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.EditManifestationDialog();
            s.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.ShowManifestationsDialog();
            s.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.AddTagDialog();
            s.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.EditTagDialog();
            s.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.ShowTagsDialog();
            s.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.AddManifestationTypeDialog();
            s.Show();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.EditManifestationTypeDialog();
            s.Show();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {

        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }
    }
}
