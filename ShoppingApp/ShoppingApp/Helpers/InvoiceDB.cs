using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Helpers
{
    public class InvoiceDB
    {
        public static async Task<List<Invoice>> Read()
        {
            var store = new Store { Name = "سوپرمارکت ماد", Address = "تهران-میدان توحید-خیابان اردشیر-پلاک 77" };
            var invoices = new List<Invoice>() {
                new Invoice{ Store = store,CreationDate = DateTime.Now.AddDays(-1), TotalPrice = 175000, Code="001",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 175000},
                new Invoice{ Store = store,CreationDate = DateTime.Now.AddDays(-11), TotalPrice = 2350, Code="002",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 2350}

            };

            return invoices;
        }
    }
}
