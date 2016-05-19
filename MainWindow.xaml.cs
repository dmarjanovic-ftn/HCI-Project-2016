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
using System.Collections.ObjectModel;

using HCI_2016_Project.DataClasses;
using HCI_2016_Project.Utils;

namespace HCI_2016_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int ICON_SIZE  = 20;
        private const int OFFSET     = ICON_SIZE / 2;

        private ViewModel vm;

        Point start = new Point();

        public class ViewModel
        {
            public ObservableCollection<Manifestation> Manifestations { get; set; }
            public ObservableCollection<Manifestation> DroppedManifestations { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            // this.DataContext = vm;
        }

        private void MenuItem_Click_0(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.AddManifestationDialog();
            s.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.EditManifestationDialog(new Manifestation());
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
            var s = new HCI_2016_Project.UserInterface.Dialogs.EditTagDialog(new Tag());
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
            var s = new HCI_2016_Project.UserInterface.Dialogs.EditManifestationTypeDialog(new ManifestationType());
            s.Show();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            var s = new HCI_2016_Project.UserInterface.Dialogs.ShowManifestationsTypeDialog();
            s.Show();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Serialization.DeserializeManifestationTypes();
            Serialization.DeserializeTags();
            Serialization.DeserializeManifestations();

            vm = new ViewModel();
            vm.Manifestations = new ObservableCollection<Manifestation>();
            vm.DroppedManifestations = new ObservableCollection<Manifestation>();

            foreach (Manifestation manifestation in AppData.GetInstance().Manifestations)
            {
                vm.Manifestations.Add(manifestation);
            }

            Console.WriteLine(vm.Manifestations.Count);
            this.DataContext = vm;
        }

        // Click on Manifestation on left sidebar
        private void ManifestationItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(null);

            StackPanel test = sender as StackPanel;
            
            Console.WriteLine(test.Tag);
        }

        // Move Manifestation Item
        private void ManifestationItem_MouseMove(object sender, MouseEventArgs e)
        {
            // Console.WriteLine("moving...");

            StackPanel manifestationPanel = sender as StackPanel;
            Manifestation dataObject = null;
            foreach (Manifestation manifestation in vm.Manifestations)
            {
                if ((string) manifestationPanel.Tag == manifestation.Label)
                {
                    dataObject = manifestation;
                    break;
                }
            }

            // Initialize the drag & drop operation
            DataObject data = new DataObject("myFormat", dataObject);
            DragDrop.DoDragDrop(manifestationPanel, data, DragDropEffects.Move);
        }

        // Drag Manifestation Item into Manifestations' Map
        private void ManifestationsMap_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("test");
        }

        // Drop Manifestation Item
        private void ManifestationsMap_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("dropped");

            Point dropPosition = e.GetPosition(ManifestationsMap);

            if (e.Data.GetDataPresent("myFormat"))
            {
                Manifestation manifestation = e.Data.GetData("myFormat") as Manifestation;
                vm.Manifestations.Remove(manifestation);
                Console.WriteLine(manifestation.Name);

                Canvas canvas = this.ManifestationsMap;

                Image ManifestationIcon = new Image
                {
                    Width  = ICON_SIZE,
                    Height = ICON_SIZE,
                    //Name = manifestation.Name,
                    Source = new BitmapImage(new Uri(manifestation.IconSrc, UriKind.Absolute)),
                };
                /*BitmapImage icon = new BitmapImage(new Uri(manifestation.IconSrc, UriKind.Absolute));
                Image ManifestationIcon*/

                /*Ellipse r = new Ellipse();
                r.Height=10;
                r.Width=10;
                r.Stroke =Brushes.Black;*/

                canvas.Children.Add(ManifestationIcon);

                Canvas.SetLeft(ManifestationIcon, dropPosition.X - OFFSET);
                Canvas.SetTop(ManifestationIcon, dropPosition.Y - OFFSET);

                /*root.Children.Add(e);
                Y +=10;*/
                //Studenti2.Add(student);
            }
        }
    }
}
