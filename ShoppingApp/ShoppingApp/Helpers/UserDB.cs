using ShoppingApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ShoppingApp.Helpers
{
    public class UserDB
    {
        public SQLiteConnection Connect()
        {
           var _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
            _SQLiteConnection.CreateTable<UserInfoViewModel>();
            return _SQLiteConnection;
        }
        public IEnumerable<UserInfoViewModel> GetUsers()
        {
            using (var _SQLiteConnection = Connect())
            {
                return (from u in _SQLiteConnection.Table<UserInfoViewModel>()
                        select u).ToList(); 
            }
        }
        public UserInfoViewModel GetSpecificUser(Guid id)
        {
            using (var _SQLiteConnection = Connect())
            {
                return _SQLiteConnection.Table<UserInfoViewModel>().FirstOrDefault(t => t.Id == id); 
            }
        }
        public void DeleteUser(int id)
        {
            using (var _SQLiteConnection = Connect())
            {
                _SQLiteConnection.Delete<UserInfoViewModel>(id); 
            }
        }
        public string AddUser(UserInfoViewModel user)
        {
            using (var _SQLiteConnection = Connect())
            {
                var data = _SQLiteConnection.Table<UserInfoViewModel>();
                var d1 = data.Where(x => x.PhoneNumber == user.PhoneNumber).FirstOrDefault();
                if (d1 == null)
                {
                    _SQLiteConnection.Insert(user);
                    return "Sucessfully Added";
                }
                else
                    return "Already Phone id Exist"; 
            }
        }
        public bool updateUserValidation(string userid)
        {
            using (var _SQLiteConnection = Connect())
            {
                var data = _SQLiteConnection.Table<UserInfoViewModel>();
                var d1 = (from values in data
                          where values.PhoneNumber == userid
                          select values).SingleOrDefault();
                if (d1 != null)
                {
                    return true;
                }
                else
                    return false; 
            }
        }
        public bool updateUser(string username, string pwd)
        {
            using (var _SQLiteConnection = Connect())
            {
                var data = _SQLiteConnection.Table<UserInfoViewModel>();
                var d1 = (from values in data
                          where values.PhoneNumber == username
                          select values).SingleOrDefault();
                if (true)
                {
                    d1.Password = pwd;
                    _SQLiteConnection.Update(d1);
                    return true;
                }
                else
                    return false; 
            }
        }
        public bool LoginValidate(string userName1, string pwd1)
        {
            using (var _SQLiteConnection = Connect())
            {
                var data = _SQLiteConnection.Table<UserInfoViewModel>();
                var d1 = data.Where(x => x.PhoneNumber == userName1 && x.Password == pwd1).FirstOrDefault();
                if (d1 != null)
                {
                    return true;
                }
                else
                    return false; 
            }
        }
    }
}
