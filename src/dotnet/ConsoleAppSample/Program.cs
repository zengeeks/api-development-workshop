using System.Text.Json;

namespace ConsoleAppSample;

public class Program
{
    private static readonly HttpClient Client = new();

    public static async Task Main(string[] args)
    {
        await RunPostSamplesAsync();
        await RunGetSamplesAsync();
    }

    /// <summary>
    /// GET のサンプル
    /// </summary>
    /// <returns></returns>
    private static async Task RunGetSamplesAsync()
    {
        const string uriOfGetAll = "https://func-apidevworkshop-zen.azurewebsites.net/api/items";

        // GET
        var responseOfGetAll = await Client.GetAsync(uriOfGetAll);
        var allItems = await responseOfGetAll.Content.ReadAsStringAsync();

        Console.WriteLine("GetAll result:");
        Console.WriteLine(allItems);
    }

    /// <summary>
    /// POST のサンプル
    /// </summary>
    /// <returns></returns>
    private static async Task RunPostSamplesAsync()
    {
        const string uriOfPost = "https://func-apidevworkshop-zen.azurewebsites.net/api/items";
        var itemToAdd = new Item
        {
            Name = $"Item {DateTime.Now.ToLongTimeString()}",
            Description = "Description Z"
        };
        var content = new StringContent(JsonSerializer.Serialize(itemToAdd));

        // Body に json をセットして POST
        var response = await Client.PostAsync(uriOfPost, content);
        var addedItem = await response.Content.ReadAsStringAsync();

        Console.WriteLine("POST result:");
        Console.WriteLine(addedItem);
    }
}

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}