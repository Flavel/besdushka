using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Besdushka.CreateNewRoom
{
    public partial class StoryView : ContentView
    {
        public StoryView()
        {
            InitializeComponent();


            WaitHeight();
        }
        public async void WaitHeight()
        {
            await Task.Run(() =>
            {
                while (this.Height == -1) { }
                Device.BeginInvokeOnMainThread(() =>
                {
                    worldDescription.HeightRequest = this.Height - label.Height;
                });
            });
        }

    }
}
