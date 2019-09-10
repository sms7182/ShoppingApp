using ShoppingApp.Helpers;
using ShoppingApp.ViewModels.Commands;
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class RegisterVM : INotifyPropertyChanged,INavigatedPage
    {
        private string phoneNumber;
        private string password;
        private string confirmPassword;
        private UserInfo user;

        public event PropertyChangedEventHandler PropertyChanged;
        public RegistrationCommand RegistrationCommand { get; set; }
        public NavigationCommand NavigationCommand { get; set; }

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
                User = new UserInfo { PhoneNumber = this.PhoneNumber, Password = this.Password };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public RegisterVM()
        {
            RegistrationCommand = new RegistrationCommand(this);
            NavigationCommand = new NavigationCommand(this);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async void Register(UserInfo newUser)
        {
            
            try
            {
                var retrunvalue = UserDB.AddUser(newUser);
                if (retrunvalue)
                {
                    await App.Current.MainPage.DisplayAlert("کاربر جدید", "کاربر جدید با موفقیت اضافه شد.", "OK");
                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("کاربر جدید", "خطا در هنگام ثبت کاربر جدید رخ داد.", "OK");                    
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("کاربر جدید", ex.Message, "OK");
                //Debug.WriteLine(ex);
            }
        }

        public void Navigate()
        {
            App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
