
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class UserInfoViewModel
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }


    }

    public class SavedUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
