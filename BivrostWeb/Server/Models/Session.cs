namespace BivrostWeb.Server.Models;

public class Session(string sessionName)
{
    public string sessionId;
    public string sessionName = sessionName;
    
    public Dictionary<string, Student> students = new();

    public Student? GetStudent(string studentId)
    {
        return students.GetValueOrDefault(studentId);
    }

    public void AddStudent(string studentId, Student student)
    {
        if (!students.TryAdd(studentId, student))
        {
            Console.WriteLine($"A student with the ID {studentId} already exists in this session.");
        }
    }

    public void RemoveStudent(string studentId)
    {
        students.Remove(studentId);
    }
    
}