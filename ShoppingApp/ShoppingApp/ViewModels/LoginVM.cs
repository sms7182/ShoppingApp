
using ShoppingApp.Helpers;
using ShoppingApp.ViewModels.Commands;
using ShoppingApp.Views;
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class LoginVM : INotifyPropertyChanged
    {
        public NavigationCommand NavCommand { get; set; }
        private UserInfo user;
        private string phoneNumber;
        private string password;

        public UserInfo User
        {
            get => user; set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginCommand LoginCommand { get; set; }
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

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public LoginVM()
        {
            User = new UserInfo();
            NavCommand = new NavigationCommand(this);
            LoginCommand = new LoginCommand(this);
        }
       
        public void Navigate()
        {
            App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        public async void Login()
        {
            bool canLogin = await UserDB.Login(User.PhoneNumber, User.Password);
            if(canLogin)
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("خطا","دوباره تلاش کنید","قبول");
            }
        }

    }   

}
