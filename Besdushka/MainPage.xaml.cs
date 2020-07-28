using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Besdushka
{
    public class DataBase
    {
        public class Resp
        {
            public string warnings;
        }

        public int checkConnection()
        {
            object a;
            if (!App.Current.Properties.TryGetValue("password", out a))
            {
                App.Current.Properties.Remove("password");
                App.Current.Properties.Remove("nickname");
                return 1;
            }
            if (!App.Current.Properties.TryGetValue("nickname", out a))
            {
                App.Current.Properties.Remove("password");
                App.Current.Properties.Remove("nickname");
                return 1;
            }

            var formContent = new FormUrlEncodedContent(new[]
             {

                new KeyValuePair<string, string>("nickname", App.Current.Properties["nickname"] as string),
                new KeyValuePair<string, string>("password", App.Current.Properties["password"] as string),
            });

            var myHttpClient = new HttpClient();
            var response = myHttpClient.PostAsync("http://25.89.162.50/besdushka/login.php", formContent);

            var json = response.Result.Content.ReadAsStringAsync().Result;

            var resp = Newtonsoft.Json.JsonConvert.DeserializeObject<Resp>(json);
            if (resp.warnings == "0")
                return 0;
            else
            {
                App.Current.Properties.Remove("password");
                App.Current.Properties.Remove("nickname");
                return 1;
            }
        }
    }

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            
            InitializeComponent();

            LoginPage loginPage = new LoginPage();
            loginPage.nickname.Unfocused += (s, e) => {
                Title = loginPage.nickname.Text;
            };

            DataBase db = new DataBase();
            if (db.checkConnection() == 1)
            {
                Navigation.PushModalAsync(loginPage);
            }

            object name;
            if (App.Current.Properties.TryGetValue("nickname", out name))
            {
                Title = App.Current.Properties["nickname"] as string;
            }
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
            loginPage.nickname.Unfocused += (s, b) => {
                Title = loginPage.nickname.Text;
            };
            await Navigation.PushModalAsync(loginPage);
        }

    }
}
