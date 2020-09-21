using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Besdushka.CreateNewRoom;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Besdushka
{
    public partial class CreateNewLib : ContentPage
    {
        ObservableCollection<Item> items = new ObservableCollection<Item>();
        public CreateNewLib()
        {
            InitializeComponent();
            
            ItemsCollection.ItemsSource = items;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            PopupItemView ItemView = new PopupItemView(new Item("", "", new ObservableCollection<Property>()));
            ItemView.AddBtn.Clicked += (s, e1) => {
                items.Add(new Item(ItemView.name.Text, ItemView.description.Text, ItemView.propertiesObservableCollection));
                
                //PropertiesCollection
            };

            PopupNavigation.Instance.PushAsync(ItemView); 
        }
    }
}
