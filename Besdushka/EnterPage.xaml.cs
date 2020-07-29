using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Besdushka
{
    public partial class EnterPage : ContentPage
    {
        public EnterPage()
        {
            DataBase db = new DataBase();
            InitializeComponent();
            Title = "Поиск комнаты";
            List<RoomInfo> roomList = db.RoomList();
            foreach(RoomInfo room in roomList)
            {
                TextCell cell = new TextCell();
                cell.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new RoomPage(room.roomid));
                };
                cell.Text += room.roomname;
                RoomTable.Add(cell);
            }
        }
    }
}
