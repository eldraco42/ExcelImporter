using NSwag;
using ExcelImporter;
using OfficeOpenXml;

// use EPPlus non-commercial license
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

// initialization
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiDocument(options => {
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "Excel Importer",
            Description = "An ASP.NET Core Web API for importing excel files into a data structure.",
        };
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
