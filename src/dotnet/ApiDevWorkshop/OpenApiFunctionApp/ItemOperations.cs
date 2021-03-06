using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using OpenApiFunctionApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.OpenApi.Models;

namespace OpenApiFunctionApp;

public class IemOperations
{
    private readonly ILogger<IemOperations> _logger;
    private static readonly List<Item> Items = Enumerable.Range(0, 5)
                                                .Select(x => new Item
                                                {
                                                    Id = x.ToString(),
                                                    Name = $"Item {x}",
                                                    Description = $"Description {x}",
                                                    Category = x % 2 == 0 ? "hat" : "bag"
                                                })
                                                .ToList();

    public IemOperations(ILogger<IemOperations> log)
    {
        _logger = log;
    }

    [FunctionName(nameof(GetItems))]
    [OpenApiOperation(operationId: nameof(GetItems), tags: new[] { "Item operations" }, Summary = "全ての Item を取得", Description = "登録されている全ての Item を取得します。API 起動時は id 0 〜 4 の Item が登録されています。")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Item>), Description = "登録されている全ての Item")]
    public IActionResult GetItems([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items")] HttpRequest req)
    {
        return new OkObjectResult(Items);
    }

    [FunctionName(nameof(GetItem))]
    [OpenApiOperation(operationId: "GetItem", tags: new[] { "Item operations" }, Summary = "Item の取得", Description = "指定した id の item を取得します。")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "取得したい Item の Id")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Item), Description = "指定した Id の Item")]
    public IActionResult GetItem([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items/{id}")] HttpRequest req, string id)
    {
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return new NotFoundObjectResult($"id: {id} のアイテムは存在しません。");
        }

        return new OkObjectResult(item);
    }

    [FunctionName(nameof(AddItem))]
    [OpenApiOperation(operationId: "AddItem", tags: new[] { "Item operations" }, Summary = "Item を追加", Description = "name と description を指定して Item を追加します。id は GUID が自動採番されます。")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ItemToAdd), Description = "登録する Item の値", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Item), Description = "追加した Item")]
    public IActionResult AddItem([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "items")] ItemToAdd itemToAdd)
    {
        if (string.IsNullOrEmpty(itemToAdd.Name) || string.IsNullOrEmpty(itemToAdd.Category) || string.IsNullOrEmpty(itemToAdd.Description))
        {
            return new BadRequestObjectResult("name, category, description の入力は必須です。");
        }

        var item = new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = itemToAdd.Name,
            Category = itemToAdd.Category,
            Description = itemToAdd.Description
        };
        Items.Add(item);

        return new ObjectResult(item)
        {
            StatusCode = StatusCodes.Status201Created
        };
    }

    [FunctionName(nameof(UpdateItem))]
    [OpenApiOperation(operationId: "UpdateItem", tags: new[] { "Item operations" }, Summary = "Item を更新", Description = "Item を更新します。")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "更新する Item の Id")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ItemToAdd), Description = "更新するアイテムの name と description", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Item), Description = "更新した Item")]
    public IActionResult UpdateItem([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "items/{id}")] Item itemToUpdate, string id)
    {
        // 入力チェック
        if (string.IsNullOrEmpty(itemToUpdate.Name) || string.IsNullOrEmpty(itemToUpdate.Category) || string.IsNullOrEmpty(itemToUpdate.Description))
        {
            return new BadRequestObjectResult("name, category, description の入力は必須です。");
        }

        // item の存在チェック
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return new NotFoundObjectResult($"id: {id} のアイテムは存在しません。");
        }

        item.Name = itemToUpdate.Name;
        item.Category = itemToUpdate.Category;
        item.Description = itemToUpdate.Description;

        return new OkObjectResult(item);
    }

    [FunctionName(nameof(DeleteItem))]
    [OpenApiOperation(operationId: "DeleteItem", tags: new[] { "Item operations" }, Summary = "Item を削除", Description = "item を削除します。")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "削除するアイテムの id")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(string), Description = "The No content response")]
    public IActionResult DeleteItem([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "items/{id}")] HttpRequest req, string id)
    {
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return new NotFoundObjectResult($"id: {id} のアイテムは存在しません。");
        }

        Items.Remove(item);

        return new NoContentResult();
    }

    #region Demo 用 APIs

    [FunctionName(nameof(GetItemByQuery))]
    [OpenApiOperation(operationId: "GetItemByQuery", tags: new[] { "Item operations" }, Summary = "Query string で指定した category から Item の取得", Description = "Query string で指定した category の item を取得します。API 起動時は category は bag または hat が登録されています。")]
    [OpenApiParameter(name: "category", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "取得したい category (デフォルトでは bag か hat)")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Item>), Description = "指定した category の Item")]
    public IActionResult GetItemByQuery([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items-query")] HttpRequest req)
    {
        string category = req.Query["category"];
        if (category == null)
        {
            return new BadRequestObjectResult("Query string で category を指定してください。");
        }
        var items = Items.Where(x => x.Category == category).ToList();
        if (!items.Any())
        {
            return new NotFoundObjectResult($"category: {category} のアイテムは存在しません。");
        }

        return new OkObjectResult(items);
    }

    [FunctionName(nameof(AddItemByFormData))]
    [OpenApiOperation(operationId: "AddItemByFormData", tags: new[] { "Item operations" }, Summary = "Form Data の値で Item を追加", Description = "x-www-form-urlencoded で入力された name, category, description を指定して Item を追加します。id は自動採番されます。")]
    [OpenApiRequestBody(contentType: "application/x-www-form-urlencoded", bodyType: typeof(ItemToAdd), Description = "登録するアイテムの name と description", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Item), Description = "追加した Item")]
    public IActionResult AddItemByFormData([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "items-form")] HttpRequest req)
    {
        if (!req.Form.TryGetValue("name", out var name) || !req.Form.TryGetValue("category", out var category) || !req.Form.TryGetValue("description", out var description))
        {
            return new BadRequestObjectResult("name,category, description は全て入力必須です。");
        }

        var item = new Item
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Category = category,
            Description = description
        };

        Items.Add(item);

        return new ObjectResult(item)
        {
            StatusCode = StatusCodes.Status201Created
        };
    }

    #endregion Demo 用 APIs
}