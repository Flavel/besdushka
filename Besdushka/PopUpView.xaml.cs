using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Besdushka
{
    public partial class PopUpView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopUpView()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0);
            this.BackgroundInputTransparent = true;
            this.Appearing += async (s, e) =>
            {
                await Task.Delay(2500);
                await PopupNavigation.Instance.RemovePageAsync(this);
            };
        }
    }
}
