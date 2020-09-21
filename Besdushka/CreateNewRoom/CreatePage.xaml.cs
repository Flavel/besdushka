using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Besdushka.CreateNewRoom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePage : TabbedPage
    {
        string worldDescription { get; set; }
        public ObservableCollection<Item> itemsCollection = new ObservableCollection<Item>();
        public ObservableCollection<string> Attributes = new ObservableCollection<string>();
        public ObservableCollection<string> Characteristics = new ObservableCollection<string>();
        public View worldView = new StoryView();
        public CreatePage()
        {
            InitializeComponent();
            ThisCreatePage.CreatePage = this;
            setColumnAndRows();
            grid.Children.Add(worldView);
            worldDescription = "";
        }
        void setColumnAndRows()
        {
            Grid.SetColumn(worldView, 0);
            Grid.SetRow(worldView, 0);
            Grid.SetColumnSpan(worldView, 3);
            Grid.SetRowSpan(worldView, 4);
        }
        void saveWorld()
        {
            if (worldView is StoryView)
            {
                worldDescription = ((StoryView)worldView).worldDescription.Text;
            }
            if(worldView is ItemsView)
            {
                itemsCollection = ((ItemsView)worldView).itemsCollection;
            }
            if (worldView is SpecificationsView)
            {
                Attributes = ((SpecificationsView)worldView).CharacterAttributes;
                Characteristics = ((SpecificationsView)worldView).CharacterCharacteristics;
            }
        }
        void Story(object sender, EventArgs e)
        {
            saveWorld();
            grid.Children.Remove(worldView);
            worldView = new StoryView();
            ((StoryView)worldView).worldDescription.Text = worldDescription;
            
            setColumnAndRows();
            grid.Children.Add(worldView);
        }
        void Specifications(object sender, EventArgs e)
        {

            saveWorld();
            grid.Children.Remove(worldView);
            worldView = new SpecificationsView(Attributes, Characteristics);
            setColumnAndRows();
            grid.Children.Add(worldView);
        }
        void Items(object sender, EventArgs e)
        {
            saveWorld();
            grid.Children.Remove(worldView);
            worldView = new ItemsView(itemsCollection);
            setColumnAndRows();
            grid.Children.Add(worldView);
        }
        void NPC(object sender, EventArgs e)
        {
            saveWorld();
            grid.Children.Remove(worldView);
            worldView = new NPCView();
            setColumnAndRows();
            grid.Children.Add(worldView);
        }
        void Create(object sender, EventArgs e)
        {
            ToServer.CreateRoom(roomName.Text, roomDescription.Text);
            ToServer.CreateRoomAnsEvent += pushRoomPage;

        }
        void pushRoomPage(object sendet, AnsEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (e.s[1] == "Success")
                {
                    Navigation.PushAsync(new RoomPage(int.Parse(e.s[3])));
                    ToServer.myRooms.Add(int.Parse(e.s[3]));
                    Navigation.RemovePage(this);
                }
                else
                {
                    DisplayAlert("Ошибка", e.s[2], "OK");
                }
                ToServer.CreateRoomAnsEvent -= pushRoomPage;
            });
        }
    }

}
