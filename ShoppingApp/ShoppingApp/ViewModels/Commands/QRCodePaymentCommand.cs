using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ShoppingApp.Views;

namespace ShoppingApp.ViewModels.Commands
{
    public class QRCodePaymentCommand : ICommand
    {
        public InvoiceViewModel InvoiceViewModel { get; set; }
        public QRCodePaymentCommand(InvoiceViewModel invoiceViewModel)
        {
            InvoiceViewModel = invoiceViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            InvoiceViewModel.NavigateToQRCodePage();
        }

        public event EventHandler CanExecuteChanged;
    }
}
