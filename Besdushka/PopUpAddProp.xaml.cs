using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Besdushka.CreateNewRoom;

namespace Besdushka
{
    public partial class PopUpAddProp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopUpAddProp(Property property)
        {
            InitializeComponent();
            name.Text = property.name;
            Description.Text = property.description;
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
