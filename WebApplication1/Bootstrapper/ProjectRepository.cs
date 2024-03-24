using Npgsql;
using WebApplication1.Models;

namespace WebApplication1.Bootstrapper;

public static class ProjectRepository
{
    private const string GET_PROJECTS_LIST_DATA = "Host=amsterdam.cvw4k2esyfrl.us-east-2.rds.amazonaws.com;Database=init_db;Username=root;Password=pass4root;";
    
    public static List<Project?> Projects { get; private set; } = new();
    
    public static void LoadProjects()
    {
        // List<Project?> projects = new List<Project?>();
        //
        // using (var connection = new NpgsqlConnection(GET_PROJECTS_LIST_DATA))
        // {
        //     connection.Open();
        //
        //     var sql = "SELECT id, fullname, shortname FROM projects";
        //     using (var command = new NpgsqlCommand(sql, connection))
        //     {
        //         using (var reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 var project = new Project
        //                 {
        //                     Id = reader.GetInt32(0),
        //                     FullName = reader.GetString(1),
        //                     ShortName = reader.GetString(2)
        //                 };
        //                 projects.Add(project);
        //             }
        //         }
        //     }
        // }
        //
        // Projects = projects;
    }

    // public static void LoadTestProjects()
    // {
    //     List<Project> projects = new List<Project>
    //     {
    //         new Project("p1", "Project 1", "P1", new List<Session>()
    //         {
    //             new Session("s1", "Session 1", true, "23.12.3213", 18, 34),
    //             new Session("s2", "Session 2", false, "12.12.2121", 17, 53),
    //             new Session("s3", "Session 3", true, "04.04.2424", 21, 32)
    //         }),
    //         new Project("p2", "Project 2", "P2", new List<Session>()
    //         {
    //             new Session("s4", "Session 4", true, "03.12.2001", 8, 12),
    //             new Session("s5", "Session 5", true, "03.12.2001", 11, 32),
    //             new Session("s6", "Session 6", true, "04.04.2424", 12, 41)
    //         }),
    //         new Project("p3", "Project 3", "P3", new List<Session>()
    //         {
    //             new Session("s7", "Session 7", true, "23.12.3213", 11, 63),
    //             new Session("s8", "Session 8", true, "12.12.2121", 19, 62),
    //             new Session("s9", "Session 9", true, "04.04.2424", 22, 21)
    //         })
    //     };
    //
    //     Projects = projects;
    // }
    //
    // public static Project? GetProjectById(string id)
    // {
    //     Project? project = Projects.FirstOrDefault(p => p.Id == id);
    //     
    //     return project;
    // }
    //
    // public static Session? GetSessionById(string sessionId)
    // {
    //     foreach (var project in Projects)
    //     {
    //         foreach (var session in project.Sessions)
    //         {
    //             if (session.Id == sessionId)
    //             {
    //                 return session;
    //             }
    //         }
    //     }
    //     return null;
    // }
}