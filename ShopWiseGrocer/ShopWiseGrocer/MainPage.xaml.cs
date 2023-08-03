using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShopWiseGrocer.Models;
using ShopWiseGrocer.Services;
using SQLite;
using Xamarin.Forms;

namespace ShopWiseGrocer
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<GroceryItem> items;
        private ObservableCollection<Grouping<string, GroceryItem>> groupedItems;
        private DatabaseService _databaseService;

        public MainPage()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GroceryItems.db3");
            _databaseService = new DatabaseService(dbPath);

            items = new ObservableCollection<GroceryItem>();
            groupedItems = new ObservableCollection<Grouping<string, GroceryItem>>();
            GroceryListView.ItemsSource = groupedItems;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = LoadItems();
        }

        private async Task OnAddItem(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ItemEntry.Text) && CategoryPicker.SelectedItem != null)
            {
                var category = (Category)CategoryPicker.SelectedItem;
                var newItem = new GroceryItem(ItemEntry.Text, category.Name);
                try
                {
                    await _databaseService.SaveItemAsync(newItem);
                }
                catch (SQLiteException ex)
                {
                    await DisplayAlert("Error", "An error occurred while adding the item. Please try again later.", "OK");
                    return;
                }
                ItemEntry.Text = string.Empty;
                await LoadItems();
            }
        }

        private async Task OnDeleteItem(object sender, EventArgs e)
        {
            var item = (sender as ImageButton)?.BindingContext as GroceryItem;
            if (item != null)
            {
                try
                {
                    await _databaseService.DeleteItemAsync(item);
                }
                catch (SQLiteException ex)
                {
                    await DisplayAlert("Error", "An error occurred while deleting the item. Please try again later.", "OK");
                    return;
                }
                await LoadItems();
            }
        }

        private async Task OnMarkAsPurchased(object sender, CheckedChangedEventArgs e)
        {
            var item = (sender as CheckBox)?.BindingContext as GroceryItem;
            if (item != null)
            {
                item.IsPurchased = e.Value;
                try
                {
                    await _databaseService.UpdateItemAsync(item);
                }
                catch (SQLiteException ex)
                {
                    await DisplayAlert("Error", "An error occurred while updating the item. Please try again later.", "OK");
                    return;
                }
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            if (sender is ListView lv) lv.SelectedItem = null;
        }

        private async Task LoadItems()
        {
            try
            {
                items = new ObservableCollection<GroceryItem>(await _databaseService.GetItemsAsync());
                UpdateGrouping();
            }
            catch (AggregateException ae)
            {
                ae.Handle((x) =>
                {
                    if (x is SQLiteException)
                    {
                        DisplayAlert("Error", "An error occurred while loading items. Please try again later.", "OK");
                        return true;
                    }
                    return false;
                });
            }
        }

        private void UpdateGrouping()
        {
            var categoryOrder = new List<string>
            {
                "Beverages",
                "Dairy",
                "Bakery",
                "Meat",
                "Produce",
                "Snacks",
                "Frozen",
                "Condiments",
                "Canned Goods",
                "Dry Goods",
                "Other"
            };

            var sorted = from item in items
                         let categoryIndex = categoryOrder.IndexOf(item.Category)
                         orderby categoryIndex == -1, categoryIndex
                         group item by item.Category into itemGroup
                         select new Grouping<string, GroceryItem>(itemGroup.Key, itemGroup);

            groupedItems = new ObservableCollection<Grouping<string, GroceryItem>>(sorted);
            GroceryListView.ItemsSource = groupedItems;
        }
    }
}
