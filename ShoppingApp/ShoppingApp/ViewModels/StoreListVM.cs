using ShoppingApp.Helpers;
using ShoppingApp.ViewModels.Commands;
using ShoppingApp.ViewModels.Contracts;
using ShoppingApp.Views;
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class StoreListVM
    {
        private StoreInfo selectedStore;

        public StoreListCommand StoreListCommand { get; set; }
        public StoreViewCommand StoreViewCommand { get; set; }
        public List<StoreInfo> Stores { get; set; }
        public StoreInfo SelectedStore
        {
            get => selectedStore; set
            {
                selectedStore = value;
                if(selectedStore==null)
                {
                    return;
                }

                StoreListCommand.Execute(selectedStore);
                selectedStore = null;
            }
        }

        public StoreListVM()
        {
            StoreListCommand = new StoreListCommand(this);
            StoreViewCommand = new StoreViewCommand(this);
            GetStores();
        }

        private async void GetStores()
        {
            Stores = await StoreDB.Read();
        }
        public async void ViewStore(Guid id)
        {            
            var viewPage = new ViewStorePage();
            var selectedStore = await StoreDB.GetStore(id);
            viewPage.BindingContext = new StoreVM(selectedStore);
            await App.Current.MainPage.Navigation.PushAsync(viewPage);
        }

        public void CreateInvoice(StoreInfo selectedStore)
        {
            //Xamarin.Forms.DataGrid.DataGridComponent.Init();
            var newInvoiceVM = new InvoiceViewModel() { Store = selectedStore };
            var createPage = new ObjectListView(newInvoiceVM);
            App.Current.MainPage.Navigation.PushAsync(createPage);
        }
    }
}
