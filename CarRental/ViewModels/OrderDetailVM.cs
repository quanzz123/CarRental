﻿namespace CarRental.ViewModels
{
    public class OrderDetailVM
    {
        public int OrderDetailID { get; set; }
        public string CarName { get; set; }
        public string Image {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price; 
    }
}