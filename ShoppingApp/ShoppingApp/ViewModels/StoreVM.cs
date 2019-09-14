
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class StoreVM
    {
        public Store Store { get; set; }
        public StoreVM(Store selectedStore)
        {
            Store = selectedStore;
        }
    }

    
}
