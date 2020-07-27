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
        public struct user
        {
            public string nickname { get; set; }
            public string name { get; set; }
            public string password { get; set; } 
        }
        public class Resp
        {
            public string warnings;
        }
        public RegisterPage()
        {
            InitializeComponent();
        }

        async void RegButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if(password.Text != confirmpassword.Text)
            {
                await DisplayAlert("Уведомление", "Пароли не совпадают)))00))00)", "СУКААААА", "БлЯТЬ!!!11!!!1!");
                return;
            }
            if(nickname.Text == "" || nickname.Text == null)
            {
                await DisplayAlert("Уведомление", "Введите никнейм", "сука");
                return;
            }
            if(justname.Text == "" || justname.Text == null)
            {
                await DisplayAlert("Уведомление", "Введите ваше имя", "сука");
                return;
            }
            if(password.Text == "" || password.Text == null)
            {
                await DisplayAlert("Уведомление", "Пароль пустой", "сука");
                return;
            }

            
            //string json = JsonConvert.SerializeObject(user);

            var formContent = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("nickname", nickname.Text),
                new KeyValuePair<string, string>("name", justname.Text),
                new KeyValuePair<string, string>("password", password.Text),
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync("http://25.89.162.50/besdushka/register.php", formContent);

            var json = await response.Content.ReadAsStringAsync();

            var resp = JsonConvert.DeserializeObject<Resp>(json);
            if(resp.warnings == "1")
            {
                await DisplayAlert("Уведомление", "Пользователь с данным никнеймом уже существует", "сука");
            } else if (resp.warnings == "0")
            {
                await DisplayAlert("Уведомление", "Регистрация прошла успешно", "О, да!");
                await Navigation.PopModalAsync(true);
            }

        }

        async void BackToLoginPage_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}
