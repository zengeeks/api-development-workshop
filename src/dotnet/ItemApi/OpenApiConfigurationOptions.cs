using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace ItemApi;

public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
    public override OpenApiInfo Info { get; set; } = new()
    {
        Version = "v1.0.0-preview",
        Title = "API Development workshop API definition",
        Description = "Powered by ZEN ARCHITECTS.",
        License = new OpenApiLicense()
        {
            Name = "Workshop Repository",
            Url = new Uri("https://github.com/zengeeks/api-development-workshop"),
        }
    };

    public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
}