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
        }
        public class Resp
        {
            public string warnings;
        }
        async void LoginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var formContent = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("nickname", nickname.Text),
                new KeyValuePair<string, string>("password", password.Text),
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync("http://25.89.162.50/besdushka/login.php", formContent);

            var json = await response.Content.ReadAsStringAsync();

            var resp = Newtonsoft.Json.JsonConvert.DeserializeObject<Resp>(json);
            if(resp.warnings == "0")
            {
                App.Current.Properties.Add("nickname", nickname.Text);
                App.Current.Properties.Add("password", password.Text);
                
                await Navigation.PopModalAsync(true);
            } else
            {
                await DisplayAlert("Уведомление", "Неправильный никнейм или пароль", "сука");
            }


        }
        async void RegisterButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }
    }
}
