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
        public List<Tag> AllTags { get; set; }
        public Tag SelectedTag { get; set; }
        public Boolean ButtonEnabled { get; set; }

        #region Search Options
        public string TagLabel { get; set; }
        public string TagDescription { get; set; }
        #endregion

        public ShowTagsDialog()
        {
            InitializeComponent();

            SelectedTag = null;

            this.DataContext = this;

            Tags = new ObservableCollection<Tag>();
            AllTags = new List<Tag>();
            foreach (Tag t in AppData.GetInstance().Tags)
            {
                Tags.Add(t);
                AllTags.Add(t);
            }
        }

        private void DeleteTag_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show(
                "Da li ste sigurni da želite da obrišete odabrani tag ("
                + SelectedTag.Mark + ")? Biće obrisani tagovi u svim manifestacijama u kojima je dodat!",
                "Potvrda brisanja", System.Windows.MessageBoxButton.YesNo);

            if (messageBox == MessageBoxResult.Yes)
            {
                List<Manifestation> manifestations = new List<Manifestation>();
                List<Tag> tgs;
                foreach (Manifestation m in AppData.GetInstance().Manifestations)
                {
                    tgs = new List<Tag>();
                    foreach (Tag t in m.Tags)
                    {
                        if (t.Mark != SelectedTag.Mark) tgs.Add(t);
                    }
                    m.Tags = tgs;
                    manifestations.Add(m);
                }

                AppData.GetInstance().Manifestations = manifestations;
                Serialization.SerializeManifestations();

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
            var tagEditDialog = new HCI_2016_Project.UserInterface.Dialogs.EditTagDialog(SelectedTag);
            tagEditDialog.Show();
        }

        private void ViewDetailsTag_Click(object sender, RoutedEventArgs e)
        {
            var showDetailDialog = new HCI_2016_Project.UserInterface.Dialogs.TagDetailsWindow(SelectedTag);
            showDetailDialog.Show();
        }

        private void TagsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = SelectedTag != null;

            EditTag.IsEnabled = ButtonEnabled;
            DeleteTag.IsEnabled = ButtonEnabled;
            ViewDetailsTag.IsEnabled = ButtonEnabled;
        }

        // Perform search...
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Tags.Clear();

            foreach (Tag t in AllTags)
            {
                if (IfSearchOkay(t))
                {
                    Tags.Add(t);
                }
            }
        }

        private bool IfSearchOkay(Tag t)
        {
            if (TagLabel != null && TagLabel != "" && !t.Mark.ToLower().Contains(TagLabel.ToLower()))
                return false;

            if (TagDescription != null & TagDescription != "" && !t.Description.ToLower().Contains(TagDescription.ToLower()))
                return false;

            return true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            Console.WriteLine(focusedControl.GetType());

            if (focusedControl.GetType() == typeof(System.Windows.Controls.Primitives.ToggleButton))
            {
                HelpProvider.ShowHelp("SearchTags", "color", this);
            }
            else if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                string sec = HelpProvider.GetHelpSection((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, sec, this);
            }
            else
            {
                HelpProvider.ShowHelp("SearchTags", "#", this);
            }
        }
    }
}
