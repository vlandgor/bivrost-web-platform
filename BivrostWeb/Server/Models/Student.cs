namespace BivrostWeb.Server.Models;

public class Student
{
    public string studentId;
    public string studentName;
    public bool studentStatus;
    public bool studentLocked;
    public int studentProgress;

    public Student(string studentId, string studentName)
    {
        this.studentId = studentId;
        this.studentName = studentName;
    }
}