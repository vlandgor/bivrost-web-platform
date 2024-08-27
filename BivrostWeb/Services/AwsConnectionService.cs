using System.Text;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace BivrostWeb.Services;

public static class AwsConnectionService
{
    public static async Task<List<Project>> GetProjectsList(string email)
    {
        string apiUrl = $"https://0wiig3gn2b.execute-api.us-east-2.amazonaws.com/dev?email={email}";
    
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON to List<Project>
                    List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(json);

                    return projects;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    return null; // Or throw an exception if necessary
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null; // Or throw an exception if necessary
        }
    }
    
    public static async Task<bool> AddNewSession(string projectId, Session newSession)
    {
        string apiUrl = $"https://2y80gcjdha.execute-api.us-east-2.amazonaws.com/dev/";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(new { projectId, sessionData = newSession });
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to add new session. Status code: {response.StatusCode}");
                    
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public static async Task<Project> GetProject(string projectId)
    {
        string apiUrl = $"https://iedvj6v5gc.execute-api.us-east-2.amazonaws.com/dev?id={projectId}";
    
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON to List<Project>
                    Project sessions = JsonConvert.DeserializeObject<Project>(json);

                    return sessions;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    return null; // Or throw an exception if necessary
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null; // Or throw an exception if necessary
        }
    }
    
    public static async Task<Session> GetSession(string projectId, string sessionId)
    {
        string apiUrl = $"https://0se6xxit83.execute-api.us-east-2.amazonaws.com/dev?id={projectId}&s_id={sessionId}";
    
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON to List<Project>
                    Session sessions = JsonConvert.DeserializeObject<Session>(json);

                    return sessions;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    return null; // Or throw an exception if necessary
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null; // Or throw an exception if necessary
        }
    }
    
    public static async Task<Dictionary<string, Server.Models.Session>> GetActiveSessions()
    {
        string apiUrl = $"https://bvrkm0qogg.execute-api.us-east-2.amazonaws.com/dev";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Status success");

                    string json = await response.Content.ReadAsStringAsync();

                    // Extract the body field from the JSON object
                    var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
                    string body = jsonObject.body;

                    if (string.IsNullOrEmpty(body))
                    {
                        Console.WriteLine("The 'body' field is null or empty.");
                        return null; // Handle this case appropriately
                    }

                    // Deserialize the body to List<Session>
                    List<Server.Models.Session> sessionsList = JsonConvert.DeserializeObject<List<Server.Models.Session>>(body);

                    if (sessionsList == null)
                    {
                        Console.WriteLine("Failed to deserialize the 'body' into a List<Session>.");
                        return null; // Handle this case appropriately
                    }

                    Console.WriteLine($"Got session list");

                    // Convert List<Session> to Dictionary<string, Session>
                    Dictionary<string, Server.Models.Session> sessionsDictionary = sessionsList
                        .Where(session => session.sessionId != null) // Filter out sessions with null sessionName
                        .ToDictionary(
                            session => session.sessionId, // This should be sessionID if it exists
                            session => new Server.Models.Session(session.sessionName) // This should be sessionID if it exists
                            {
                                sessionId = session.sessionId,
                                students = session.students != null ? session.students
                                    .Where(student => student.Key != null) // Filter out students with null keys (studentId)
                                    .ToDictionary(
                                        student => student.Key, // This should be studentId
                                        student => new Server.Models.Student(student.Value.studentName)
                                        {
                                            studentId =  student.Value.studentId,
                                            studentStatus = student.Value.studentStatus,
                                            studentLocked = student.Value.studentLocked,
                                            studentProgress = student.Value.studentProgress
                                        }) : new Dictionary<string, Server.Models.Student>() // Handle case with no students
                            });

                    return sessionsDictionary;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    return null; // Or throw an exception if necessary
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null; // Or throw an exception if necessary
        }
    }

    public static async Task<User> GetUserData(string email)
    {
        string apiUrl = $"https://2ckins4os0.execute-api.us-east-2.amazonaws.com/dev?email={email}";
    
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON to List<Project>
                    User user = JsonConvert.DeserializeObject<User>(json);

                    return user;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    return null; // Or throw an exception if necessary
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null; // Or throw an exception if necessary
        }
    }
    
    public static async Task<bool> RegisterNewUser(User user)
    {
        string apiUrl = "https://baihi7wo9b.execute-api.us-east-2.amazonaws.com/dev/";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                var payload = new { userData = user };  // Wrap user in a userData object
                string jsonContent = JsonConvert.SerializeObject(payload);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to add new user. Status code: {response.StatusCode}");
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
    
    public static async Task<bool> AddNewProject(Project project)
    {
        string apiUrl = "https://bcxarzgl56.execute-api.us-east-2.amazonaws.com/dev/";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                var payload = new { projectData = project };
                string jsonContent = JsonConvert.SerializeObject(payload);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to add new user. Status code: {response.StatusCode}");
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
    
    public static async Task<bool> AddNewStudent(string projectId, string sessionId, Student newStudent)
    {
        string apiUrl = $"https://qx7t6wk1ze.execute-api.us-east-2.amazonaws.com/dev";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(new { projectId, sessionId, studentData = newStudent });
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to add new session. Status code: {response.StatusCode}");
                    
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
    
    public static async Task<bool> SendUserInvite(string email, string projectId)
    {
        string apiUrl = $"https://t1mj2r46y8.execute-api.us-east-2.amazonaws.com/dev";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonContent = JsonConvert.SerializeObject(new { email, projectId});
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to add new session. Status code: {response.StatusCode}");
                    
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
}