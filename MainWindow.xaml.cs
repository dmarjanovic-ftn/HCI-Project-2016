﻿using HCI_2016_Project.UserInterface.Dialogs;
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
        private const int ICON_SIZE = 32;
        private const int OFFSET    = ICON_SIZE / 2;

        private const string FROM_SIDEBAR = "ManifestationDraggedFromSidebar";
        private const string FROM_CANVAS  = "ManifestationDraggedFromCanvas";

        private ViewModel vm;

        Point start = new Point();

        public class ViewModel
        {
            public ObservableCollection<Manifestation> Manifestations { get; set; }
            public ObservableCollection<Manifestation> DroppedManifestations { get; set; }
            public Manifestation ClickedManifestation { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
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
                if (manifestation.X == -1 && manifestation.Y == -1)
                {
                    vm.Manifestations.Add(manifestation);
                }
                else
                {
                    Canvas canvas = ManifestationsMap;

                    Image ManifestationIcon = new Image
                    {
                        Width = ICON_SIZE,
                        Height = ICON_SIZE,
                        Uid = manifestation.Label,
                        Source = new BitmapImage(new Uri(manifestation.IconSrc, UriKind.Absolute)),
                    };

                    ManifestationIcon.ToolTip = GetTooltip(manifestation);

                    canvas.Children.Add(ManifestationIcon);

                    Canvas.SetLeft(ManifestationIcon, manifestation.X);
                    Canvas.SetTop(ManifestationIcon, manifestation.Y);

                    vm.DroppedManifestations.Add(manifestation);
                }
            }

            this.DataContext = vm;

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                MenuItem_Click_1(null, null);
            }
            else if (e.Key == Key.F1)
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                HelpProvider.ShowHelp("Index", "#", this);
            }
        }

        // Return clicked Manifestation on canvas. Otherwise return null.
        private Manifestation ClickedManifestation(int x, int y)
        {
            foreach (Manifestation manifestation in vm.DroppedManifestations)
            {
                if (Math.Sqrt(Math.Pow((x - manifestation.X - OFFSET), 2) + 
                    Math.Pow((y - manifestation.Y - OFFSET), 2)) < 1.5*OFFSET)
                {
                    return manifestation;
                }
            }

            return null;
        }

        // Click on Manifestation on left sidebar
        private void ManifestationItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(null);

            StackPanel manifestationPanel = sender as StackPanel;
            Manifestation dataObject = null;

            foreach (Manifestation manifestation in vm.Manifestations)
            {
                if ((string)manifestationPanel.Tag == manifestation.Label)
                {
                    dataObject = manifestation;
                    break;
                }
            }

            // Initialize the drag & drop operation
            DataObject data = new DataObject(FROM_SIDEBAR, dataObject);
            DragDrop.DoDragDrop(manifestationPanel, data, DragDropEffects.Move);
        }

        // Move Manifestation Item
        private void ManifestationItem_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        // Drag Manifestation Item into Manifestations' Map
        private void ManifestationsMap_DragEnter(object sender, DragEventArgs e)
        {

        }

        // Drop Manifestation Item
        private void ManifestationsMap_Drop(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(ManifestationsMap);

            // There's another manifestation on this position
            if (ClickedManifestation((int)dropPosition.X, (int)dropPosition.Y) != null)
            {
                return;
            }

            if (e.Data.GetDataPresent(FROM_SIDEBAR))
            {
                Manifestation manifestation = e.Data.GetData(FROM_SIDEBAR) as Manifestation;

                vm.Manifestations.Remove(manifestation);
                manifestation.X = (int) dropPosition.X - OFFSET;
                manifestation.Y = (int) dropPosition.Y - OFFSET;

                AppData.ChangeDroppedManifestation(manifestation);

                vm.DroppedManifestations.Add(manifestation);

                Canvas canvas = this.ManifestationsMap;

                Image ManifestationIcon = new Image
                {
                    Width  = ICON_SIZE,
                    Height = ICON_SIZE,
                    Uid    = manifestation.Label,
                    Source = new BitmapImage(new Uri(manifestation.IconSrc, UriKind.Absolute)),
                };

                ManifestationIcon.ToolTip = GetTooltip(manifestation);

                canvas.Children.Add(ManifestationIcon);

                Canvas.SetLeft(ManifestationIcon, manifestation.X);
                Canvas.SetTop(ManifestationIcon, manifestation.Y);

                return;
            }
            
            if (e.Data.GetDataPresent(FROM_CANVAS))
            {
                Manifestation manifestation = e.Data.GetData(FROM_CANVAS) as Manifestation;

                vm.DroppedManifestations.Remove(manifestation);
                manifestation.X = (int)dropPosition.X - OFFSET;
                manifestation.Y = (int)dropPosition.Y - OFFSET;

                AppData.ChangeDroppedManifestation(manifestation);
                vm.DroppedManifestations.Add(manifestation);

                Canvas canvas = this.ManifestationsMap;

                UIElement remove = null;
                foreach (UIElement elem in canvas.Children) {
                    if (elem.Uid == manifestation.Label)
                    {
                        remove = elem;
                        break;
                    }
                }
                canvas.Children.Remove(remove);

                Image ManifestationIcon = new Image
                {
                    Width  = ICON_SIZE,
                    Height = ICON_SIZE,
                    Uid    = manifestation.Label,
                    Source = new BitmapImage(new Uri(manifestation.IconSrc, UriKind.Absolute)),
                };

                ManifestationIcon.ToolTip = GetTooltip(manifestation);

                canvas.Children.Add(ManifestationIcon);

                Canvas.SetLeft(ManifestationIcon, manifestation.X);
                Canvas.SetTop(ManifestationIcon, manifestation.Y);

                return;
            }
        }

        // Click on Canvas of Manfestations' Map
        private void ManifestationsMap_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(null);

            Canvas map = sender as Canvas;
            Manifestation dataObject = null;
            Point mousePosition = e.GetPosition(ManifestationsMap);

            dataObject = ClickedManifestation((int) mousePosition.X, (int) mousePosition.Y);

            // Initialize the drag & drop operation
            if (dataObject != null)
            {
                DataObject data = new DataObject(FROM_CANVAS, dataObject);
                DragDrop.DoDragDrop(map, data, DragDropEffects.Move);
            }
        }

        // Drag Manifestation on ManifestationsMap
        private void ManifestationsMap_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void ManifestationsMap_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(ManifestationsMap);
            vm.ClickedManifestation = ClickedManifestation((int)mousePosition.X, (int)mousePosition.Y);
            ContextMenu cm = ManifestationContextMenu;

            if (vm.ClickedManifestation != null)
            {
                cm.PlacementTarget = sender as Canvas;
                cm.IsOpen = true;
            }
        }

        // Click on Remove in ContextMenu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            vm.DroppedManifestations.Remove(vm.ClickedManifestation);

            vm.ClickedManifestation.X = -1;
            vm.ClickedManifestation.Y = -1;

            vm.Manifestations.Add(vm.ClickedManifestation);

            AppData.ChangeDroppedManifestation(vm.ClickedManifestation);

            Canvas canvas = ManifestationsMap;
            UIElement remove = null;
            foreach (UIElement elem in canvas.Children)
            {
                if (elem.Uid == vm.ClickedManifestation.Label)
                {
                    remove = elem;
                    break;
                }
            }
            canvas.Children.Remove(remove);
        }

        // Click on Demo Mode
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var AddManifestationDemoDialog = new AddManifestationDialog();
            AddManifestationDemoDialog.Show();
            AddManifestationDemoDialog.StartDemo();
        }

        private StackPanel GetTooltip(Manifestation manifestation)
        {
            StackPanel complex = new StackPanel();
            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");
            complex.Background = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            complex.Orientation = Orientation.Vertical;
            complex.MaxWidth = 200;

            TextBlock category = new TextBlock();
            category.Text = manifestation.Type.Name;
            System.Drawing.Color cc = System.Drawing.ColorTranslator.FromHtml("#666666");
            category.Foreground = new SolidColorBrush(Color.FromArgb(cc.A, cc.R, cc.G, cc.B));
            category.FontSize = 12;
            category.Padding = new Thickness(4, 2, 4, 2);
            TextBlock title = new TextBlock();
            title.Text = manifestation.Name;
            title.FontSize = 14;
            title.FontWeight = FontWeights.Bold;
            title.Padding = new Thickness(4, 2, 4, 2);

            complex.Children.Add(category);
            complex.Children.Add(title);

            WrapPanel panel = new WrapPanel();
            panel.Margin = new Thickness(4, 2, 4, 2);
            foreach (Tag tag in manifestation.Tags)
            {
                Label lbl = new Label();
                lbl.Content = tag.Mark;
                System.Drawing.Color BackColor = System.Drawing.ColorTranslator.FromHtml(tag.Color);
                lbl.Background = new SolidColorBrush(Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B));
                lbl.Foreground = Brushes.White;
                lbl.Padding = new Thickness(4, 2, 4, 2);
                lbl.Margin = new Thickness(1, 1, 1, 1);
                panel.Children.Add(lbl);
            }

            complex.Children.Add(panel);

            return complex;
        }

        private void GetNotification(String s)
        {
            if (s == "CHANGED")
            {
                vm.Manifestations.Clear();
                vm.DroppedManifestations.Clear();

                Canvas canvas = ManifestationsMap;

                canvas.Children.Clear();

                foreach (Manifestation manifestation in AppData.GetInstance().Manifestations)
                {
                    if (manifestation.X == -1 && manifestation.Y == -1)
                    {
                        vm.Manifestations.Add(manifestation);
                    }
                    else
                    {
                        Image ManifestationIcon = new Image
                        {
                            Width = ICON_SIZE,
                            Height = ICON_SIZE,
                            Uid = manifestation.Label,
                            Source = new BitmapImage(new Uri(manifestation.IconSrc, UriKind.Absolute)),
                        };

                        ManifestationIcon.ToolTip = GetTooltip(manifestation);

                        canvas.Children.Add(ManifestationIcon);

                        Canvas.SetLeft(ManifestationIcon, manifestation.X);
                        Canvas.SetTop(ManifestationIcon, manifestation.Y);

                        vm.DroppedManifestations.Add(manifestation);
                    }
                }
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            Serialization.OnManifestationsChange += new Serialization.SendMessageToMainWindow(GetNotification);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("Index", "#", this);
        }
    }
}
