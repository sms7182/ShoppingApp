using ShoppingBusinessObject;
using SQLite;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingApp.Helpers
{
    public abstract class UserDB
    {
        public static SQLiteConnection Connect()
        {
            var _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
            _SQLiteConnection.CreateTable<UserInfo>();
            return _SQLiteConnection;
        }

        public static async Task<bool> Login(string username, string password)
        {
            var isMobileEmpty = string.IsNullOrWhiteSpace(username);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(password);
            if (isMobileEmpty || isPasswordEmpty)
            {
                return false;
            }
            else
            {
                //todo: Loading User from Server
                UserInfo user = new UserInfo();

                if (user != null)
                {
                    //if (user.Password == password)
                    {
                        using (var conn = Connect())
                        {
                            conn.CreateTable<SavedUser>();
                            var savedUserInfo = conn.Table<SavedUser>().FirstOrDefault();
                            if (savedUserInfo == null)
                            {
                                savedUserInfo = new SavedUser();
                            }
                            savedUserInfo.UserName = username;
                            savedUserInfo.Password = password;
                            var rows = conn.Insert(savedUserInfo);
                        }

                        return true;
                    }
                    //else
                    //{
                    //    return false;
                    //}
                }
                else
                {
                    return false;
                }


            }
        }

        public static async Task<SavedUser> GetLocalUser()
        {
            using (var conn = Connect())
            {
                conn.CreateTable<SavedUser>();
                var savedUserInfo = conn.Table<SavedUser>().FirstOrDefault();
                if (savedUserInfo != null)
                {
                    return savedUserInfo;
                }
            }

            return null;
        }

        public void DeleteUser(int id)
        {
            using (var _SQLiteConnection = Connect())
            {
                _SQLiteConnection.Delete<UserInfo>(id);
            }
        }

        public static bool AddUser(UserInfo user)
        {
            using (var _SQLiteConnection = Connect())
            {
                var data = _SQLiteConnection.Table<UserInfo>();
                var d1 = data.Where(x => x.PhoneNumber == user.PhoneNumber).FirstOrDefault();
                if (d1 == null)
                {
                    _SQLiteConnection.Insert(user);
                    return true;
                }
                else
                    throw new Exception("شماره موبایل قبلاً ثبت شده است.");
            }
        }


        public static bool updateUser(string username, string pwd)
        {
            using (var _SQLiteConnection = Connect())
            {
                var data = _SQLiteConnection.Table<UserInfo>();
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
    }

    public class SavedUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
