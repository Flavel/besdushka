using System;
using System.Collections.Generic;
using Xamarin.Forms;

using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using Besdushka.CreateNewRoom;

namespace Besdushka
{
    public partial class PopupItemView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ObservableCollection<Property> propertiesObservableCollection = new ObservableCollection<Property>();
        public PopupItemView(Item item)
        {
            InitializeComponent();
            propertiesObservableCollection = item.properties;
            this.name.Text = item.name;
            this.description.Text = item.description;
            this.properties.ItemsSource = propertiesObservableCollection; 
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        async void AddProp_Button_Clicked(object sender, EventArgs e)
        {
            PopUpAddProp prop = new PopUpAddProp(new Property("", ""));
            prop.AddPropBtn.Clicked += (s, e1) =>
            {
                propertiesObservableCollection.Add(new Property(prop.name.Text, prop.Description.Text));
            };
            await PopupNavigation.Instance.PushAsync(prop);
        }

        async void properties_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (((CollectionView)sender).SelectedItem != null)
            {
                Property property = ((CollectionView)sender).SelectedItem as Property;
                PopUpAddProp ItemPop = new PopUpAddProp(property);
                ItemPop.AddPropBtn.Clicked += (s, e1) =>
                {
                    Property newProp = new Property(ItemPop.name.Text, ItemPop.Description.Text);
                    int itemIndex = propertiesObservableCollection.IndexOf(property);
                    propertiesObservableCollection[itemIndex] = newProp;
                };
                await PopupNavigation.Instance.PushAsync(ItemPop);
            }
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
