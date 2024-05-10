namespace WebApplication1.Models;

public class Student
{
    public string st_id { get; set; }
    public string st_name { get; set; }
    public int st_progress { get; set; }
    public bool st_status { get; set; }
    
    public Student(string st_id, string st_name, int st_progress, bool st_status)
    {
        this.st_id = st_id;
        this.st_name = st_name;
        this.st_progress = st_progress;
        this.st_status = st_status;
    }
}