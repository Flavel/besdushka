using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Besdushka.CreateNewRoom;
using Xamarin.Forms;

namespace Besdushka
{

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
           

            if (!App.Current.Properties.Keys.Contains("nickname") || !App.Current.Properties.Keys.Contains("password"))
            {
                LoginPage loginPage = new LoginPage();
                loginPage.nickname.Unfocused += (s, e) =>
                {
                    Title = loginPage.nickname.Text;
                };
                Navigation.PushModalAsync(loginPage);
            }
            else
            {

                ToServer.ConnectAnsEvent += connectAns;
                ToServer.Connect(App.Current.Properties["nickname"] as string, App.Current.Properties["password"] as string);

                Title = App.Current.Properties?["nickname"] as string;
            }

        }

        public async void connectAns(object sender, AnsEventArgs ansEventArgs)
        {
            
            await Device.InvokeOnMainThreadAsync(async() =>
            {
                LoginPage loginPage = new LoginPage();
                loginPage.nickname.Unfocused += (s, e) =>
                {
                    Title = loginPage.nickname.Text;
                };

                if (ansEventArgs.s[1] != "Success")
                {
                    await Navigation.PushModalAsync(loginPage);
                }
            });

            ToServer.ConnectAnsEvent -= connectAns;
        }

        async void CreateButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CreatePage());
        }

        async void EnterButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EnterPage());
        }

        async void LoginButton_Clicked(object sender, System.EventArgs e)
        {
            object a;
            if (App.Current.Properties.TryGetValue("nickname", out a))
            {
                App.Current.Properties.Remove("nickname");
            }

            if (App.Current.Properties.TryGetValue("password", out a))
            {
                App.Current.Properties.Remove("password");
            }

            LoginPage loginPage = new LoginPage();
            loginPage.nickname.Unfocused += (s, b) =>
            {
                Title = loginPage.nickname.Text;
            };
            await Navigation.PushModalAsync(loginPage);
        }
        async void NewWorld_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CreateNewLib());
        }
    }
}
