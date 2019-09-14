using System.ComponentModel;

namespace ShoppingBusinessObject
{
    public class UserInfo : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string nationalityCode;
        private string phoneNumber;
        private string email;
        private string password;

        public string FirstName
        {
            get => firstName; set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => lastName; set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string NationalityCode
        {
            get => nationalityCode; set
            {
                nationalityCode = value;
                OnPropertyChanged("NationalityCode");
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber; set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Email
        {
            get => email; set
            {
                email = value;
                OnPropertyChanged("Email");

            }
        }
        public string Password
        {
            get => password; set
            {
                password = value;
                OnPropertyChanged("Password");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
