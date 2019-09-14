
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

        public UserInfo User { get; set; }
        public Store Store { get; set; }
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
            invoiceItems = new ObservableCollection<InvoiceItem>()
            {
                new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="دستمال آشپزخانه",ItemNumber="122",Quantity = 1 ,UnitPrice = 1500,Unit="عدد",TotalPrice=1500,NetPrice =1500},
                new InvoiceItem{Code = "03",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="مایع ظرفشویی اتک",ItemNumber="331",Quantity = 2 ,UnitPrice = 1000,Unit="عدد",TotalPrice=2000,NetPrice =2000},
            }
            ;

            ItemCount = invoiceItems.Count;
            TotalPrice = invoiceItems.Sum(it => it.TotalPrice);
            //InvoiceItems.AllowNew = true;


        }

        public Invoice Invoice { get; set; }


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


                if (InvoiceItems.Any(d => d.ItemName == result.Text))
                {

                    for (int i = 0; i < InvoiceItems.Count; i++)
                    {
                        if (InvoiceItems[i].ItemName == result.Text)
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

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            var url = ApiConfiguration.GetItemByCodeUrl;

                            var response = await client.GetStringAsync(string.Format(url, result.Text));
                            if (!string.IsNullOrWhiteSpace(response))
                            {
                                var itemInfo = JsonConvert.DeserializeObject<ItemInfo>(response);

                                if (itemInfo != null)
                                {

                                    var invoiceItem = new InvoiceItem();
                                    invoiceItem.Quantity = 1;
                                    invoiceItem.ItemName = itemInfo.Name;
                                    invoiceItem.Unit = itemInfo.Unit.ToString();
                                    invoiceItem.UnitPrice = itemInfo.UnitPrice;
                                    invoiceItem.TotalPrice = itemInfo.UnitPrice;
                                    invoiceItem.NetPrice = itemInfo.UnitPrice;

                                    InvoiceItems.Add(invoiceItem);

                                    ItemCount++;
                                    TotalPrice += invoiceItem.TotalPrice;

                                }

                            }

                        }
                        catch (Exception e)
                        {

                        }
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
    }


}