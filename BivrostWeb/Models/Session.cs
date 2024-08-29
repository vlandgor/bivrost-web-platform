namespace WebApplication1.Models;

public class Session(string s_id, string s_name, bool s_status, string s_date, List<Student> s_students, int s_duration)
{
    public string s_id { get; private set; } = s_id;
    public string s_name { get; private set; } = s_name;
    public bool s_status { get; private set; } = s_status;
    public string s_date { get; private set; } = s_date;
    public List<Student> s_students { get; private set; } = s_students;
    public int s_duration { get; private set; } = s_duration;
}