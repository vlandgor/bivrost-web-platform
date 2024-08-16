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
    
    public class Server
    {
        public const int MAX_SESSIONS_AMOUNT = 16;
        public const int MAX_STUDENTS_IN_SESSION = 16;

        public readonly IHubContext<SessionHub> _hubContext;
        public readonly WebSocketService webSocketService;
        
        private List<Session> sessions = new();

        public delegate void PacketHandler(Packet packet);
        public static Dictionary<int, PacketHandler> packetHandlers;

        public Server(ILogger<WebSocketService> logger)
        {
            webSocketService = new WebSocketService(logger);
        }

        public async Task Run(ServerType serverType)
        {
            InitializeServerData();
            
            switch (serverType)
            {
                case ServerType.Development:
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

        public async Task AddSession(string sessionId)
        {
            Session session = new Session(sessionId);
            
            sessions.Add(session);
        }

        public async Task AddStudent(string sessionId, string studentId, string studentName)
        {
            Session session = GetSession(sessionId);
            Student student = new Student(studentId, studentName);
            
            session.students.Add(student);
            
            // await _hubContext.Clients.All.SendAsync("CreateStudent", student.studentLocked, student.studentId,
            //     student.studentName, student.studentStatus, student.studentProgress);
        }

        public async Task LockStudent(string sessionId, string studentId)
        {
            Student student = GetStudent(sessionId, studentId);
            student.studentLocked = true;
                
            await _hubContext.Clients.All.SendAsync("LockStudent", student.studentId);
        }

        public async Task UpdateStudentProgress(string sessionId, string studentId, int progress)
        {
            Student student = GetStudent(sessionId, studentId);
            student.studentProgress = progress;
            
            await _hubContext.Clients.All.SendAsync("UpdateStudentProgress", student.studentId, student.studentProgress);
        }
        
        private void InitializeServerData()
        {
            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.lockStudent, ServerHandle.LockStudent },
                { (int)ClientPackets.updateStudentProgress, ServerHandle.UpdateStudentProgress },
            };
        }

        private Session GetSession(string sessionId)
        {
            Session session = sessions.FirstOrDefault(s => s.sessionId == sessionId);

            return session;
        }
        
        private Student GetStudent(string sessionId, string studentId)
        {
            Session session = sessions.FirstOrDefault(s => s.sessionId == sessionId);
            Student student = session.students.FirstOrDefault(s => s.studentId == studentId);

            return student;
        }
        
        public IReadOnlyList<Student> GetStudentsBySessionId(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentException("Session ID cannot be null or empty", nameof(sessionId));
            }

            var session = sessions.FirstOrDefault(s => s.sessionId == sessionId);
    
            if (session == null)
            {
                return new List<Student>().AsReadOnly(); // Return an empty read-only list if the session is not found
            }

            return session.students.AsReadOnly();
        }
    }
}