using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using HCI_2016_Project.DataClasses;
using HCI_2016_Project.UserInterface.Dialogs;

namespace HCI_2016_Project.Commands
{
    public static class Commands
    {
        public static readonly ICommand ShowManifestationDetails = new ShowManifestationDetailsCommand();
        public static readonly ICommand RemoveManifestationFromMap = new RemoveManifestationFromMapCommand();
        public static readonly ICommand EditManifestation = new EditManifestationCommand();

        public static readonly ICommand ShowAllManifestations = new ShowAllManifestationsCommand();
        public static readonly ICommand AddManifestation = new AddManifestationCommand();
        public static readonly ICommand ShowAllManifestationTypes = new ShowAllManifestationTypesCommand();
        public static readonly ICommand AddManifestationType = new AddManifestationTypeCommand();
        public static readonly ICommand ShowAllTags = new ShowAllTagsCommand();
        public static readonly ICommand AddTag = new AddTagCommand();
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

    public class ShowAllManifestationsCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = new HCI_2016_Project.UserInterface.Dialogs.ShowManifestationsDialog();
            window.Show();
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

    public class AddManifestationCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = new HCI_2016_Project.UserInterface.Dialogs.AddManifestationDialog();
            window.Show();
        }
    }

    public class ShowAllManifestationTypesCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = new HCI_2016_Project.UserInterface.Dialogs.ShowManifestationsTypeDialog();
            window.Show();
        }
    }

    public class AddManifestationTypeCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = new HCI_2016_Project.UserInterface.Dialogs.AddManifestationTypeDialog();
            window.Show();
        }
    }

    public class ShowAllTagsCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = new HCI_2016_Project.UserInterface.Dialogs.ShowTagsDialog();
            window.Show();
        }
    }

    public class AddTagCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = new HCI_2016_Project.UserInterface.Dialogs.AddTagDialog();
            window.Show();
        }
    }
}
