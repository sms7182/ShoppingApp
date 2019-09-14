using ShoppingApp.Helpers;
using ShoppingApp.ViewModels;
using ShoppingBusinessObject;
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
        RegisterVM viewModel;
        UserInfo newUser;
        
        public RegistrationPage()
        {
            InitializeComponent();
            viewModel = new RegisterVM();
            BindingContext = viewModel;           
            NavigationPage.SetHasBackButton(this, false);                      
        }
        //private async void SignupValidation_ButtonClicked(object sender, EventArgs e)
        //{
        //    if ((string.IsNullOrWhiteSpace(passwordEntry.Text)) || (string.IsNullOrWhiteSpace(phoneEntry.Text)))
        //    {
        //        await DisplayAlert("Enter Data", "Enter Valid Data", "OK");
        //    }
        //    else if (!string.Equals(passwordEntry.Text, confirmpasswordEntry.Text))
        //    {
        //        warningLabel.Text = "Enter Same Password";
        //        passwordEntry.Text = string.Empty;
        //        confirmpasswordEntry.Text = string.Empty;
        //        warningLabel.TextColor = Color.IndianRed;
        //        warningLabel.IsVisible = true;
        //    }
        //    else if (phoneEntry.Text.Length < 10)
        //    {
        //        phoneEntry.Text = string.Empty;
        //        phoneWarLabel.Text = "Enter 10 digit Number";
        //        phoneWarLabel.TextColor = Color.IndianRed;
        //        phoneWarLabel.IsVisible = true;
        //    }
        //    else
        //    {                
        //        try
        //        {
        //            var retrunvalue = UserDB.AddUser(newUser);
        //            if (retrunvalue)
        //            {
        //                await DisplayAlert("کاربر جدید", "کاربر جدید با موفقیت اضافه شد.", "OK");
        //                await Navigation.PushAsync(new LoginPage());
        //            }
        //            else
        //            {
        //                await DisplayAlert("کاربر جدید", "خطا در هنگام ثبت کاربر جدید رخ داد.", "OK");
        //                warningLabel.IsVisible = false;                                              
        //                passwordEntry.Text = string.Empty;
        //                confirmpasswordEntry.Text = string.Empty;
        //                phoneEntry.Text = string.Empty;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await DisplayAlert("کاربر جدید", ex.Message, "OK");
        //            //Debug.WriteLine(ex);
        //        }
        //    }
        //}
        //private async void login_ClickedEvent(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new LoginPage());
        //}
    }
}