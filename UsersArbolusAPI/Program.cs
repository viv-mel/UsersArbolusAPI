using Asp.Versioning;
using Microsoft.AspNetCore.Diagnostics;
using Polly;
using UsersArbolusAPI.Options;
using UsersArbolusAPI.Services;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

// Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

// Binding IOptions
var arbolusApiOptions = builder.Configuration.GetSection(ArbolusApiOptions.SectionName);
builder.Services.Configure<ArbolusApiOptions>(arbolusApiOptions);

// Configuring HttpClients
var arbolusBaseAddress = arbolusApiOptions.GetValue<string>(nameof(ArbolusApiOptions.ApiBaseUrl));
builder.Services.AddHttpClient(ArbolusApiOptions.SectionName,
    client =>
    {
        client.BaseAddress = !string.IsNullOrWhiteSpace(arbolusBaseAddress) ? new Uri(arbolusBaseAddress) : default;
    })
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(1000)));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = Text.Plain;

            await context.Response.WriteAsync("An exception was thrown.");

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is HttpRequestException)
            {
                await context.Response.WriteAsync("The http conexion failed.");
            }

            await context.Response.WriteAsync($"Please, contact the technical team, sharing the following trace id: {context.TraceIdentifier}.");
        });
    });
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
