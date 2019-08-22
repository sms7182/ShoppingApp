using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.ViewModels;
using Xamarin.Forms.Internals;

namespace ShoppingBusinessObject
{
    public class InvoiceItem:ModelObject
    {
        private bool select;
        public bool Select
        {
            get { return @select; }
            set
            {
                if (@select != value)
                {
                    @select = value;
                    RaisePropertyChanged("Select");
                }
            }
        }
        private string itemName;
        public string ItemName
        {
            get { return itemName;} set{
                if (itemName != value)
                {
                    itemName = value;
                    RaisePropertyChanged("ItemName");
                }} }
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }

        private decimal unitPrice;
        public decimal UnitPrice {
            get { return  unitPrice; }
            set
            {
                if (unitPrice != value)
                {
                    unitPrice = value;
                    RaisePropertyChanged("UnitPrice");
                }
            }

        }
        private decimal quantity;
        public decimal Quantity
        {
            get { return quantity;}
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    RaisePropertyChanged("Quantity");
                }
            } }

        private string unit;

        public string Unit
        {
            get { return unit; }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    RaisePropertyChanged("Unit");
                }
            }
        }

        public decimal NetPrice { get; set; }
        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice;}
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    RaisePropertyChanged("TotalPrice");
                }
            }
        }
        public decimal IncPrice { get; set; }
        public decimal DecPrice { get; set; }
        public string ItemNumber { get; set; }
    }
}
