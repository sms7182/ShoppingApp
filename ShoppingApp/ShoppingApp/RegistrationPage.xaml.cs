using ShoppingApp.Helpers;
using ShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        UserInfoViewModel newUser = new UserInfoViewModel();
        UserDB userDB = new UserDB();

        public RegistrationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);            
           passwordEntry.ReturnCommand = new Command(() => confirmpasswordEntry.Focus());
            confirmpasswordEntry.ReturnCommand = new Command(() => phoneEntry.Focus());
        }
        private async void SignupValidation_ButtonClicked(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(passwordEntry.Text)) || (string.IsNullOrWhiteSpace(phoneEntry.Text)))
            {
                await DisplayAlert("Enter Data", "Enter Valid Data", "OK");
            }
            else if (!string.Equals(passwordEntry.Text, confirmpasswordEntry.Text))
            {
                warningLabel.Text = "Enter Same Password";
                passwordEntry.Text = string.Empty;
                confirmpasswordEntry.Text = string.Empty;
                warningLabel.TextColor = Color.IndianRed;
                warningLabel.IsVisible = true;
            }
            else if (phoneEntry.Text.Length < 10)
            {
                phoneEntry.Text = string.Empty;
                phoneWarLabel.Text = "Enter 10 digit Number";
                phoneWarLabel.TextColor = Color.IndianRed;
                phoneWarLabel.IsVisible = true;
            }
            else
            {                
                newUser.Password = passwordEntry.Text;
                newUser.PhoneNumber = phoneEntry.Text.ToString();
                try
                {
                    var retrunvalue = userDB.AddUser(newUser);
                    if (retrunvalue == "Sucessfully Added")
                    {
                        await DisplayAlert("User Add", retrunvalue, "OK");
                        await Navigation.PushAsync(new LoginPage());
                    }
                    else
                    {
                        await DisplayAlert("User Add", retrunvalue, "OK");
                        warningLabel.IsVisible = false;                                              
                        passwordEntry.Text = string.Empty;
                        confirmpasswordEntry.Text = string.Empty;
                        phoneEntry.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    //Debug.WriteLine(ex);
                }
            }
        }
        private async void login_ClickedEvent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}