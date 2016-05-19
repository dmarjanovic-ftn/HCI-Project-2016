using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.Commands
{
    public static class Commands
    {
        public static readonly ICommand ShowManifestationDetails = new ShowManifestationDetailsCommand();
        public static readonly ICommand RemoveManifestationFromMap = new RemoveManifestationFromMapCommand();
        public static readonly ICommand EditManifestation = new EditManifestationCommand();
    }

    public class ShowManifestationDetailsCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = new HCI_2016_Project.UserInterface.Dialogs.ManifestationDetailsWindow(parameter as Manifestation);
            window.Show();
        }
    }

    public class RemoveManifestationFromMapCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            // Console.WriteLine(parameter);
            // Console.WriteLine("Remove from Map");
            // Console.Beep();
        }
    }

    public class EditManifestationCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = new HCI_2016_Project.UserInterface.Dialogs.EditManifestationDialog(parameter as Manifestation);
            window.Show();
        }
    }
}
