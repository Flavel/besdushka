using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Besdushka.CreateNewRoom;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Besdushka
{
    public partial class PopUpAddNPCView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopUpAddNPCView()
        {
            InitializeComponent();
            
            ObservableCollection<string> Attributes = ThisCreatePage.CreatePage.Attributes;
            ObservableCollection<string> Characteristics = ThisCreatePage.CreatePage.Characteristics;
            foreach (string a in Attributes)
            {
                StackLayout AttLayout = new StackLayout();
                AttLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                AttLayout.Orientation = StackOrientation.Horizontal;
                AttLayout.Children.Add(new Label { Text = a });
                AttLayout.Children.Add(new Entry { HorizontalOptions = LayoutOptions.FillAndExpand });
                StackLayoutAddNPC.Children.Add(AttLayout);
            }
            foreach (string a in Characteristics)
            {
                StackLayout AttLayout = new StackLayout();
                AttLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                AttLayout.Orientation = StackOrientation.Horizontal;
                AttLayout.Children.Add(new Label { Text = a });
                AttLayout.Children.Add(new Entry { HorizontalOptions = LayoutOptions.FillAndExpand });
                StackLayoutAddNPC.Children.Add(AttLayout);
            }
        }
    }
}
