using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingApp.ViewModels.Commands
{
    public class NavigationCommand : ICommand
    {
        public LoginVM LoginViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public NavigationCommand(LoginVM loginVM)
        {
            LoginViewModel = loginVM;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            LoginViewModel.Navigate();
        }
    }
}
