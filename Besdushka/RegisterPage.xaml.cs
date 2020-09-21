using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Net.Http;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Text;
using System.Net.Http.Headers;

namespace Besdushka
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            ToServer.RegisterAnsEvent += ServerAns;
        }

        async void RegButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (password.Text != confirmpassword.Text)
            {
                await DisplayAlert("Уведомление", "Пароли не совпадают)))00))00)", "СУКААААА", "БлЯТЬ!!!11!!!1!");
                return;
            }
            if (nickname.Text == "" || nickname.Text == null)
            {
                await DisplayAlert("Уведомление", "Введите никнейм", "сука");
                return;
            }
            if (justname.Text == "" || justname.Text == null)
            {
                await DisplayAlert("Уведомление", "Введите ваше имя", "сука");
                return;
            }
            if (password.Text == "" || password.Text == null)
            {
                await DisplayAlert("Уведомление", "Пароль пустой", "сука");
                return;
            }

            ToServer.Register(nickname.Text, password.Text, justname.Text);

        }

        async void ServerAns(object s, AnsEventArgs e)
        {

            if (e.s[1] == "Error")
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Ошибка", e.s[2].Trim('\0'), "OK");
                });

            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                });
            }
        }
        async void BackToLoginPage_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}

