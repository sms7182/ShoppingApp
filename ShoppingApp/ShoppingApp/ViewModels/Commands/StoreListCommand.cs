using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingApp.ViewModels.Commands
{
    public class StoreListCommand : ICommand
    {
        public StoreListVM viewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public StoreListCommand(StoreListVM listVM)
        {
            viewModel = listVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var selectedStore = (Store)parameter;
            viewModel.CreateInvoice(selectedStore);
        }
    }
}
