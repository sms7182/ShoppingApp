using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingApp.ViewModels.Commands
{
    public class NavigationCommand : ICommand
    {
        public INavigatedPage ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public NavigationCommand(INavigatedPage vM)
        {
            ViewModel = vM;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Navigate();
        }
    }
}
