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
                new Invoice{ Store = store,CreationDate = DateTime.Now.AddDays(-1), TotalPrice = 8000, Code="001",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 8000,
                    InvoiceItems = new List<InvoiceItem>{
                        new InvoiceItem{Code = "01",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="صابون دستشویی",ItemNumber="111",Quantity = 1 ,UnitPrice = 4500,Unit="عدد",TotalPrice=4500,NetPrice =4500},
                        new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="دستمال آشپزخانه",ItemNumber="122",Quantity = 1 ,UnitPrice = 1500,Unit="عدد",TotalPrice=1500,NetPrice =1500},
                        new InvoiceItem{Code = "03",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="مایع ظرفشویی اتک",ItemNumber="331",Quantity = 2 ,UnitPrice = 1000,Unit="عدد",TotalPrice=2000,NetPrice =2000},
                    } },
                new Invoice{ Store = store,CreationDate = DateTime.Now.AddDays(-11), TotalPrice = 5500, Code="002",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 5500,
                 InvoiceItems = new List<InvoiceItem>{
                        new InvoiceItem{Code = "01",CreationDate=DateTime.Now.AddDays(-11),Id = Guid.NewGuid(),ItemName="سفره یکبار مصرف",ItemNumber="441",Quantity = 2 ,UnitPrice = 500,Unit="عدد",TotalPrice=1000,NetPrice =1000},
                        new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-11),Id = Guid.NewGuid(),ItemName="لیوان کاغذی",ItemNumber="622",Quantity = 3 ,UnitPrice = 1500,Unit="عدد",TotalPrice=4500,NetPrice =4500},
                    }
                }

            };

            return invoices;
        }
    }
}
