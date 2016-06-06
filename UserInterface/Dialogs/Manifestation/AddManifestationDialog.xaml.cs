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
using System.Threading;
using System.IO;

using HCI_2016_Project.DataClasses;
using HCI_2016_Project.Utils;

namespace HCI_2016_Project.UserInterface.Dialogs
{
    /// <summary>
    /// Interaction logic for AddManifestationDialog.xaml
    /// </summary>
    /// 

    public partial class AddManifestationDialog : Window
    {
        private ViewModel vm;
        private Thread thread;
        private int IsRunning;

        public class ViewModel
        {
            public Manifestation Manifestation   { get; set; }
            public List<ManifestationType> Types { get; set; }
            public List<Tag> AvailableTags       { get; set; }
            public List<CheckBox> AllTags        { get; set; }
            public Boolean IsDemoMode            { get; set; }
        }

        private static int SLEEP_TIME = 150;

        #region DemoModeValues
        private List<String> fields =
            new List<String>()
            {
                "MANLABEL_DEMO",
                "Manifestation Name Which is Looooong...+++++++++",
                "Manifestation demo description. Write more details about manifestation.",
                "SPORTMANIF++",
                "-4++456.56+++"
            };

        private List<TextBox> boxes = new List<TextBox>();

        #endregion

        public AddManifestationDialog()
        {
            InitializeComponent();

            vm = new ViewModel();
            vm.IsDemoMode = false;

            vm.Types = AppData.GetInstance().ManifestationTypes;
            vm.Manifestation = new Manifestation();
            vm.Manifestation.Date = DateTime.Now;
            vm.AvailableTags = AppData.GetInstance().Tags;
            vm.AllTags = new List<CheckBox>();

            this.DataContext = vm;

            int tagNo = 0;
            foreach (Tag tag in vm.AvailableTags)
            {
                if (tagNo % 6 == 0)
                    ListOfTags.RowDefinitions.Add(new RowDefinition());

                // Define StackPanel to CheckBox
                StackPanel sp = new StackPanel();
                System.Drawing.Color BackColor = System.Drawing.ColorTranslator.FromHtml(tag.Color);
                sp.Background = new SolidColorBrush(Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B));
                sp.MinHeight = 28;
                sp.MaxHeight = 28;
                sp.Margin = new System.Windows.Thickness(5, 2, 5, 2);

                // Define tag which is CheckVox
                CheckBox cb = new CheckBox();
                // cb.Name = tagNo.ToString();
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5); 
                cb.Content = tag.Mark;
                cb.Foreground = Brushes.White;
                vm.AllTags.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, tagNo % 6);
                Grid.SetRow(sp, tagNo / 6);
                ListOfTags.Children.Add(sp);
                ++tagNo;
            }

            IsRunning = 1;
        }

        // Save Manifestation Button
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (null == vm.Manifestation.IconSrc)
            {
                vm.Manifestation.IconSrc = vm.Manifestation.Type.IconSrc;
            }

            for (int i = 0; i < vm.AllTags.Count; ++i)
            {
                if (vm.AllTags[i].IsChecked == true)
                {
                    vm.Manifestation.Tags.Add(vm.AvailableTags[i]);
                }
            }

            AppData.GetInstance().Manifestations.Add(vm.Manifestation);
            Serialization.SerializeManifestations();
            this.Close();
        }

        // Cancel Manifestation Button
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovu manifestaciju. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        // Button for Manifestation Icon Selection
        private void ChooseIcon_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dialog.DefaultExt = ".png";
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dialog.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dialog.FileName;
                IconPath.Text = filename;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);

            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                string sec = HelpProvider.GetHelpSection((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, sec, this);
            }
            else
            {
                HelpProvider.ShowHelp("Manifestation", "#", this);
            }
        }

        // Try to Find ManifestationType on TextChange
        private void ManifestationTypeLabelField_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (ManifestationType type in AppData.GetInstance().ManifestationTypes)
            {
                if (type.Label.ToLower() == ManifestationTypeLabelField.Text.ToLower())
                {
                    vm.Manifestation.Type = type;

                    Uri imageUri = new Uri(type.IconSrc, UriKind.Absolute);
                    BitmapImage imageBitmap = new BitmapImage(imageUri);
                    ManifestationTypeError.Visibility = Visibility.Hidden;
                    ManifestationTypeName.Text = type.Name;
                    ManifestationTypeImageSrc.Source = imageBitmap;
                    return;
                }
            }

            ManifestationTypeName.Text = "";
            ManifestationTypeImageSrc.Source = null;
            ManifestationTypeError.Visibility = Visibility.Visible;
            vm.Manifestation.Type = null;
        }


        private void ChooseManifestationType_Click(object sender, RoutedEventArgs e)
        {
            var allManifestaionTypes = new ShowManifestationsTypeDialog(true);
            allManifestaionTypes.Show();
            allManifestaionTypes.OnDataChoose += new ShowManifestationsTypeDialog.ChooseData(GetManifestationType);
        }

        // Get type
        private void GetManifestationType(ManifestationType type)
        {
            vm.Manifestation.Type = type;

            Uri imageUri = new Uri(type.IconSrc, UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);

            ManifestationTypeLabelField.Text = type.Label;
            ManifestationTypeName.Text = type.Name;
            ManifestationTypeImageSrc.Source = imageBitmap;
        }

        public void StartDemo() {

            vm.IsDemoMode = true;

            boxes.Add(ManifestationLabel);
            boxes.Add(ManifestationName);
            boxes.Add(ManifestationDescription);
            boxes.Add(ManifestationTypeLabelField);
            boxes.Add(ManifestationGuestsExpected);

            List<RadioButton> radiobuttons = new List<RadioButton>();
            List<CheckBox> checkboxes = new List<CheckBox>();

            radiobuttons.Add(rbCanBringAlc);
            radiobuttons.Add(rbNoAlc);
            radiobuttons.Add(rb2);
            radiobuttons.Add(rb1);
            radiobuttons.Add(rb4);

            checkboxes.Add(Acc);
            checkboxes.Add(Out);
            checkboxes.Add(Out);
            checkboxes.Add(Out);

            thread = new Thread(() => TypingThread(boxes, fields, radiobuttons, checkboxes, vm.AllTags));
            thread.Start();
        }

        public delegate void UpdateTextCallback(TextBox textbox, string message);
        public delegate void RadioButtonChecker(RadioButton rb);
        public delegate void CheckBoxChecker(CheckBox cb);

        private void TypingThread(List<TextBox> boxes, List<String> messages, 
            List<RadioButton> radiobuttons, List<CheckBox> checkboxes, List<CheckBox> tags)
        {
            while (true)
            {
                for (int i = 0; i < boxes.Count; ++i)
                {
                    String message = messages[i];
                    TextBox textBox = boxes[i];

                    foreach (char s in message)
                    {
                        Thread.Sleep(SLEEP_TIME);

                        if (s == 'D' || s == '+')
                        {
                            Thread.Sleep(SLEEP_TIME);
                            textBox.Dispatcher.Invoke(
                                new UpdateTextCallback(this.RemoveLastChar),
                                new object[] { textBox, "" }
                                );
                        }

                        if (s != '+')
                        {
                            textBox.Dispatcher.Invoke(
                                new UpdateTextCallback(this.UpdateText),
                                new object[] { textBox, s.ToString() }
                                );
                        }
                    }
                }

                foreach (RadioButton rb in radiobuttons)
                {
                    Thread.Sleep(9 * SLEEP_TIME);
                    rb.Dispatcher.Invoke(
                        new RadioButtonChecker(this.CheckRadioButton),
                        new object[] { rb }
                        );
                }

                foreach (CheckBox cb in checkboxes)
                {
                    Thread.Sleep(9 * SLEEP_TIME);
                    cb.Dispatcher.Invoke(
                        new CheckBoxChecker(this.CheckCheckBox),
                        new object[] { cb }
                        );
                }

                for (int i = 0; i < tags.Count; ++i)
                {
                    if (i % 3 == 1)
                    {
                        Thread.Sleep(9 * SLEEP_TIME);
                        tags[i].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { tags[i] }
                            );
                    }
                }
                Thread.Sleep(18 * SLEEP_TIME);

                foreach (TextBox t in boxes)
                {
                    t.Dispatcher.Invoke(
                        new UpdateTextCallback(this.ClearTextBox),
                               new object[] { t, "" }
                               );
                }

                for (int i = 0; i < tags.Count; ++i)
                {
                    if (i % 3 == 1)
                    {
                        tags[i].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { tags[i] }
                            );
                    }
                }

                checkboxes[0].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { checkboxes[0] }
                            );

                checkboxes[1].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { checkboxes[1] }
                            );

                radiobuttons[3].Dispatcher.Invoke(
                        new RadioButtonChecker(this.CheckRadioButton),
                        new object[] { radiobuttons[3] }
                        );
            }
        }

        private void UpdateText(TextBox textbox, string message)
        {
            textbox.AppendText(message);
        }

        private void ClearTextBox(TextBox textbox, string message)
        {
            textbox.Text = "";
        }

        private void RemoveLastChar(TextBox textbox, string message)
        {
            textbox.Text = textbox.Text.Substring(0, textbox.Text.Count() - 1);
        }

        private void CheckRadioButton(RadioButton btn)
        {
            btn.IsChecked = true;
        }

        private void CheckCheckBox(CheckBox cb)
        {
            cb.IsChecked = !cb.IsChecked;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                if (IsRunning == 1)
                {
                    thread.Suspend();
                }
                else
                {
                    thread.Resume();
                }

                IsRunning = (IsRunning + 1) % 2;
            }
        }
    }
}
