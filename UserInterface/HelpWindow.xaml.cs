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
using System.IO;

namespace HCI_2016_Project.UserInterface
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow(String key, String section, Window originator)
        {
            InitializeComponent();

            string helpDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName.Replace("\\bin", "");

            string path = String.Format("{0}\\HelpHtml\\{1}.html", helpDirectory, key);
            
            Console.WriteLine(path);

            if (!File.Exists(path))
            {
                key = "error";
            }

            Uri u = new Uri(String.Format("file:///{0}/HelpHtml/{1}.html#{2}", helpDirectory, key, section));

            HelpWindowWB.Navigate(u);
        }
    }
}
