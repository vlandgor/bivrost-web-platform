namespace BivrostWeb.Server.Models;

public class Session()
{
    public string sessionId { get; set; }
    public string sessionName { get; set; }
    
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

    public List<Student> GetStudents()
    {
        foreach (Student student in students.Values)
        {
            Console.WriteLine(student.studentId);
        }
        
        return students.Values.ToList();
    }
}