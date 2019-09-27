using Newtonsoft.Json;
using ShoppingApp.ViewModels;
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
    public class InvoiceDB
    {
        public static SQLiteConnection Connect()
        {
            var _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
            _SQLiteConnection.CreateTable<UserInfo>();
            return _SQLiteConnection;
        }

        public static async Task<InvoiceInfo> GetLocalInvoice(Guid storeId,Guid userId)
        {
            using (var conn = Connect())
            {
                conn.CreateTable<InvoiceInfo>();
                var savedInvoiceInfo = conn.Table<InvoiceInfo>()
                    .Where(it => it.CreatedById == userId && it.StoreId == storeId)
                    .OrderByDescending(it => it.CreationDate)
                    .FirstOrDefault();

                if (savedInvoiceInfo != null)
                {
                    return savedInvoiceInfo;
                }
                
            }

            return null;
        }

        public static async Task<bool> SaveLocal(InvoiceInfo invoice)
        {
            try
            {
                using (var conn = Connect())
                {
                    conn.CreateTable<InvoiceInfo>();
                    var savedInvoiceInfo = conn.Table<InvoiceInfo>().Where(it=>it.Id == invoice.Id).FirstOrDefault();
                    if (savedInvoiceInfo == null)
                    {
                        conn.Insert(invoice);
                    }
                    else
                    {
                        conn.Update(invoice);
                    }                    
                }
            }
            catch (Exception e)
            {

                return false;
            }

            return true;
        }

        public static async Task<bool> DeleteLocal(Guid invoiceId)
        {
            try
            {
                using (var conn = Connect())
                {
                    conn.CreateTable<InvoiceInfo>();
                    var savedInvoiceInfo = conn.Table<InvoiceInfo>().Where(it => it.Id == invoiceId).FirstOrDefault();
                    if (savedInvoiceInfo != null)
                    {
                        conn.Delete(savedInvoiceInfo);
                    }                    
                }
            }
            catch (Exception e)
            {

                return false;
            }

            return true;
        }

        public static async Task<List<FlatInvoiceInfo>> Read()
        {
            var invoices = new List<FlatInvoiceInfo>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = ApiConfiguration.GetInvoiceByUserUrl;

                    var response = client.GetStringAsync(string.Format(url, App.CurrentUserId)).Result;
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        invoices = JsonConvert.DeserializeObject<List<FlatInvoiceInfo>>(response);
                    }
                } catch (Exception e)
                { }
            }
            //    var store = new Store { Name = "سوپرمارکت ماد", Address = "تهران-میدان توحید-خیابان اردشیر-پلاک 77" };
            //var invoices = new List<Invoice>() {
            //    new Invoice{ Store = store,CreationDate = DateTime.Now.AddDays(-1), TotalPrice = 8000, Code="001",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 8000,
            //        InvoiceItems = new List<InvoiceItem>{
            //            new InvoiceItem{Code = "01",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="صابون دستشویی",ItemNumber="111",Quantity = 1 ,UnitPrice = 4500,Unit="عدد",TotalPrice=4500,NetPrice =4500},
            //            new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="دستمال آشپزخانه",ItemNumber="122",Quantity = 1 ,UnitPrice = 1500,Unit="عدد",TotalPrice=1500,NetPrice =1500},
            //            new InvoiceItem{Code = "03",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="مایع ظرفشویی اتک",ItemNumber="331",Quantity = 2 ,UnitPrice = 1000,Unit="عدد",TotalPrice=2000,NetPrice =2000},
            //        } },
            //    new Invoice{ Store = store,CreationDate = DateTime.Now.AddDays(-11), TotalPrice = 5500, Code="002",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 5500,
            //     InvoiceItems = new List<InvoiceItem>{
            //            new InvoiceItem{Code = "01",CreationDate=DateTime.Now.AddDays(-11),Id = Guid.NewGuid(),ItemName="سفره یکبار مصرف",ItemNumber="441",Quantity = 2 ,UnitPrice = 500,Unit="عدد",TotalPrice=1000,NetPrice =1000},
            //            new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-11),Id = Guid.NewGuid(),ItemName="لیوان کاغذی",ItemNumber="622",Quantity = 3 ,UnitPrice = 1500,Unit="عدد",TotalPrice=4500,NetPrice =4500},
            //        }
            //    }

            //};

            return invoices;
        }

        public static async Task<InvoiceInfo> GetById(Guid id)
        {
            InvoiceInfo invoice = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = ApiConfiguration.GetInvoiceById;

                    var response = client.GetStringAsync(string.Format(url, id)).Result;
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        invoice = JsonConvert.DeserializeObject<InvoiceInfo>(response);                        
                    }
                }
                catch (Exception e)
                { }
            }
            //    var store = new Store { Name = "سوپرمارکت ماد", Address = "تهران-میدان توحید-خیابان اردشیر-پلاک 77" };
            //var invoices = new List<Invoice>() {
            //    new Invoice{ Store = store,CreationDate = DateTime.Now.AddDays(-1), TotalPrice = 8000, Code="001",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 8000,
            //        InvoiceItems = new List<InvoiceItem>{
            //            new InvoiceItem{Code = "01",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="صابون دستشویی",ItemNumber="111",Quantity = 1 ,UnitPrice = 4500,Unit="عدد",TotalPrice=4500,NetPrice =4500},
            //            new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="دستمال آشپزخانه",ItemNumber="122",Quantity = 1 ,UnitPrice = 1500,Unit="عدد",TotalPrice=1500,NetPrice =1500},
            //            new InvoiceItem{Code = "03",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="مایع ظرفشویی اتک",ItemNumber="331",Quantity = 2 ,UnitPrice = 1000,Unit="عدد",TotalPrice=2000,NetPrice =2000},
            //        } },
            //    new Invoice{ Store = store,CreationDate = DateTime.Now.AddDays(-11), TotalPrice = 5500, Code="002",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 5500,
            //     InvoiceItems = new List<InvoiceItem>{
            //            new InvoiceItem{Code = "01",CreationDate=DateTime.Now.AddDays(-11),Id = Guid.NewGuid(),ItemName="سفره یکبار مصرف",ItemNumber="441",Quantity = 2 ,UnitPrice = 500,Unit="عدد",TotalPrice=1000,NetPrice =1000},
            //            new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-11),Id = Guid.NewGuid(),ItemName="لیوان کاغذی",ItemNumber="622",Quantity = 3 ,UnitPrice = 1500,Unit="عدد",TotalPrice=4500,NetPrice =4500},
            //        }
            //    }

            //};

            return invoice;
        }


        public static async Task<bool> Save(InvoiceInfo invoice)
        {
            var success = false;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = ApiConfiguration.PostInvoiceUrl;
                    var json = JsonConvert.SerializeObject(invoice);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, stringContent);
                    if (response!=null && response.IsSuccessStatusCode)
                    {
                        var contents = await response.Content.ReadAsStringAsync();
                        success = JsonConvert.DeserializeObject<bool>(contents);
                    }
                }
                catch (Exception e)
                {

                }
            }

            return success;
        }

        public static async Task<ItemInfo> GetItemByCode(string itemCode)
        {
            ItemInfo findItem = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = ApiConfiguration.GetItemByCodeUrl;

                    var response = client.GetStringAsync(string.Format(url, itemCode)).Result;
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        findItem = JsonConvert.DeserializeObject<ItemInfo>(response);                        
                    }

                }
                catch (Exception e)
                {

                }
            }

            return findItem;
        }
    }
}
