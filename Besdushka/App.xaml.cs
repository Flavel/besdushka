using System;
using Besdushka.CreateNewRoom;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Besdushka
{
    public static class ThisCreatePage
    {
        public static CreatePage CreatePage { get; set; }
    }
   
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ToServer.Bind();
            ToServer.msgListenerAsync();
            ToServer.NewMessageEvent += newMessage;
            MainPage = new NavigationPage(new MainPage());

        }

        public void newMessage(object sender, AnsEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                PopUpView popUp = new PopUpView();
                
                await PopupNavigation.Instance.PushAsync(new PopUpView());
            });
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
