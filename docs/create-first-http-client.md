# コンソールアプリで HTTP クライアントを作成する

このワークショップでのコーディングのメインは API の開発ですが、その前に API を呼び出す側の C# のプログラミングを簡単にご紹介します。

C# では `HttpClient` を使って HTTP プロトコルでの通信を行うことできます。以下のコードは、Visual Studio にてコンソールアプリのプロジェクトで、 `HttpClient` を使った GET と POST の API をコールする簡易なサンプルです。

> 📢 コードの詳細の解説は、ワークショップにて行います。

```csharp
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
```

`HttpClient` class の利用には昔多くの誤解があり、ネット上ではいまだにアンチパターンでのサンプルコードも見かけます。本格的に利用する際は、以下のドキュメントを参考に正しく使いましょう。

- [IHttpClientFactory を使用して回復力の高い HTTP 要求を実装する](https://docs.microsoft.com/ja-jp/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests)
- [HttpClient クラス](https://docs.microsoft.com/ja-jp/dotnet/api/system.net.http.httpclient)

<br>

[戻る](./learn-openapi.md) | [**次へ: Azure Functions での API 開発の第一歩**](./create-function-app.md)

----

[目次へ戻る](./selfpaced-handson.md)
