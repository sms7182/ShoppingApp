
using ShoppingApp.ViewModels.Contracts;
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class StoreVM
    {
        public StoreInfo Store { get; set; }
        public StoreVM(StoreInfo selectedStore)
        {
            Store = selectedStore;
        }
    }

    
}
