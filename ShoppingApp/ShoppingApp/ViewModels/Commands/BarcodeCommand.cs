using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using ShoppingBusinessObject;

namespace ShoppingApp.ViewModels.Commands
{
    public class BarcodeCommand:ICommand
    {
        public BarcodeCommand(InvoiceViewModel invoiceViewModel)
        {
            InvoiceViewModel = invoiceViewModel;
        }
        public InvoiceViewModel InvoiceViewModel { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }


        public  void Execute(object parameter)
        {
           
            InvoiceViewModel.Scan();
        }
        

        public event EventHandler CanExecuteChanged;
    }
}
