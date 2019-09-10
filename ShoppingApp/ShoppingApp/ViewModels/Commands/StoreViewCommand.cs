using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingApp.ViewModels.Commands
{
    public class StoreViewCommand : ICommand
    {
        public StoreListVM viewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public StoreViewCommand(StoreListVM listView)
        {
            viewModel = listView;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var storeId = (Guid)parameter;
            viewModel.ViewStore(storeId);

        }
    }
}
 