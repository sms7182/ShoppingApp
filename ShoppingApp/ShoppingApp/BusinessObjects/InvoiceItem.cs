﻿using System;
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
        public InvoiceItem()
        {
            Id=Guid.NewGuid();
            
        }
        public Guid Id { get; set; }

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
                }}
        }


        private string itemNumber;
        public string ItemNumber
        {
            get { return itemNumber; }
            set
            {
                if (itemNumber != value)
                {
                    itemNumber = value;
                    RaisePropertyChanged("ItemNumber");
                }
            }
        }

        public Guid ItemId { get; set; }

        public string Code { get; set; }
        public DateTime CreationDate { get; set; }

        private double unitPrice;
        public double UnitPrice {
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


        private double quantity;
        public double Quantity
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

        public double NetPrice { get; set; }
        private double totalPrice;
        public double TotalPrice
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
        public double IncPrice { get; set; }
        public double DecPrice { get; set; }
       
    }
}
