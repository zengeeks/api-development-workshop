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
    [OpenApiOperation(operationId: nameof(GetItems), tags: new[] { "Item operations" }, Summary = "�S�Ă� Item ���擾", Description = "�o�^����Ă���S�Ă� Item ���擾���܂��BAPI �N������ id 0 �` 4 �� Item ���o�^����Ă��܂��B")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Item>), Description = "�o�^����Ă���S�Ă� Item")]
    public IActionResult GetItems([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items")] HttpRequest req)
    {
        return new OkObjectResult(Items);
    }

    [FunctionName(nameof(GetItem))]
    [OpenApiOperation(operationId: "GetItem", tags: new[] { "Item operations" }, Summary = "Item �̎擾", Description = "�w�肵�� id �� item ���擾���܂��B")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "�擾������ Item �� Id")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Item), Description = "�w�肵�� Id �� Item")]
    public IActionResult GetItem([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items/{id}")] HttpRequest req, string id)
    {
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return new NotFoundObjectResult($"id: {id} �̃A�C�e���͑��݂��܂���B");
        }

        return new OkObjectResult(item);
    }

    [FunctionName(nameof(AddItem))]
    [OpenApiOperation(operationId: "AddItem", tags: new[] { "Item operations" }, Summary = "Item ��ǉ�", Description = "name �� description ���w�肵�� Item ��ǉ����܂��Bid �� GUID �������̔Ԃ���܂��B")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ItemToAdd), Description = "�o�^���� Item �̒l", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Item), Description = "�ǉ����� Item")]
    public IActionResult AddItem([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "items")] ItemToAdd itemToAdd)
    {
        if (string.IsNullOrEmpty(itemToAdd.Name) || string.IsNullOrEmpty(itemToAdd.Category) || string.IsNullOrEmpty(itemToAdd.Description))
        {
            return new BadRequestObjectResult("name, category, description �̓��͕͂K�{�ł��B");
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
    [OpenApiOperation(operationId: "UpdateItem", tags: new[] { "Item operations" }, Summary = "Item ���X�V", Description = "Item ���X�V���܂��B")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "�X�V���� Item �� Id")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ItemToAdd), Description = "�X�V����A�C�e���� name �� description", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Item), Description = "�X�V���� Item")]
    public IActionResult UpdateItem([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "items/{id}")] Item itemToUpdate, string id)
    {
        // ���̓`�F�b�N
        if (string.IsNullOrEmpty(itemToUpdate.Name) || string.IsNullOrEmpty(itemToUpdate.Category) || string.IsNullOrEmpty(itemToUpdate.Description))
        {
            return new BadRequestObjectResult("name, category, description �̓��͕͂K�{�ł��B");
        }

        // item �̑��݃`�F�b�N
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return new NotFoundObjectResult($"id: {id} �̃A�C�e���͑��݂��܂���B");
        }

        item.Name = itemToUpdate.Name;
        item.Category = itemToUpdate.Category;
        item.Description = itemToUpdate.Description;

        return new OkObjectResult(item);
    }

    [FunctionName(nameof(DeleteItem))]
    [OpenApiOperation(operationId: "DeleteItem", tags: new[] { "Item operations" }, Summary = "Item ���폜", Description = "item ���폜���܂��B")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "�폜����A�C�e���� id")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(string), Description = "The No content response")]
    public IActionResult DeleteItem([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "items/{id}")] HttpRequest req, string id)
    {
        var item = Items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return new NotFoundObjectResult($"id: {id} �̃A�C�e���͑��݂��܂���B");
        }

        Items.Remove(item);

        return new NoContentResult();
    }

    #region Demo �p APIs

    [FunctionName(nameof(GetItemByQuery))]
    [OpenApiOperation(operationId: "GetItemByQuery", tags: new[] { "Item operations" }, Summary = "Query string �Ŏw�肵�� category ���� Item �̎擾", Description = "Query string �Ŏw�肵�� category �� item ���擾���܂��BAPI �N������ category �� bag �܂��� hat ���o�^����Ă��܂��B")]
    [OpenApiParameter(name: "category", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "�擾������ category (�f�t�H���g�ł� bag �� hat)")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Item>), Description = "�w�肵�� category �� Item")]
    public IActionResult GetItemByQuery([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items-query")] HttpRequest req)
    {
        string category = req.Query["category"];
        if (category == null)
        {
            return new BadRequestObjectResult("Query string �� category ���w�肵�Ă��������B");
        }
        var items = Items.Where(x => x.Category == category).ToList();
        if (!items.Any())
        {
            return new NotFoundObjectResult($"category: {category} �̃A�C�e���͑��݂��܂���B");
        }

        return new OkObjectResult(items);
    }

    [FunctionName(nameof(AddItemByFormData))]
    [OpenApiOperation(operationId: "AddItemByFormData", tags: new[] { "Item operations" }, Summary = "Form Data �̒l�� Item ��ǉ�", Description = "x-www-form-urlencoded �œ��͂��ꂽ name, category, description ���w�肵�� Item ��ǉ����܂��Bid �͎����̔Ԃ���܂��B")]
    [OpenApiRequestBody(contentType: "application/x-www-form-urlencoded", bodyType: typeof(ItemToAdd), Description = "�o�^����A�C�e���� name �� description", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Item), Description = "�ǉ����� Item")]
    public IActionResult AddItemByFormData([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "items-form")] HttpRequest req)
    {
        if (!req.Form.TryGetValue("name", out var name) || !req.Form.TryGetValue("category", out var category) || !req.Form.TryGetValue("description", out var description))
        {
            return new BadRequestObjectResult("name,category, description �͑S�ē��͕K�{�ł��B");
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

    #endregion Demo �p APIs
}