using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;
using ShoppingApp.ViewModels;
using ShoppingBusinessObject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectListView : ContentPage
    {
        private int paymentsetting = 2;
        private InvoiceViewModel invoiceViewModel;
        private ICommand swipeButtonCommand;

        public ObjectListView(InvoiceViewModel viewModel)
        {
            InitializeComponent();
            
            devgrid.CustomUnboundColumnData += (object sender, DevExpress.Mobile.DataGrid.GridColumnDataEventArgs e) =>
            {
                if (e.Column.FieldName == "IncButton")
                {
                    e.Value = ImageSource.FromResource("ShoppingApp.Resources.IncIcon.png");
                }

                if (e.Column.FieldName == "DecButton")
                {
                    e.Value = ImageSource.FromResource("ShoppingApp.Resources.DecIcon.png");
                }
            };

            invoiceViewModel = viewModel;
            BindingContext = invoiceViewModel;

            ThemeManager.ThemeName = Themes.Light;

            // Header customization.
            ThemeManager.Theme.HeaderCustomizer.BackgroundColor = Color.LightBlue;//FromRgb(187, 228, 208);
            ThemeFontAttributes myFont = new ThemeFontAttributes("Verdana",
                                        ThemeFontAttributes.FontSizeFromNamedSize(NamedSize.Large),
                                        FontAttributes.None, Color.Black);
            ThemeManager.Theme.HeaderCustomizer.Font = myFont;


            // Cell customization.
            ThemeManager.Theme.CellCustomizer.SelectionColor = Color.LightGray;//.FromRgb(186, 220, 225);
            ThemeFontAttributes myFont1 = new ThemeFontAttributes("Verdana",
                                        ThemeFontAttributes.FontSizeFromNamedSize(NamedSize.Medium),
                                        FontAttributes.None, Color.Black);
            ThemeManager.Theme.CellCustomizer.Font = myFont1;


            // Various customization.
            ThemeManager.Theme.TotalSummaryCustomizer.BackgroundColor = Color.LightGreen;//.FromRgb(163, 162, 168);
            ThemeFontAttributes myFont2 = new ThemeFontAttributes("Verdana",
                                ThemeFontAttributes.FontSizeFromNamedSize(NamedSize.Default),
                                FontAttributes.None, Color.Black);
            ThemeManager.Theme.TotalSummaryCustomizer.Font = myFont2;
            ThemeManager.Theme.NewItemRowCustomizer.Font = myFont2;


            ThemeManager.RefreshTheme();
        }

        private void OnClicked(object sender, EventArgs e)
        {

        }


        private async void btn_clicked(object sender, EventArgs e)
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

                this.devgrid.BatchBegin();

                var dataGridItemsSource = this.devgrid.ItemsSource;
                if (dataGridItemsSource == null)
                {
                    this.devgrid.ItemsSource = new ObservableCollection<InvoiceItem>();
                    dataGridItemsSource = this.devgrid.ItemsSource;
                }

                var invoiceItems = ((ObservableCollection<InvoiceItem>)dataGridItemsSource);


                if (invoiceItems.Any(d => d.ItemName == result.Text))
                {

                    for (int i = 0; i < invoiceItems.Count; i++)
                    {
                        if (invoiceItems[i].ItemName == result.Text)
                        {
                            invoiceItems[i].Quantity = invoiceItems[i].Quantity + 1;
                            invoiceItems[i].TotalPrice = (invoiceItems[i].Quantity * invoiceItems[i].UnitPrice);
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

                    invoiceItems.Add(invoiceItem);

                }

                this.devgrid.ItemsSource = invoiceItems;

                this.devgrid.RefreshData();

                this.devgrid.BatchCommit();

            }
        }     

        void OnSwipeButtonClick(object sender, SwipeButtonEventArgs e)
        {
            if (e.ButtonInfo.ButtonName == "DeleteButton")
            {
                //DisplayAlert("Alert from " + e.ButtonInfo.Caption, "Delete ", "OK");
                invoiceViewModel.DeleteLine(e.SourceRowIndex);
                //devgrid.DeleteRow(e.RowHandle);
            }
        }


        void OnCustomizeCell(CustomizeCellEventArgs e)
        {
            if (e.FieldName == "TotalPrice" && !e.IsSelected)
            {
                decimal total = Convert.ToDecimal(e.Value.ToString());
                if (total < 850)
                {
                    e.ForeColor = Color.Red;
                }
                else if (total > 1400)
                {
                    e.ForeColor = Color.Green;
                }

                e.Handled = true;
            }
        }

        private void Row_Edited(object sender, RowEditingEventArgs e)
        {
            if (e.Action == EditingRowAction.Apply)
            {
                var rowData = this.devgrid.GetRow(e.RowHandle);
                var tempQuantity = rowData.GetFieldValue("Quantity");
                var tempUnitPrice = rowData.GetFieldValue("UnitPrice");
                if (tempUnitPrice != null && tempQuantity != null)
                {
                    var unitPrice = decimal.Parse(tempUnitPrice.ToString());
                    var quantity = decimal.Parse(tempQuantity.ToString());
                    var devgridItemsSource = (ObservableCollection<InvoiceItem>)this.devgrid.ItemsSource;
                    for (int i = 0; i < devgridItemsSource.Count; i++)
                    {
                        var item = rowData.GetFieldValue("ItemName");
                        if (devgridItemsSource[i].ItemName == item.ToString())
                        {
                            devgridItemsSource[i].TotalPrice = unitPrice * quantity;
                            break;

                        }
                    }

                    this.devgrid.ItemsSource = devgridItemsSource;
                }

            }
        }

        private async void Invoice_PaymentClicked(object sender, EventArgs e)
        {
            if (paymentsetting == 1)
            {
                //{
            //    var paymentPage = new PaymentPage();
            //    paymentPage.BindingContext = ((InvoiceViewModel)this.BindingContext);
            //    await  Navigation.PushAsync(paymentPage);
            var paymentPage = new PaymentPage((InvoiceViewModel)this.BindingContext);
            await Navigation.PushAsync(paymentPage);
            }
            else
            {
                // await Navigation.PushAsync(new PaymentQRCodePage((BindingList<InvoiceItem>)this.devgrid.ItemsSource));
            }

        }

        private void Devgrid_RowTap(object sender, RowTapEventArgs e)
        {
            if(e.FieldName == "IncButton")
            {
                this.devgrid.BatchBegin();

                invoiceViewModel.IncQuantity(e.RowHandle);
                devgrid.RefreshData();
                this.devgrid.BatchCommit();

            }
            else if(e.FieldName == "DecButton")
            {
                this.devgrid.BatchBegin();

                invoiceViewModel.DecQuantity(e.RowHandle);
                
                devgrid.RefreshData();
                this.devgrid.BatchCommit();

            }
        }
    }
}