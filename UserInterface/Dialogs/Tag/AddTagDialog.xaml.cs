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

using HCI_2016_Project.Utils;
using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for AddTagDialog.xaml
    /// </summary>
    public partial class AddTagDialog : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public Tag Tag { get; set; }
        }

        public AddTagDialog()
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Tag = new Tag();
            vm.Tag.Color = "#FFFFFF";

            this.DataContext = vm;
        }

        // Save Tag Button
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            AppData.GetInstance().Tags.Add(vm.Tag);
            Serialization.SerializeTags();
            this.Close();
        }

        // Cancel Tag Dialog
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovu etiketu/tag. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            String color = ClrPcker_Background.SelectedColor.ToString().Remove(1, 2);
            vm.Tag.Color = color;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            Console.WriteLine(focusedControl.GetType());

            if (focusedControl.GetType() == typeof(System.Windows.Controls.Primitives.ToggleButton))
            {
                HelpProvider.ShowHelp("Tag", "color", this);
            }
            else if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                string sec = HelpProvider.GetHelpSection((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, sec, this);
            }
            else
            {
                HelpProvider.ShowHelp("Tag", "#", this);
            }
        }
    }
}
