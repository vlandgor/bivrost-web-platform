namespace BivrostWeb.Server.Models;

public class Student(string studentName)
{
    public string studentId { get; set; }
    public string studentName { get; set; } = studentName;
    public bool studentStatus { get; set; }
    public bool studentLocked { get; set; }
    public int studentProgress { get; set; }
}