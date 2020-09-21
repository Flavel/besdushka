using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace Besdushka
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            ToServer.ConnectAnsEvent += connectAns;
        }

        void LoginButton_Clicked(System.Object sender, System.EventArgs e)
        {

            if (nickname.Text == null)
                nickname.Text = "";
            if (password.Text == null)
                password.Text = "";
            
            ToServer.Connect(nickname.Text, password.Text);

        }

        public async void connectAns(object sender, AnsEventArgs ansEventArgs)
        {
            if (ansEventArgs.s[1] == "Success")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopModalAsync();
                    App.Current.Properties["nickname"] = nickname.Text;
                    App.Current.Properties["password"] = password.Text;

                });
            }
            else
            {
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Ошибка", ansEventArgs.s[2].Trim('\0'), "OK");
                });
            }
        }


        
        void RegisterButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new RegisterPage());
        }
    }
}
