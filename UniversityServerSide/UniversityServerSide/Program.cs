using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace UniversityServerSide
{
    class Program
    {

        private static byte[] _buffer = new byte[1024];
        private static List<Socket> _clientSocket = new List<Socket>();
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            Console.Title = "SERVER";
            SetupServer();  //starts the server
            Console.ReadLine();
        }

        private static void SetupServer()   //server init.
        {
            Console.WriteLine("Setting up the server... Please Wait");
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 6784));
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Console.WriteLine("Server is up and running!");
        }

        private static void AcceptCallback(IAsyncResult ARes)   //accept callback
        {
            Socket socket = _serverSocket.EndAccept(ARes);
            _clientSocket.Add(socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            Console.WriteLine("Client Connected");

            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static bool CheckPermission(short b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private static void ReceiveCallback(IAsyncResult ARes)  //getting requests from the client; sending response 
        {
            short permission = 0;
            int received = 0;
            Socket socket = (Socket)ARes.AsyncState;
            try
            {
                received = socket.EndReceive(ARes);
                byte[] dataBuff = new byte[received];
                Array.Copy(_buffer, dataBuff, received);
                string[] text = new string[1024];
                text = Encoding.ASCII.GetString(dataBuff).Split(';');
                Console.WriteLine("Text received: " + text[0]);

                string response = string.Empty;
                if (text.Length >= 3)
                {
                    permission = UserControl.checkSession(text);
                }
                switch (text[0])
                {
                    case "get time":
                        {
                            response = DateTime.Now.ToLongTimeString();
                            break;
                        }
                    case "add faculty":
                        {
                            if (CheckPermission(permission, 2))
                                response = DatabaseControlFaculty.AddFaculty(text);
                            else
                                response = "Invalid permissions";
                            break;
                        }
                    case "select faculty":
                        {
                            response = DatabaseControlFaculty.SelectFaculty(text);
                            break;
                        }
                    case "add profession":
                        {
                            if (CheckPermission(permission, 2))
                                response = DatabaseControlProfession.AddProfession(text);
                            else
                                response = "Invalid permissions";
                            break;
                        }
                    case "select profession":
                        {
                            response = DatabaseControlProfession.SelectProfession(text);
                            break;
                        }
                    case "add group":
                        {
                            if (CheckPermission(permission, 2))
                                response = DatabaseControlGroup.AddGroup(text);
                            else
                                response = "Invalid permissions";
                            break;
                        }
                    case "add subject":
                        {
                            if (CheckPermission(permission, 2))
                                response = DatabaseControlSubject.AddSubject(text);
                            else
                                response = "Invalid permissions";
                            break;
                        }
                    case "add professor":
                        {
                            if (CheckPermission(permission, 2))
                                response = DatabaseControlProfessor.AddProfessor(text);
                            else
                                response = "Invalid permissions";
                            break;
                        }
                    case "add student":
                        {
                            if (CheckPermission(permission, 2))
                                response = DatabaseControlStudent.AddStudent(text);
                            else
                                response = "Invalid permissions";
                            break;
                        }
                    case "add student to group":
                        {
                            if (CheckPermission(permission, 2))
                                response = DatabaseControlStudent.AddStudentToGroup(text);
                            else
                                response = "Invalid permissions";
                            break;
                        }
                    case "create gradebook":
                        {
                            if (CheckPermission(permission, 2))
                                response = DatabaseControlGroup.CreateGradeBook(text);
                            else
                                response = "Invalid permissions";
                            break;
                        }
                    case "show gradebook":
                        {
                            text[3] = DatabaseControlStudent.getStudentByUser(text);
                            response = DatabaseControlGroup.ShowGradeBook(text);
                            break;
                        }
                    case "login":
                        {
                            response = UserControl.Login(text);
                            break;
                        }
                    case "logout":
                        {
                            response = UserControl.Logout(text);
                            break;
                        }
                    case "grade":
                        {
                            if (CheckPermission(permission, 4))
                                response = DatabaseControlProfessor.GradeStudent(text);
                            else
                                response = "Invalid Permission";
                            break;
                        }
                    case "show students":
                        {
                            if (CheckPermission(permission, 0))
                            {
                                try
                                {
                                    response = string.Join(";", DatabaseControlStudent.RetrieveStudents(text));
                                }
                                catch (Exception e)
                                {
                                    response = e.Message;
                                }
                            }
                            else
                                response = "Invalid Permission";
                            break;
                        }
                    case "show professor":
                        {
                            if(CheckPermission(permission, 2))
                            {
                                response = DatabaseControlProfessor.GetProfessor(text);
                            }
                            else
                            {
                                response = "Invalid Permission";
                            }
                            break;
                        }
                    default:
                        {
                            response = "Command does not exist";
                            break;
                        }
                }
                byte[] data = Encoding.ASCII.GetBytes(response);
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client Disconnected");
            }
        }

        private static void SendCallback(IAsyncResult ARes) //send callback
        {
            Socket socket = (Socket)ARes.AsyncState;
            socket.EndSend(ARes);
        }

        internal static string RandomStringGenerator()  //generates a random string from the given dictionary
        {
            const string dict = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random(Guid.NewGuid().GetHashCode());
            StringBuilder key = new StringBuilder();
            char keyChar;
            for (int i = 0; i < 8; i++)
            {
                keyChar = dict[random.Next(0, dict.Length)];
                key.Append(keyChar);
            }
            return key.ToString();
        }

    }
}
