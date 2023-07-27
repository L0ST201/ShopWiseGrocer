using System;
using Xamarin.Forms;

namespace ShopWiseGrocer.Models
{
    public class GroceryItem
    {
        public string Name { get; set; }
        public bool IsPurchased { get; set; }
        public string Category { get; set; }
        public string GroupName => Category; // This will be used for grouping in the ListView
        public Color Color { get; set; }
        public double TextSize { get; set; }  // Added TextSize property

        public GroceryItem(string name, string category)
        {
            Name = name;
            Category = category;
            IsPurchased = false;
            TextSize = 20.0;  // Initialize TextSize to some default value
        }
    }
}
