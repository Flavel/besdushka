using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Besdushka
{
    public partial class RoomPage : CarouselPage
    {
        public RoomPage(int id)
        {
            DataBase db = new DataBase();
            //users = db.UsersInRoom(id);

            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), checkUsers);
            RoomInfo room = db.roomInfo(id);
            Title = room.roomname;
            description.Text = room.description;
            this.id = id;
            this.Disappearing += (s, e) =>
            {
                update = false;
            };
        }
        int id;
        List<User> users = new List<User>();
        bool update = true;
        private bool checkUsers()
        {
            DataBase db = new DataBase();
            List<User> check = db.UsersInRoom(this.id);

            for(int j = 0; j < check.Count; j++)
            {
                if((check.Count != users.Count) || (check[j].nickname != users[j].nickname))
                {
                    for (int i = PlayersTable.Count - 1; i >= 0; i--)
                    {
                        PlayersTable.RemoveAt(i);
                    }
                    foreach (User user in check)
                    {
                        TextCell cell = new TextCell();
                        cell.Text = user.nickname;
                        PlayersTable.Add(cell);
                    }
                    users = check;
                    break;
                }
            }
            return update;
        }
        
    }
}
