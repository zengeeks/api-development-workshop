using System.Text.Json;

namespace RestClientConsoleSample;

public class Program
{

    public static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();

        // GET sample
        var getResponse = await httpClient.GetAsync("https://func-apidevworkshop-zen.azurewebsites.net/api/items");
        var allItems = await getResponse.Content.ReadAsStringAsync();

        Console.WriteLine("GET result:");
        Console.WriteLine(allItems);

        // POST sample
        var itemToAdd = new Item
        {
            Name = "demo",
            Category = "console",
            Description = "Console app demo"
        };
        var content = new StringContent(JsonSerializer.Serialize(itemToAdd));

        var postResponse = await httpClient.PostAsync("https://func-apidevworkshop-zen.azurewebsites.net/api/items", content);
        var addedItem = await postResponse.Content.ReadAsStringAsync();

        Console.WriteLine("POST result:");
        Console.WriteLine(addedItem);
    }
}

public class Item
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
}