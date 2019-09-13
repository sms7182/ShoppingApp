
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace ShoppingApp.ViewModels
{
    public class InvoiceViewModel : ModelObject
    {
        #region fields

        public UserInfo User { get; set; }
        public Store Store { get; set; }
        public string Code { get; set; }
        public Status Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal NetPrice { get; set; }
        public decimal IncPrice { get; set; }
        public decimal DecPrice { get; set; }
        public DateTime CreationDate { get; set; }

        private BindingList<InvoiceItem> invoiceItems;
        #endregion
        public BarcodeCommand BarcodeScanCommand { get; set; }
        public QRCodePaymentCommand QrCodePaymentCommand { get; set; }

        public BindingList<InvoiceItem> InvoiceItems
        {
            get { return invoiceItems; }
        }


        public InvoiceViewModel()
        {
            BarcodeScanCommand = new BarcodeCommand(this);
            QrCodePaymentCommand = new QRCodePaymentCommand(this);
            invoiceItems = new BindingList<InvoiceItem>()
            {
                new InvoiceItem{Code = "02",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="دستمال آشپزخانه",ItemNumber="122",Quantity = 1 ,UnitPrice = 1500,Unit="عدد",TotalPrice=1500,NetPrice =1500},
                new InvoiceItem{Code = "03",CreationDate=DateTime.Now.AddDays(-1),Id = Guid.NewGuid(),ItemName="مایع ظرفشویی اتک",ItemNumber="331",Quantity = 2 ,UnitPrice = 1000,Unit="عدد",TotalPrice=2000,NetPrice =2000},
            }
            ;
            InvoiceItems.AllowNew = true;


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
            HandleResult(result);
        }
        private void HandleResult(Result result)
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
                            break;

                        }
                    }
                }
                else
                {
                    var invoiceItem = new InvoiceItem();
                    invoiceItem.Quantity = 1;
                    invoiceItem.ItemName = result.Text;
                    invoiceItem.Unit = "Number";
                    invoiceItem.UnitPrice = 750;
                    invoiceItem.TotalPrice = 750;
                    InvoiceItems.Add(invoiceItem);


                }
                InvoiceItems.ResetBindings();


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
                var list =InvoiceItems as BindingList<InvoiceItem>;
               list.RemoveAt(rowNo);
                invoiceItems = list;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void IncQuantity(int rowNo)
        {
            InvoiceItems[rowNo].Quantity++;
            InvoiceItems[rowNo].NetPrice = InvoiceItems[rowNo].Quantity * InvoiceItems[rowNo].UnitPrice;
            InvoiceItems[rowNo].TotalPrice = InvoiceItems[rowNo].NetPrice + InvoiceItems[rowNo].IncPrice - InvoiceItems[rowNo].DecPrice;
        }
        public void DecQuantity(int rowNo)
        {
            if(InvoiceItems[rowNo].Quantity==0)
            {
                InvoiceItems.RemoveAt(rowNo);
                return;
            }

            InvoiceItems[rowNo].Quantity--;
            InvoiceItems[rowNo].NetPrice = InvoiceItems[rowNo].Quantity * InvoiceItems[rowNo].UnitPrice;
            InvoiceItems[rowNo].TotalPrice = InvoiceItems[rowNo].NetPrice + InvoiceItems[rowNo].IncPrice - InvoiceItems[rowNo].DecPrice;
        }
    }


}