using DevExpress.Mobile.DataGrid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingApp.ViewModels.Commands
{
    public class ListTapCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        InvoiceViewModel viewModel;
        public ListTapCommand(InvoiceViewModel invoiceVM)
        {
            viewModel = invoiceVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var e = (RowTapEventArgs)parameter;
            if (e == null)
            {
                return;
            }

            if (e.FieldName == "IncButton")
            {
                viewModel.IncQuantity(e.RowHandle);
            }
            else if (e.FieldName == "DecButton")
            {
                viewModel.DecQuantity(e.RowHandle);
            }
        }
    }
}
