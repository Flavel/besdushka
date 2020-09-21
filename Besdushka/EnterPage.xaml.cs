using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace Besdushka
{
    public partial class EnterPage : ContentPage
    {

        public ObservableCollection<room> roomList = new ObservableCollection<room>();

        public EnterPage()
        {
            InitializeComponent();
            ToServer.GetRoomListAnsEvent += getRooms;
            ToServer.GetRoomList(20);

            collection.ItemsSource = roomList;
            collection.BindingContext = roomList;
            Title = "Поиск комнаты";
            
        }
        public room SelectedRoom;
        void collection_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (((CollectionView)sender).SelectedItem != null)
            {
                SelectedRoom = ((CollectionView)sender).SelectedItem as room;
                Navigation.PushAsync(new RoomPage(SelectedRoom.id));
            }
            ((CollectionView)sender).SelectedItem = null;
            
        }

        public void getRooms(object sender, AnsEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                for (int i = 2; i < e.s.Length; i += 4)
                {
                    try
                    {
                        room room = new room(int.Parse(e.s[i]), e.s[i + 1], e.s[i + 2], int.Parse(e.s[i + 3]));
                        roomList.Add(room);
                    } catch
                    {
                        break;
                    }
                }
            });
        }
        int lastIndex = 19;
        void collection_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if(e.LastVisibleItemIndex == lastIndex)
            {
                ToServer.GetRoomList(20, lastIndex + 1);
                lastIndex += 20;   
            }
        }
    }

}
