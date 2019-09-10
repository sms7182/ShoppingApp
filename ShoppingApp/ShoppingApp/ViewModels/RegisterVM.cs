using ShoppingApp.Helpers;
using ShoppingApp.ViewModels.Commands;
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class RegisterVM : INotifyPropertyChanged
    {
        private string phoneNumber;
        private string password;
        private string confirmPassword;
        private UserInfo user;

        public event PropertyChangedEventHandler PropertyChanged;
        public RegistrationCommand RegistrationCommand { get; set; }

        public UserInfo User
        {
            get => user; set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber; set
            {
                phoneNumber = value; User = new UserInfo { PhoneNumber = this.PhoneNumber, Password = this.Password };
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Password
        {
            get => password; set
            {
                password = value; User = new UserInfo { PhoneNumber = this.PhoneNumber, Password = this.Password };
                OnPropertyChanged("Password");
            }
        }
        public string ConfirmPassword
        {
            get => confirmPassword; set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public RegisterVM()
        {
            RegistrationCommand = new RegistrationCommand(this);
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Register(UserInfo newUser)
        {
            var retrunvalue = UserDB.AddUser(newUser);
        }
    }
}
