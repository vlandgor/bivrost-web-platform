using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.ServerConnection;

public static class ServerConnection
{
    public static async Task<List<Project>> GetProjectsList()
    {
        string apiUrl = "https://0wiig3gn2b.execute-api.us-east-2.amazonaws.com/dev";
    
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

    public static async Task<List<Session>> GetSessionsList(string projectId)
    {
        string apiUrl = $"https://zkofr0zqnb.execute-api.us-east-2.amazonaws.com/dev?id={projectId}";
    
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON to List<Project>
                    List<Session> sessions = JsonConvert.DeserializeObject<List<Session>>(json);

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
}