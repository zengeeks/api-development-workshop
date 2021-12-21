using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ItemApi.Models;

namespace ItemApi;

public class BlobOperations
{
    private readonly ILogger<BlobOperations> _logger;

    public BlobOperations(ILogger<BlobOperations> log)
    {
        _logger = log;
    }

    [FunctionName("csv")]
    [OpenApiOperation(operationId: "SaveCsvToBlobAsync", tags: new[] { "Blob operations" }, Summary = "CSV の保存", Description = "Request body に含まれているテキストを Blob へ保存します。")]
    [OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "保存するファイル名を入力 (拡張子は不要)")]
    [OpenApiRequestBody(contentType: "text/csv", bodyType: typeof(string), Description = "保存する CSV を入力", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "text/plain", bodyType: typeof(string), Description = "保存した csv のファイル名")]
    public async Task<IActionResult> SaveCsvToBlobAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "csv/{name}")] HttpRequest req,
        string name,
        [Blob("csv/{name}.csv", FileAccess.Write, Connection = "BlobConnectionString")] Stream output)
    {
        await req.Body.CopyToAsync(output);
        return new ObjectResult($"{name}.csv を保存しました。")
        {
            StatusCode = StatusCodes.Status201Created
        };
    }


    [FunctionName("image")]
    [OpenApiOperation(operationId: "SaveImageToBlobAsync", tags: new[] { "Blob operations" })]
    [OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
    [OpenApiParameter(name: "extension", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
    [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(MultiPartFormDataModel), Description = "アップロードする画像", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> SaveImageToBlobAsync(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "images/{name}/{extension}")] HttpRequest req,
    string name, string extension,
    [Blob("images/{name}.{extension}", FileAccess.Write, Connection = "BlobConnectionString")] Stream output)
    {
        var file = req.Form.Files[0];
        await file.CopyToAsync(output);
        return new ObjectResult($"{name}.{extension} を保存しました。")
        {
            StatusCode = StatusCodes.Status201Created
        };
    }
}