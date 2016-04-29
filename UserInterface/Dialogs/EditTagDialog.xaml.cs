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
using HCI_2016_Project.Utils;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for EditTagDialog.xaml
    /// </summary>
    public partial class EditTagDialog : Window
    {

        private ViewModel vm;

        public class ViewModel
        {
            public Tag Tag { get; set; }
            public List<String> Colors { get; set; }
            public String TagOldMark { get; set; }
        }

        public EditTagDialog(Tag selectedTag)
        {
            InitializeComponent();

            vm = new ViewModel();
            vm.Colors = new List<String>();
            vm.Colors.Add("#F44336");
            vm.Colors.Add("#3F51B5");
            vm.Colors.Add("#43A047");

            vm.Tag = selectedTag;
            vm.TagOldMark = selectedTag.Mark;

            this.DataContext = vm;
        }

        // Save Edited Tag
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<Tag> tags = new List<Tag>();
            foreach (Tag tag in AppData.GetInstance().Tags)
            {
                if (tag.Mark == vm.TagOldMark)
                {
                    tags.Add(vm.Tag);
                }
                else
                {
                    tags.Add(tag);
                }
            }
            AppData.GetInstance().Tags = tags;
            Serialization.SerializeTags();
            this.Close();
        }

        // Cancel Edit Mode
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovaj tag. Podaci će ostati nepromijenjeni. Da li ste sigurni?", "Odustani od izmjene", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
