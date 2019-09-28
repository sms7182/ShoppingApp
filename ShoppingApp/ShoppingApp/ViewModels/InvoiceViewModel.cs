
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mobile.DataGrid;
using ShoppingApp.Annotations;
using ShoppingApp.ViewModels.Commands;
using ShoppingApp.Views;
using ShoppingBusinessObject;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using Newtonsoft.Json;
using ShoppingApp.Helpers;
using ShoppingApp.ViewModels.Contracts;

namespace ShoppingApp.ViewModels
{
    public class ItemInfo {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Unit { get; set; }

    }
    public class InvoiceViewModel : ModelObject
    {
        #region fields
        public Guid Id { get; set; }
        public UserInfo User { get; set; }
        public StoreInfo Store { get; set; }
        public string Code { get; set; }
        public Status Status { get; set; }
        public decimal TotalPrice
        {
            get => totalPrice; set
            {
                totalPrice = value;
                RaisePropertyChanged("TotalPrice");
            }
        }
        public decimal ItemCount
        {
            get => itemCount; set
            {
                itemCount = value;
                RaisePropertyChanged("ItemCount");
            }
        }

        public decimal NetPrice { get; set; }
        public decimal IncPrice { get; set; }
        public decimal DecPrice { get; set; }
        public DateTime CreationDate { get; set; }

        private ObservableCollection<InvoiceItem> invoiceItems;
        private decimal itemCount;
        private decimal totalPrice;

        #endregion

        public BarcodeCommand BarcodeScanCommand { get; set; }
        public QRCodePaymentCommand QrCodePaymentCommand { get; set; }

        public ObservableCollection<InvoiceItem> InvoiceItems
        {
            get { return invoiceItems; }
        }


        public InvoiceViewModel()
        {
            BarcodeScanCommand = new BarcodeCommand(this);
            QrCodePaymentCommand = new QRCodePaymentCommand(this);
            CreationDate = DateTime.Now;
            Id = Guid.NewGuid();
            invoiceItems = new ObservableCollection<InvoiceItem>()
            //{
            //    new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="دستمال آشپزخانه",ItemNumber="122",Quantity = 1 ,UnitPrice = 1500,Unit="عدد",TotalPrice=1500,NetPrice =1500},
            //    new InvoiceItem{Code = "03",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="مایع ظرفشویی اتک",ItemNumber="331",Quantity = 2 ,UnitPrice = 1000,Unit="عدد",TotalPrice=2000,NetPrice =2000},
            //}
            ;

            //var r = new Random(20);
            //var count = r.Next(20);
            //for (int i = 0; i < count; i++)
            //{
            //    HandleResult(new Result(((char)r.Next(65, 90)).ToString() ,null,null,BarcodeFormat.All_1D));
            //}

            ItemCount = invoiceItems.Count;
            TotalPrice = invoiceItems.Sum(it => it.TotalPrice);
            //InvoiceItems.AllowNew = true;


        }

        public async void SaveLocal()
        {
            var localInvoice = new InvoiceInfo
            {
                Id = Id,
                Code = Code,
                CreationDate = CreationDate,
                CreatedById = App.CurrentUserId,
                StoreId = Store.Id,
                StoreName = Store.Name,
                NetPrice = NetPrice,
                TotalPrice = TotalPrice
            };

            for (int i = 0; i < InvoiceItems.Count; i++)
            {
                var lineItem = InvoiceItems[i];
                var newLine = new InvoiceInfoLine
                {
                    Id = lineItem.Id,
                    ItemId = lineItem.ItemId,
                    ItemCode = lineItem.ItemNumber,
                    ItemName = lineItem.ItemName,
                    Quantity = lineItem.Quantity,
                    UnitPrice = lineItem.UnitPrice,
                    NetPrice = lineItem.NetPrice,
                    TotalPrice = lineItem.TotalPrice
                };

                localInvoice.InvoiceInfoLines.Add(newLine);

            }

            await InvoiceDB.SaveLocal(localInvoice);
        }

        public async void Scan()
        {
            var scanner = new MobileBarcodeScanner();
            var mobileBarcodeScanningOptions = new MobileBarcodeScanningOptions();
            mobileBarcodeScanningOptions.AutoRotate = true;
            scanner.AutoFocus();
            if (!scanner.IsTorchOn)
            {
                scanner.Torch(true);
            }
            scanner.TopText = "Hold the camera up to  the barcode ";
            scanner.BottomText = "wait for the barcode automatically  scan!";

            ZXing.Result result = await scanner.Scan(mobileBarcodeScanningOptions);
            await HandleResult(result);
        }

        private async Task HandleResult(Result result)
        {
            if (result != null)
            {

             
                if (InvoiceItems.Any(d => d.ItemNumber == result.Text))
                {

                    for (int i = 0; i < InvoiceItems.Count; i++)
                    {
                        if (InvoiceItems[i].ItemNumber == result.Text)
                        {
                            InvoiceItems[i].Quantity = InvoiceItems[i].Quantity + 1;
                            InvoiceItems[i].TotalPrice = (InvoiceItems[i].Quantity * InvoiceItems[i].UnitPrice);
                            ItemCount++;
                            TotalPrice += InvoiceItems[i].UnitPrice;
                            break;

                        }
                    }
                }
                else
                {
                    var itemInfo = await InvoiceDB.GetItemByCode(result.Text);
                    if (itemInfo != null)
                    {

                        var invoiceItem = new InvoiceItem();
                        invoiceItem.Quantity = 1;
                        invoiceItem.ItemName = itemInfo.Name;
                        invoiceItem.ItemNumber = itemInfo.Code;
                        invoiceItem.ItemId = itemInfo.Id;
                        invoiceItem.Unit = itemInfo.Unit.ToString();
                        invoiceItem.UnitPrice = itemInfo.UnitPrice;
                        invoiceItem.TotalPrice = itemInfo.UnitPrice;
                        invoiceItem.NetPrice = itemInfo.UnitPrice;

                        InvoiceItems.Add(invoiceItem);

                        ItemCount++;
                        TotalPrice += invoiceItem.TotalPrice;

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("خطا", "دوباره تلاش کنید", "باشه");
                    }
                                          
                }
                //InvoiceItems.ResetBindings();               

            }
        }

        public async void NavigateToQRCodePage()
        {
            var paymentQrCodePage = new PaymentQRCodePage(this);
            await App.Current.MainPage.Navigation.PushAsync(paymentQrCodePage);
        }

        public async void DeleteLine(int rowNo)
        {
            try
            {
                ItemCount -= invoiceItems[rowNo].Quantity;
                TotalPrice -= invoiceItems[rowNo].TotalPrice;
                invoiceItems.RemoveAt(rowNo);

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void IncQuantity(int rowNo)
        {
            invoiceItems[rowNo].Quantity++;
            invoiceItems[rowNo].NetPrice = invoiceItems[rowNo].Quantity * invoiceItems[rowNo].UnitPrice;
            invoiceItems[rowNo].TotalPrice = invoiceItems[rowNo].NetPrice + invoiceItems[rowNo].IncPrice - invoiceItems[rowNo].DecPrice;
            ItemCount++;
            TotalPrice += invoiceItems[rowNo].UnitPrice;
        }
        public void DecQuantity(int rowNo)
        {
            if (InvoiceItems[rowNo].Quantity == 1)
            {
                InvoiceItems.RemoveAt(rowNo);
                return;
            }

            InvoiceItems[rowNo].Quantity--;
            InvoiceItems[rowNo].NetPrice = InvoiceItems[rowNo].Quantity * InvoiceItems[rowNo].UnitPrice;
            InvoiceItems[rowNo].TotalPrice = InvoiceItems[rowNo].NetPrice + InvoiceItems[rowNo].IncPrice - InvoiceItems[rowNo].DecPrice;
            ItemCount--;
            TotalPrice -= invoiceItems[rowNo].UnitPrice;
        }

        public async Task<Guid?> Save()
        {            
            //var invoiceItems = invoiceViewModel.InvoiceItems;
            var invoice = new InvoiceInfo();
            invoice.Id = Guid.NewGuid();
            invoice.CreatedById = App.CurrentUserId;
            for (int i = 0; i < invoiceItems.Count; i++)
            {
                var temp = new InvoiceInfoLine();
                temp.Id = Guid.NewGuid();
                temp.ItemCode = invoiceItems[i].ItemNumber;
                temp.ItemName = invoiceItems[i].ItemName;
                temp.ItemId = invoiceItems[i].ItemId;
                temp.NetPrice = invoiceItems[i].NetPrice;
                temp.Quantity = invoiceItems[i].Quantity;
                temp.TotalPrice = invoiceItems[i].TotalPrice;
                temp.UnitPrice = invoiceItems[i].UnitPrice;

                invoice.InvoiceInfoLines.Add(temp);
            }

            //invoice.Code = "75";
            invoice.CreationDate = DateTime.UtcNow;
            invoice.StoreName = Store.Name;
            invoice.StoreId = Store.Id;

            invoice.NetPrice = invoiceItems.Sum(s => s.DecPrice);
            invoice.TotalPrice = invoiceItems.Sum(d => d.TotalPrice);

            var result = await InvoiceDB.Save(invoice);

            if(result)
            {
                await InvoiceDB.DeleteLocal(invoice.Id);
                return invoice.Id;
            }

            return null;

        }
    }


}