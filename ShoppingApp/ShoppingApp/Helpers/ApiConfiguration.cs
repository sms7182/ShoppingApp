using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.Helpers
{
    public class ApiConfiguration
    {
        public const string GetItemByCodeUrl = "http://192.168.1.50/fatalerror/api/item/byid?itemcode={0}";
        public const string GetInvoiceByUserUrl = "http://192.168.1.50/fatalerror/api/invoice/byid?userid={0}";

    }
}
