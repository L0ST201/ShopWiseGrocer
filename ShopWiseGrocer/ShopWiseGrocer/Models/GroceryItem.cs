using System;
using Xamarin.Forms;
using System.ComponentModel;
using SQLite;

namespace ShopWiseGrocer.Models
{
    public class GroceryItem : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        private bool _isPurchased;
        public bool IsPurchased
        {
            get => _isPurchased;
            set
            {
                _isPurchased = value;
                OnPropertyChanged(nameof(IsPurchased));
            }
        }

        public string Category { get; set; }
        public string GroupName => Category;

        public string ColorString { get; set; }

        public double TextSize { get; set; }

        public GroceryItem()
        {
        }

        public GroceryItem(string name, string category)
        {
            Name = name;
            Category = category;
            IsPurchased = false;
            TextSize = 20.0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
