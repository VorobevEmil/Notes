using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notes.WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                var apiVersion = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"Notes API {apiVersion}",
                        Description = "A simple example ASP NET Core Web API.",
                        TermsOfService = new Uri("https://vk.com/doc644601539_617643276?hash=Ne9XR3ZJ0ktGLQ2SpM30pKP7BjxpiSfbLEcBkZuzCCX&dl=yaUjnFKe3BxrtAAInThWMhRq1Vr5Smj2j1pIXQzwuK8"),
                        Contact = new OpenApiContact()
                        {
                            Name = "Vorobev Emil",
                            Email = String.Empty,
                            Url = new Uri("https://t.me/emilvorob")
                        }
                    });

                options.AddSecurityDefinition($"AuthTokem {apiVersion}",
                    new OpenApiSecurityScheme()
                    {
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Name = "Authorization",
                        Description = "Authorization token"
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = $"AuthToken {apiVersion}",
                            }
                        },
                        new string[] {}
                    }
                });

                options.CustomOperationIds(apiDescription => 
                    apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                        ? methodInfo.Name
                        : null);
            }
        }
    }
}
