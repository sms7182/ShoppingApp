using Newtonsoft.Json;
using ShoppingBusinessObject;
using SQLite;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public static async Task<Guid> Login(string username, string password)
        {
            var isMobileEmpty = string.IsNullOrWhiteSpace(username);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(password);
            if (isMobileEmpty || isPasswordEmpty)
            {
                return Guid.Empty;
            }
            else
            {
                //todo: Loading User from Server
                UserInfo user = null;
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var url = ApiConfiguration.GetUserByName;

                        var response = client.GetStringAsync(string.Format(url, username)).Result;
                        if (!string.IsNullOrWhiteSpace(response))
                        {
                            user = JsonConvert.DeserializeObject<UserInfo>(response);
                        }

                    }
                    catch (Exception e)
                    {

                    }
                }

                if (user != null)
                {
                    if (user.Password == password)
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

                        return user.Id;
                    }                    
                }              
            }

            return Guid.Empty;
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

        public async Task<bool> DeleteUser(Guid id)
        {
            var success = false;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = ApiConfiguration.DeleteUser;

                    var response = await client.DeleteAsync(string.Format(url, id));
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var contents = await response.Content.ReadAsStringAsync();
                        success = JsonConvert.DeserializeObject<bool>(contents);
                    }

                }
                catch (Exception e)
                {

                }
            }

            if (success)
            {
                using (var _SQLiteConnection = Connect())
                {
                    _SQLiteConnection.Delete<UserInfo>(id);
                } 
            }

            return success;
        }

        public static async Task<bool> AddUser(UserInfo user)
        {

            var success = false;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = ApiConfiguration.GetUserByName;

                    var response = client.GetStringAsync(string.Format(url, user.PhoneNumber)).Result;
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        throw new Exception("شماره موبایل قبلاً ثبت شده است.");
                    }

                }
                catch(Exception)
                {
                    throw;
                }
            }
                               
            using (HttpClient client = new HttpClient())
            {
                try
                {                   
                    var url = ApiConfiguration.PostUser;
                    var json = JsonConvert.SerializeObject(user);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, stringContent);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var contents = await response.Content.ReadAsStringAsync();
                        success = JsonConvert.DeserializeObject<bool>(contents);
                    }

                }
                catch (Exception e)
                {

                }
            }

            return true;                                    
            
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
