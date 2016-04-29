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

using HCI_2016_Project.Utils;
using HCI_2016_Project.Binders;
using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for ShowTagsDialog.xaml
    /// </summary>
    public partial class ShowTagsDialog : Window
    {
        public ObservableCollection<Tag> Tags { get; set; }
        public Tag SelectedTag { get; set; }
        public Boolean ButtonEnabled { get; set; }

        public ShowTagsDialog()
        {
            InitializeComponent();

            SelectedTag = null;

            this.DataContext = this;

            Tags = new ObservableCollection<Tag>();
            foreach (Tag t in AppData.GetInstance().Tags)
            {
                Tags.Add(t);
            }
        }

        private void DeleteTag_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show(
                "Da li ste sigurni da želite da obrišete odabrani tag ("
                + SelectedTag.Mark + ")?",
                "Potvrda brisanja", System.Windows.MessageBoxButton.YesNo);

            if (messageBox == MessageBoxResult.Yes)
            {
                Tags.Remove(SelectedTag);

                List<Tag> tags = new List<Tag>();
                foreach (Tag tag in Tags)
                {
                    tags.Add(tag);
                }
                AppData.GetInstance().Tags = tags;
                Serialization.SerializeTags();
            }
        }

        private void EditTag_Click(object sender, RoutedEventArgs e)
        {
            //var tagEditDialog = new HCI_2016_Project.UserInterface.Dialogs.EditTagDialog(SelectedTag);
            //tagEditDialog.Show();
        }

        private void ViewDetailsTag_Click(object sender, RoutedEventArgs e)
        {
            //var showDetailDialog = new HCI_2016_Project.UserInterface.Dialogs.TagDetailsWindow(SelectedTag);
            //showDetailDialog.Show();
        }

        private void TagsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = SelectedTag != null;

            EditTag.IsEnabled = ButtonEnabled;
            DeleteTag.IsEnabled = ButtonEnabled;
            ViewDetailsTag.IsEnabled = ButtonEnabled;
        }
    }
}
