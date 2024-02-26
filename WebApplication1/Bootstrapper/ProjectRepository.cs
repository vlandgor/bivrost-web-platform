using Npgsql;
using WebApplication1.Models;

namespace WebApplication1.Bootstrapper;

public static class ProjectRepository
{
    private const string GET_PROJECTS_LIST_DATA = "Host=amsterdam.cvw4k2esyfrl.us-east-2.rds.amazonaws.com;Database=init_db;Username=root;Password=pass4root;";
    
    public static List<Project?> Projects { get; private set; } = new();
    
    public static void LoadProjects()
    {
        List<Project?> projects = new List<Project?>();

        using (var connection = new NpgsqlConnection(GET_PROJECTS_LIST_DATA))
        {
            connection.Open();

            var sql = "SELECT id, fullname, shortname FROM projects";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var project = new Project
                        {
                            Id = reader.GetString(0),
                            FullName = reader.GetString(1),
                            ShortName = reader.GetString(2)
                        };
                        projects.Add(project);
                    }
                }
            }
        }

        Projects = projects;
    }

    public static void LoadTestProjects()
    {
        List<Project> projects = new List<Project>
        {
            new Project { Id = "1", FullName = "Project 1", ShortName = "P1" },
            new Project { Id = "2", FullName = "Project 2", ShortName = "P2" },
            new Project { Id = "3", FullName = "Project 3", ShortName = "P3" }
        };

        Projects = projects;
    }

    public static Project? GetProjectById(string id)
    {
        Project? project = Projects.FirstOrDefault(p => p.Id == id);
        
        return project;
    }
}