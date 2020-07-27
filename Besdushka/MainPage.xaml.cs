using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            object name;
            if (!App.Current.Properties.TryGetValue("nickname", out name))
            {
                App.Current.Properties["nickname"] = "";
            }

            InitializeComponent();

            if (App.Current.Properties["nickname"] as string == "")
            {
                Navigation.PushModalAsync(new LoginPage());
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
            await Navigation.PushModalAsync(new LoginPage());

        }

        async void RegistrationButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
