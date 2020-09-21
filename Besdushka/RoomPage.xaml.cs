using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Besdushka
{
    public class message
    {
        public string date { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public message(string date, string name, string text)
        {
            this.date = date;
            this.name = name;
            this.text = text;
        }
    }
    public partial class RoomPage : CarouselPage
    {

        bool loadChat = false;
        public bool listener = true;
        public ObservableCollection<message> ChatOCollection = new ObservableCollection<message>();
        public RoomPage(int id)
        {
            InitializeComponent();
            if (ToServer.myRooms.Contains(id))
            {
                Enter.Clicked -= Enter_Clicked;
                Enter.Clicked += Exit_Clicked;
                Enter.Text = "Выйти";
            }
            
            ToServer.GetRoomInfoAnsEvent += RoomInfoAns;
            ToServer.GetRoomInfo(id);

            ToServer.NewMessageEvent += MsgAns;
            ToServer.GetChatLogEvent += ChatLogAns;
            ChatCollection.ItemsSource = ChatOCollection;
            ChatCollection.BindingContext = ChatOCollection;

            Title = id.ToString();
            this.id = id;


            //users = new List<User>();

        }
        int id;
        //List<User> users;

        public void RoomInfoAns(object sender, AnsEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ToServer.GetRoomInfoAnsEvent -= RoomInfoAns;
                if (e.s[1] == "Error")
                {

                    DisplayAlert("Ошибка", e.s[2], "OK");
                    Page page = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    if (page is EnterPage)
                    {
                        ((EnterPage)page).roomList.Remove(((EnterPage)page).SelectedRoom);
                    }

                    await Navigation.PopAsync();
                    return;
                }
                
                Title = e.s[2];
                description.Text = e.s[3].Replace("\\n", "\n");
                await Task.Delay(1000);
                ToServer.GetChatLog(this.id, 20, 0);
                loadChat = true;

            });
        }
        bool open = true;
        public void ChatLogAns(object sender, AnsEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if(e.s[1] == "Error")
                {
                    await Task.Delay(1000);
                    
                    ToServer.GetChatLog(this.id, 20, lastIndex - 19);
                    return;
                }
                
                if (e.s.Length == 3 && e.s[1] == "Success" )
                {
                    endOfTheChat = true;
                    return;
                }
                for (int i = e.s.Length-4; i >= 2; i -= 3)
                {
                    try
                    {
                        ChatOCollection.Insert(0, new message(e.s[i], e.s[i + 1], e.s[i + 2]));
                    } catch
                    {
                        break;
                    }
                }
                loadChat = false;
                if (open)
                {
                    ChatCollection.ScrollTo(ChatOCollection.Count - 1);
                    open = false;
                    ChatCollection.Scrolled += ScrollEvent;
                }
            });
        }
        bool onBottom = true;
        public void MsgAns(object sender, AnsEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                message message = new message("", e.s[3], e.s[4]);
                ChatOCollection.Add(message);
                if (onBottom)
                    ChatCollection.ScrollTo(message);
            });
        }
        public void Enter_Clicked(System.Object sender, System.EventArgs e)
        {
            ToServer.EnterTheRoom(this.id);
            Enter.Clicked -= Enter_Clicked;
            Enter.Clicked += Exit_Clicked;
            Enter.Text = "Выйти";
        }
        public void Exit_Clicked(System.Object sender, System.EventArgs e)
        {
            ToServer.LeaveTheRoom(this.id);
            ToServer.myRooms.Remove(this.id);
            Navigation.PopAsync();
        }

        int lastIndex = 19;
        bool endOfTheChat = false;
        public void ScrollEvent(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.LastVisibleItemIndex == ChatOCollection.Count - 1)
            {
                onBottom = true;
            }
            else
            {
                onBottom = false;
            }
            if (e.FirstVisibleItemIndex == 0 && !endOfTheChat && !loadChat)
            {
                loadChat = true;
                ToServer.GetChatLog(this.id, 20, lastIndex + 1);
                lastIndex += 20;
            }
        }
        public void SendMsg_Clicked(System.Object sender, System.EventArgs e)
        {
            if (messageText.Text != "" || messageText.Text != null)
            {
                ToServer.SendRoomMsg(this.id, messageText.Text);
                messageText.Text = "";
            }
        }
    }
}
