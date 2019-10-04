using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.Helpers
{
    public class ApiConfiguration
    {
        private const string Url = "http://marketwebservice-fatalerror.fandogh.cloud";

        public const string GetUserByName = Url + "/api/login/byusername?userName={0}";
        public const string DeleteUser = Url + "/api/login/deluser/";
        public const string PostUser = Url + "/api/login/";

        
        public const string GetItemByCodeUrl = Url + "/api/item/byid?itemcode={0}";
        public const string GetInvoiceByUserUrl = Url + "/api/invoice/byUserId?userid={0}";
        public const string GetInvoiceById = Url + "/api/invoice/byid?id={0}";
        public const string PostInvoiceUrl = Url + "/api/invoice/";

        public const string GetAllStores = Url + "/api/store/getAll";
        public const string GetStoreById = Url + "/api/store/byid?id={0}";
    }
}
