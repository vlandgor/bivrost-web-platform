using BivrostWeb.Hub;
using BivrostWeb.Server.Models;
using BivrostWeb.Server.Packets;
using BivrostWeb.Services;
using Microsoft.AspNetCore.SignalR;

namespace BivrostWeb.Server
{
    public enum ServerType
    {
        Development,
        Production
    }
    
    public class Server(IHubContext<SessionHub> sessionHub)
    {
        public const int MAX_SESSIONS_AMOUNT = 16;
        public const int MAX_STUDENTS_IN_SESSION = 16;

        private readonly WebSocketService webSocketService = new();
        
        public delegate void PacketHandler(Packet packet);
        public static Dictionary<int, PacketHandler> packetHandlers;
        
        private Dictionary<string, Session> sessions = new();

        private ServerHandle serverHandle;
        private ServerSend serverSend;

        public async Task Run(ServerType serverType)
        {
            InitializeServerData();
            
            switch (serverType)
            {
                case ServerType.Development:
                    sessions = await AwsConnectionService.GetActiveSessions();
                    break;
                case ServerType.Production:
                    break;
            }
        }

        public async Task HandleRequestAsync(HttpContext context, Func<Task> next)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                await webSocketService.HandleWebSocketAsync(context);
            }
            else
            {
                await next();
            }
        }

        public async Task AddSession(string sessionId, string sessionName)
        {
            Session session = new Session(sessionName);
            
            if (!sessions.TryAdd(sessionId, session))
            {
                Console.WriteLine($"A session with the ID {sessionId} already exists in this session.");
            }
        }

        public async Task AddStudent(string sessionId, string studentId, string studentName)
        {
            Session session = sessions.GetValueOrDefault(sessionId);
            Student student = new Student(studentName);
            
            session.AddStudent(studentId, student);
            
            await sessionHub.Clients.All.SendAsync("CreateStudent", studentId, student.studentLocked,
                student.studentName, student.studentStatus, student.studentProgress);
        }

        public async Task LockStudent(string sessionId, string studentId)
        {
            Console.WriteLine($"SessionId: {sessionId}. StudentId: {studentId}");
    
            // Retrieve the session and student
            Session session = sessions.GetValueOrDefault(sessionId);
            if (session == null)
            {
                Console.WriteLine($"Session with ID {sessionId} not found.");
                return;
            }
    
            Student student = session.GetStudent(studentId);
            if (student == null)
            {
                Console.WriteLine($"Student with ID {studentId} not found in session {sessionId}.");
                return;
            }

            // Update the student's locked status
            student.studentLocked = true;
    
            Console.WriteLine($"Student : {student.studentName} was locked");
    
            // Notify all clients about the change
            await sessionHub.Clients.All.SendAsync("LockStudent", studentId);
        }
        
        private void InitializeServerData()
        {
            serverHandle = new ServerHandle(this);
            serverSend = new ServerSend(this);
            
            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.lockStudent, serverHandle.LockStudent },
            };
        }
    }
}