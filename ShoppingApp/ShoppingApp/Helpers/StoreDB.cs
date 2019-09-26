using Newtonsoft.Json;
using ShoppingApp.ViewModels.Contracts;
using ShoppingBusinessObject;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingApp.Helpers
{
    public class StoreDB
    {
        public static SQLiteConnection Connect()
        {
            var _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
            _SQLiteConnection.CreateTable<UserInfo>();
            return _SQLiteConnection;
        }

        public static async Task<List<StoreInfo>> Read()
        {
            var stores = new List<StoreInfo>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = ApiConfiguration.GetAllStores;

                    var response = client.GetStringAsync(string.Format(url)).Result;
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        stores = JsonConvert.DeserializeObject<List<StoreInfo>>(response);
                    }
                }
                catch (Exception e)
                { }
            }

            //using (var conn = Connect())
            //{
            //    conn.CreateTable<Store>();
            //    Store store1 = new Store { Id = Guid.Parse("C3F2E3D8-9F7E-42DA-A3DA-12AF68B7C40A"), Name = "سوپرمارکت ماد", Address = "تهران-میدان توحید-خیابان اردشیر-پلاک 77" };
            //    conn.Insert(store1);
            //    stores.Add(store1);

            //    var store2 = new Store { Id = Guid.Parse("C4F2E3D8-9F7E-42DA-A3DA-12AF68B7C40A"), Name = "مرکز خرید بزرگ", Address = "تهران-پیروزان-کوچه علیمردانی-نبش آگاهی-پلاک 33" };
            //    conn.Insert(store2);
            //    stores.Add(store2);

            //    var store3 = new Store { Id = Guid.Parse("C5F2E3D8-9F7E-42DA-A3DA-12AF68B7C40A"), Name = "فروشگاه کوروش", Address = "تهران-خیابان ولیعصر- پلاک 45" };
            //    conn.Insert(store3);
            //    stores.Add(store3);

            //    var store4 = new Store { Id = Guid.Parse("C6F2E3D8-9F7E-42DA-A3DA-12AF68B7C40A"), Name = "فروشگاه رفاه", Address = "تهران-تجریش-میدان قدس-پلاک 11" };
            //    conn.Insert(store4);
            //    stores.Add(store4);

            //    var store5 = new Store { Id = Guid.Parse("C7F2E3D8-9F7E-42DA-A3DA-12AF68B7C40A"), Name = "فروشگاه شهروند", Address = "تهران-تجریش-پلاک 1" };
            //    conn.Insert(store5);
            //    stores.Add(store5);
            //};
            
            return stores;
        }

        public static async Task<StoreInfo> GetStore(Guid id)
        {
            StoreInfo store = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = ApiConfiguration.GetStoreById;

                    var response = client.GetStringAsync(string.Format(url, id)).Result;
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        store = JsonConvert.DeserializeObject<StoreInfo>(response);
                    }

                }
                catch (Exception e)
                {

                }
            }

            return store;
        }

    }
}
