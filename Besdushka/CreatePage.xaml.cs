using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Besdushka
{
    public partial class CreatePage : ContentPage
    {
        public CreatePage()
        {
            InitializeComponent();
            Title = "Создание комнаты";
            roomname.Placeholder = "Комната " + App.Current.Properties["nickname"] as string;
            //
        }

        void CreateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if(roomname.Text == "" || roomname.Text == null)
            {
                roomname.Text = roomname.Placeholder;
            }
            if(description.Text == "" || description.Text == null)
            {
                description.Text = "Без описания";
            }
            DataBase db = new DataBase();
            int a = db.createRoom(roomname.Text, description.Text, privateroom.IsToggled);
            if(a >= 0)
            {
                if (db.addUserInRoom(a) == 1)
                {
                    App.Current.Properties.Remove("nickname");
                    App.Current.Properties.Remove("password");

                    DisplayAlert("Ошибка", "Неверное имя пользователя или пароль", "ОК");
                    return;
                }

                RoomPage newRoom = new RoomPage(a);
                Navigation.PushAsync(newRoom);

                
                Navigation.RemovePage(this);

                // Переход в комнату
            }
            if(a == -1)
            {
                Navigation.PopAsync();
                DisplayAlert("Уведомление об ошибке", "Неверный логин или пароль", "сука");
            }
            if(a == -2)
            {
                DisplayAlert("Уведомление об ошибке", "Передана пустая строка", "сука");
            }
        }

    }
}
