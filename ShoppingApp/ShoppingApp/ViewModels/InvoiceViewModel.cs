
using System;
using System.Collections.Generic;
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
    public class InvoiceViewModel:ModelObject
    {
        #region fields

        private readonly BindingList<InvoiceItem> invoiceItems;
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
            QrCodePaymentCommand=new QRCodePaymentCommand(this);
            invoiceItems=new BindingList<InvoiceItem>();
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
         await   HandleResult(result);
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
                            
                            var response= await client.GetStringAsync(string.Format(url,result.Text));
                            if (!string.IsNullOrWhiteSpace(response))
                            {
                              var itemInfo=    JsonConvert.DeserializeObject<ItemInfo>(response);

                                if (itemInfo != null)
                                {

                                    var invoiceItem = new InvoiceItem();
                                    invoiceItem.Quantity = 1;
                                    invoiceItem.ItemName = itemInfo.Name;
                                    invoiceItem.Unit = itemInfo.Unit.ToString();
                                    invoiceItem.UnitPrice = itemInfo.UnitPrice;
                                    invoiceItem.TotalPrice = itemInfo.UnitPrice;
                                    invoiceItem.NetPrice=itemInfo.UnitPrice;

                                    InvoiceItems.Add(invoiceItem);

                                }

                            }
                          
                        }
                        catch (Exception e)
                        {

                        }
                    }


                }
                InvoiceItems.ResetBindings();
                
                
            }
        }

        public async void NavigateToQRCodePage()
        {
            var paymentQrCodePage = new PaymentQRCodePage(this);
           await App.Current.MainPage.Navigation.PushAsync(paymentQrCodePage);
        }
    }
}
