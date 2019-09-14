using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingApp.ViewModels.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginVM viewModel { get; set; }

        public LoginCommand(LoginVM userVM)
        {
            this.viewModel = userVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var user = (UserInfo)parameter;
            if(user == null)
            {
                return false;
            }

            var isMobileEmpty = string.IsNullOrWhiteSpace(user.PhoneNumber);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(user.Password);
            if (isMobileEmpty || isPasswordEmpty)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Login();
        }
    }
}
