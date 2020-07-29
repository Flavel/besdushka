using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace Besdushka
{
    public class RoomInfo
    {
        public int roomid { get; set; }
        public string roomname { get; set; }
        public int ownerid { get; set; }
        public string description { get; set; }

    }
    public class User
    {
        public int userid;
        public string nickname;
        public string name;
    }

    public class DataBase
    {
        public class Resp
        {
            public string warnings;
        }
        public class Room
        {
            public string warnings;
            public int roomid;
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

        public int createRoom(string roomName, string description, bool privateRoom)
        {
            var formContent = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("roomname", roomName),
                new KeyValuePair<string, string>("description", description),
                new KeyValuePair<string, string>("privateroom", privateRoom? "true" : "false"),
                new KeyValuePair<string, string>("nickname", App.Current.Properties["nickname"] as string),
                new KeyValuePair<string, string>("password", App.Current.Properties["password"] as string),
            });

            var myHttpClient = new HttpClient();
            var response = myHttpClient.PostAsync("http://25.89.162.50/besdushka/createroom.php", formContent);

            var json = response.Result.Content.ReadAsStringAsync().Result;

            var resp = Newtonsoft.Json.JsonConvert.DeserializeObject<Room>(json);

            if (resp.warnings == "1")
            {
                //Не совпадает логин/пароль
                App.Current.Properties.Remove("password");
                App.Current.Properties.Remove("nickname");
                return -1;
            }
            if (resp.warnings == "2")
            {
                //Передано пустое поле
                return -2;
            }

            return resp.roomid;
        }
        public RoomInfo roomInfo(int id)
        {
            RoomInfo room = new RoomInfo();

            var formContent = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("roomid", id.ToString()),
            });

            var myHttpClient = new HttpClient();
            var response = myHttpClient.PostAsync("http://25.89.162.50/besdushka/roominfo.php", formContent);

            var json = response.Result.Content.ReadAsStringAsync().Result;

            room = Newtonsoft.Json.JsonConvert.DeserializeObject<RoomInfo>(json);
            return room;
        }
        public List<RoomInfo> RoomList()
        {
            List<RoomInfo> roomList = new List<RoomInfo>();
            var myHttpClient = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("nickname", App.Current.Properties["nickname"] as string),
                new KeyValuePair<string, string>("password", App.Current.Properties["password"] as string),
            });
            var response = myHttpClient.PostAsync("http://25.89.162.50/besdushka/roomlist.php", formContent);

            var json = response.Result.Content.ReadAsStringAsync().Result;

            roomList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoomInfo>>(json);
            return roomList;
        }
        public List<User> UsersInRoom(int id)
        {
            List<User> users = new List<User>();
            var myHttpClient = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("roomid", id.ToString()),
            });
            var response = myHttpClient.PostAsync("http://25.89.162.50/besdushka/usersinroomlist.php", formContent);

            var json = response.Result.Content.ReadAsStringAsync().Result;

            users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);

            return users;
        }
        public int addUserInRoom(int id)
        {
            
            var myHttpClient = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("nickname", App.Current.Properties["nickname"] as string),
                new KeyValuePair<string, string>("password", App.Current.Properties["password"] as string),
                new KeyValuePair<string, string>("room", id.ToString()),
            });
            var response = myHttpClient.PostAsync("http://25.89.162.50/besdushka/addusersinroom.php", formContent);

            var json = response.Result.Content.ReadAsStringAsync().Result;

            Resp resp = Newtonsoft.Json.JsonConvert.DeserializeObject<Resp>(json);

            if (resp.warnings == "0")
                return 0;
            else
                return 1;
        }

    }

}

