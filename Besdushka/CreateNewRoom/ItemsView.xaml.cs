using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Besdushka.CreateNewRoom
{
    public class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public ObservableCollection<Property> properties { get; set; }
        public string propertiestext { get; set; }
        public Item(string name, string description, ObservableCollection<Property> properties)
        {
            this.name = name;
            this.description = description;
            this.properties = properties;
            this.propertiestext = "";
            foreach (Property property in properties)
            {
                this.propertiestext += property.name + '\n' + property.description + '\n';
            }
        }
    }
    public class Property
    {
        public string name { get; set; }
        public string description { get; set; }
        public Property(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

    }
    public partial class ItemsView : ContentView
    {
        public ObservableCollection<Item> itemsCollection { get; set; }
        public ItemsView(ObservableCollection<Item> collection)
        {
            InitializeComponent();
            
            itemsCollection = collection;
            this.BindingContext = this;
        }
        void Add(object sender, EventArgs e)
        {
            PopupItemView newItem = new PopupItemView(new Item("", "", new ObservableCollection<Property>()));
            newItem.AddBtn.Clicked += (s, e1) =>
            {
                Item item = new Item(newItem.name.Text, newItem.description.Text, newItem.propertiesObservableCollection);
                itemsCollection.Add(item);
            };
            PopupNavigation.Instance.PushAsync(newItem);
        }
        async void items_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (((CollectionView)sender).SelectedItem != null)
            {
                Item item = ((CollectionView)sender).SelectedItem as Item;
                PopupItemView ItemPop = new PopupItemView(item);
                ItemPop.AddBtn.Clicked += (s, e1) =>
                {
                    Item newItem = new Item(ItemPop.name.Text, ItemPop.description.Text, ItemPop.propertiesObservableCollection);
                    int itemIndex = itemsCollection.IndexOf(item);
                    itemsCollection[itemIndex] = newItem;
                };
                await PopupNavigation.Instance.PushAsync(ItemPop);
            }
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
