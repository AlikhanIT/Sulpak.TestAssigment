using Asp.Versioning;

namespace Sulpak.TestAssigment.PublicApi;

internal static class VersioningExtension
{
    internal static WebApplicationBuilder ConfigureVersioning(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new ApiVersion(1, 0);
            option.ReportApiVersions = true;
            option.ApiVersionReader = ApiVersionReader.Combine(
                new MediaTypeApiVersionReader("ver"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return builder;
    }
    
    internal static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        var xmlDocsPath = Path.Combine(AppContext.BaseDirectory, typeof(Program).Assembly.GetName().Name + ".xml");
        builder.Services.AddSwaggerGen(options => { options.IncludeXmlComments(xmlDocsPath); });

        return builder;
    }
}