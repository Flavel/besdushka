using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Besdushka.CreateNewRoom
{
    public partial class NPCView : ContentView
    {
        public NPCView()
        {
            InitializeComponent();
        }

        void Add_Clicked(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopUpAddNPCView());
        }
    }
}
