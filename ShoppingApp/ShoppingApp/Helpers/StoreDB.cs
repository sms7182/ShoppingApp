using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Helpers
{
    public class StoreDB
    {
        public static async Task<List<Store>> Read()
        {
            var stores = new List<Store>() {
                new Store{ Name = "سوپرمارکت ماد", Address = "تهران-میدان توحید-خیابان اردشیر-پلاک 77" },
                new Store{ Name = "مرکز خرید بزرگ", Address = "تهران-پیروزان-کوچه علیمردانی-نبش آگاهی-پلاک 33" },
                new Store{ Name = "فروشگاه کوروش", Address = "تهران-خیابان ولیعصر- پلاک 45" },
                new Store{ Name = "فروشگاه رفاه", Address = "تهران-تجریش-میدان قدس-پلاک 11" } ,
                new Store{ Name = "فروشگاه شهروند", Address = "تهران-تجریش-پلاک 1" }
            };

            return stores;
        }
    }
}
