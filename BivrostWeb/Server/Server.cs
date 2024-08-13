using BivrostWeb.Hub;
using BivrostWeb.Server.Models;
using BivrostWeb.Server.Packets;
using BivrostWeb.Services;
using Microsoft.AspNetCore.SignalR;

namespace BivrostWeb.Server
{
    public class Server : IDisposable
    {
        public const int MAX_SESSIONS_AMOUNT = 16;
        public const int MAX_STUDENTS_IN_SESSION = 16;

        private WebSocketService webSocketService;
        private readonly IHubContext<SessionHub> _hubContext;

        private List<Session> sessions = new();

        public delegate void PacketHandler(Packet packet);
        public static Dictionary<int, PacketHandler> packetHandlers;

        private string student1Id = Guid.NewGuid().ToString().Substring(0, 8);
        private string student2Id = Guid.NewGuid().ToString().Substring(0, 8);

        public Server(WebSocketService webSocketService, IHubContext<SessionHub> hubContext)
        {
            this.webSocketService = webSocketService;
            _hubContext = hubContext;

            ServerHandle.OnStudentLocked += HandleStudentLocked;
            ServerHandle.OnStudentProgressUpdated += HandleStudentProgressUpdated;

            InitializeServerData();
            TestSessionFlow();
        }

        private async Task TestSessionFlow()
        {
            await CreateSession("s1");
            await Task.Delay(2000);
            
            await AddStudentToSession("s1", new Student(student1Id, "Student 1"));
            await Task.Delay(2000);
            await AddStudentToSession("s1", new Student(student2Id, "Student 2"));
            await Task.Delay(2000);
            await UpdateStudentProgress("s1", student1Id, 45);
            await Task.Delay(2000);
            await UpdateStudentProgress("s1", student2Id, 45);
        }

        private async Task CreateSession(string sessionId)
        {
            Session session = new Session(sessionId);
            sessions.Add(session);
        }

        private async Task AddStudentToSession(string sessionId, Student student)
        {
            Session session = GetSession(sessionId);
            session.students.Add(student);

            await _hubContext.Clients.All.SendAsync("CreateStudent", student.studentLocked, student.studentId,
                student.studentName, student.studentStatus, student.studentProgress);
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

        #region Event Handlers
        private void HandleStudentLocked(string sessionId, string studentId)
        {
            LockStudent(sessionId, studentId);
        }

        private void HandleStudentProgressUpdated(string sessionId, string studentId, int studentProgress)
        {
            UpdateStudentProgress(sessionId, studentId, studentProgress);
        }
        
        private void InitializeServerData()
        {
            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.lockStudent, ServerHandle.LockStudent },
                { (int)ClientPackets.updateStudentProgress, ServerHandle.UpdateStudentProgress },
            };
        }
        #endregion

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
        
        public void Dispose()
        {
            ServerHandle.OnStudentLocked -= HandleStudentLocked;
            ServerHandle.OnStudentProgressUpdated += HandleStudentProgressUpdated;
        }
    }
}