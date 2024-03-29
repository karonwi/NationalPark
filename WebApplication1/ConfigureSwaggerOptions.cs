﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace ParkyAPI
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;
        
        public void Configure(SwaggerGenOptions options)
        {
            foreach(var desc in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = $"Parky API{desc.ApiVersion}",
                        Version = desc.ApiVersion.ToString()
                    });
            }

            var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var cmlCommentsFulPath = Path.Combine(AppContext.BaseDirectory,xmlComments);
            options.IncludeXmlComments(cmlCommentsFulPath);
        }
    }
}
