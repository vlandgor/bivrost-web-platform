namespace WebApplication1.Models;

public class Session
{
    public string s_id { get; private set; }
    public string s_name { get; private set; }
    public bool s_status { get; private set; }
    public List<Student> s_students { get; private set; }
    
    public Session(string s_id, string s_name, bool s_status, List<Student> s_students)
    {
        this.s_id = s_id;
        this.s_name = s_name;
        this.s_status = s_status;
        this.s_students = s_students;
    }
}