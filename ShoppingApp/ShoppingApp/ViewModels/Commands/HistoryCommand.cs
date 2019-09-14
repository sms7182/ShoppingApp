using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingApp.ViewModels.Commands
{
    public class HistoryCommand : ICommand
    {
        HistoryVM viewModel;
        public event EventHandler CanExecuteChanged;

        public HistoryCommand(HistoryVM model)
        {
            viewModel = model;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var selectedInvoice = (Invoice)parameter;
            viewModel.ViewInvoice(selectedInvoice);
        }
    }
}
