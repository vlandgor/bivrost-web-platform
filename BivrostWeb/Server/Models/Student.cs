namespace BivrostWeb.Server.Models;

public class Student(string studentName)
{
    public string studentId;
    public string studentName = studentName;
    
    public bool studentStatus;
    public bool studentLocked;
    public int studentProgress;
}