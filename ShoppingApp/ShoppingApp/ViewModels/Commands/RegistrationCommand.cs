using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingApp.ViewModels.Commands
{
    public class RegistrationCommand : ICommand
    {
        public RegisterVM ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public RegistrationCommand(RegisterVM view)
        {
            ViewModel = view;
        }
        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

           // return true;


            if ((string.IsNullOrWhiteSpace(ViewModel.PhoneNumber)) || (string.IsNullOrWhiteSpace(ViewModel.Password)))
            {
                return false;
            }
            else if (!string.Equals(ViewModel.Password, ViewModel.ConfirmPassword))
            {
                return false;
            }
            else if (ViewModel.PhoneNumber.Length < 11)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Register(ViewModel.User);
        }
    }
}
