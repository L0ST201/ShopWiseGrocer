using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ShopWiseGrocer.Models;
using Xamarin.Forms;

namespace ShopWiseGrocer
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<GroceryItem> items;
        private ObservableCollection<Grouping<string, GroceryItem>> groupedItems;

        public MainPage()
        {
            InitializeComponent();

            items = new ObservableCollection<GroceryItem>();
            groupedItems = new ObservableCollection<Grouping<string, GroceryItem>>();
            GroceryListView.ItemsSource = groupedItems;
        }

        private void OnAddItem(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ItemEntry.Text) && CategoryPicker.SelectedItem != null)
            {
                var newItem = new GroceryItem(ItemEntry.Text, CategoryPicker.SelectedItem.ToString());
                items.Add(newItem);
                ItemEntry.Text = string.Empty;
                UpdateGrouping();
            }
        }

        private void OnDeleteItem(object sender, EventArgs e)
        {
            var item = (sender as ImageButton)?.BindingContext as GroceryItem; // Change this line
            if (item != null)
            {
                items.Remove(item);
                UpdateGrouping();
            }
        }

        private void OnMarkAsPurchased(object sender, CheckedChangedEventArgs e)
        {
            var item = (sender as CheckBox)?.BindingContext as GroceryItem;
            if (item != null)
            {
                item.IsPurchased = e.Value;
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // will implement logic to handle item selection here.
        }

        private void UpdateGrouping()
        {
            var sorted = from item in items
                         orderby item.Category
                         group item by item.Category into itemGroup
                         select new Grouping<string, GroceryItem>(itemGroup.Key, itemGroup);

            groupedItems = new ObservableCollection<Grouping<string, GroceryItem>>(sorted);
            GroceryListView.ItemsSource = groupedItems;
        }
    }
}
