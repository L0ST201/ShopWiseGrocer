using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShopWiseGrocer.Models;
using ShopWiseGrocer.Services;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadItems();
        }

        private async void OnAddItem(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ItemEntry.Text) && CategoryPicker.SelectedItem != null)
            {
                var category = (Category)CategoryPicker.SelectedItem;
                var newItem = new GroceryItem(ItemEntry.Text, category.Name);
                await _databaseService.SaveItemAsync(newItem);
                ItemEntry.Text = string.Empty;
                await LoadItems();
            }
        }

        private async void OnDeleteItem(object sender, EventArgs e)
        {
            var item = (sender as ImageButton)?.BindingContext as GroceryItem;
            if (item != null)
            {
                await _databaseService.DeleteItemAsync(item);
                await LoadItems();
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
            if (e.SelectedItem == null)
                return;

            if (sender is ListView lv) lv.SelectedItem = null;

            // Place your logic to handle item selection here if any.
        }

        private async Task LoadItems()
        {
            items = new ObservableCollection<GroceryItem>(await _databaseService.GetItemsAsync());
            UpdateGrouping();
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
