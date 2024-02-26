using System;
using System.Collections.Generic;
using Npgsql;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProjectRepository
{
    private readonly string connectionString = "Host=amsterdam.cvw4k2esyfrl.us-east-2.rds.amazonaws.com;Database=init_db;Username=root;Password=pass4root;";

    public List<Project> GetProjects()
    {
        var projects = new List<Project>();

        using (var connection = new NpgsqlConnection(connectionString))
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
                            Id = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            ShortName = reader.GetString(2)
                        };
                        projects.Add(project);
                    }
                }
            }
        }

        return projects;
    }
}