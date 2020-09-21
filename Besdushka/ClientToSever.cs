using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Besdushka
{
    public class AnsEventArgs : EventArgs
    {
        public string[] s { get; set; }
    }

    public class room
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int worldID { get; set; }
        public room(int id, string name, string description, int worldID)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.worldID = worldID;
        }
    }
    public static class ToServer
    {
        public static List<int> myRooms = new List<int>();
        public static string ip = "25.89.162.50";

        public static int port;
        public static IPEndPoint udpEndPoint;
        public static Socket udpSocket;
        public static IPEndPoint serverEndPoint;

        public static void Bind()
        {
            port = 8090;
            
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverEndPoint = new IPEndPoint(IPAddress.Parse("25.50.17.227"), 8080);
            while (true)
            {
                try
                {
                    udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                    udpSocket.Bind(udpEndPoint);
                    break;
                }
                catch
                {
                    port++;
                }

            }
        }
        public static void ConnectionClose()
        {
            udpSocket.Close();
        }

        public static event EventHandler<AnsEventArgs> CreateRoomAnsEvent;
        public static event EventHandler<AnsEventArgs> EnterTheRoomAnsEvent;
        public static event EventHandler<AnsEventArgs> GetRoomInfoAnsEvent;
        public static event EventHandler<AnsEventArgs> GetRoomListAnsEvent;
        public static event EventHandler<AnsEventArgs> LeaveTheRoomAnsEvent;
        public static event EventHandler<AnsEventArgs> RegisterAnsEvent;
        public static event EventHandler<AnsEventArgs> ConnectAnsEvent;
        public static event EventHandler<AnsEventArgs> NewMessageEvent;
        public static event EventHandler<AnsEventArgs> GetChatLogEvent;

        public static void msgListener()
        {

            var buffer = new byte[4096];
            var size = 0;
            StringBuilder data = new StringBuilder();

            EndPoint senderEndPoint = ToServer.serverEndPoint;
            while (true)
            {
                data.Clear();
                buffer = new byte[4096];
                do
                {
                    size = ToServer.udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer), 0, size);
                }
                while (ToServer.udpSocket.Available > 0);
                string[] ans = data.ToString().Split('\n');
                AnsEventArgs e = new AnsEventArgs();
                e.s = ans;
                Console.WriteLine(data);
                switch (ans[0])
                {
                    case "CreateRoomAns":
                        CreateRoomAnsEvent?.Invoke(null, e);
                        break;
                    case "EnterTheRoomAns":
                        EnterTheRoomAnsEvent?.Invoke(null, e);
                        break;
                    case "GetRoomInfoAns":
                        GetRoomInfoAnsEvent?.Invoke(null, e);
                        break;
                    case "GetRoomListAns":
                        GetRoomListAnsEvent?.Invoke(null, e);
                        break;
                    case "LeaveTheRoomAns":
                        LeaveTheRoomAnsEvent?.Invoke(null, e);
                        break;
                    case "RegisterAns":
                        RegisterAnsEvent?.Invoke(null, e);
                        break;
                    case "ConnectAns":
                        for (int i = 3; i < ans.Length; i++)
                            myRooms.Add(int.Parse(ans[i]));

                        ConnectAnsEvent?.Invoke(null, e);
                        break;
                    case "GetChatLogAns":
                        GetChatLogEvent?.Invoke(null, e);
                        break;
                    case "NewMessage":
                        NewMessageEvent?.Invoke(null, e);
                        break;
                }
            }
        }

        public static void msgListenerAsync()
        {
            Task.Run(() => msgListener());
        }

        public static void Connect(string login, string password)
        {
            udpSocket.SendTo(Encoding.UTF8.GetBytes("Connect\n" + login + '\n' + password), serverEndPoint);
        }

        public static void Register(string login, string password, string username)
        {
            udpSocket.SendTo(Encoding.UTF8.GetBytes("Register\n" + login + '\n' + password + '\n' + username), serverEndPoint);

        }

        public static void CreateRoom(string roomname, string description)
        {
            description = description.Replace("\n", "\\n");
            udpSocket.SendTo(Encoding.UTF8.GetBytes("CreateRoom\n" + roomname + '\n' + description), serverEndPoint);
        }

        public static void EnterTheRoom(int roomId)
        {
            udpSocket.SendTo(Encoding.UTF8.GetBytes("EnterTheRoom\n" + roomId), serverEndPoint);


        }

        public static void LeaveTheRoom(int roomId)
        {
            //TODO
            udpSocket.SendTo(Encoding.UTF8.GetBytes("LeaveTheRoom\n" + roomId), serverEndPoint);
        }

        public static void LogOut()
        {
            //TODO
            udpSocket.SendTo(Encoding.UTF8.GetBytes("LogOut"), serverEndPoint);
        }

        public static void IsSearchingRooms()
        {
            //TODO
            udpSocket.SendTo(Encoding.UTF8.GetBytes("IsSearchingRooms"), serverEndPoint);
        }

        public static void GetRoomInfo(int roomId)
        {
            udpSocket.SendTo(Encoding.UTF8.GetBytes("GetRoomInfo\n" + roomId.ToString()), serverEndPoint);
        }

        public static void GetRoomList(int count, int start = 0)
        {
            udpSocket.SendTo(Encoding.UTF8.GetBytes("GetRoomList\n" + count.ToString() + '\n' + start.ToString()), serverEndPoint);

        }
        public static void SendRoomMsg(int id, string msg)
        {
            msg = msg.Replace("\n", "\\n");
            udpSocket.SendTo(Encoding.UTF8.GetBytes("SendRoomMsg\n" + id.ToString() + '\n' + msg), serverEndPoint);
        }
        public static void GetChatLog(int id, int count, int offset)
        {
            udpSocket.SendTo(Encoding.UTF8.GetBytes("GetChatLog\n" + id.ToString() + '\n' + count.ToString() + '\n' + offset.ToString() + '\n'), serverEndPoint);
        }
    }
}
