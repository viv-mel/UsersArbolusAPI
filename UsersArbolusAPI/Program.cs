using Polly;
using UsersArbolusAPI.Options;
using UsersArbolusAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Binding IOptions
var arbolusApiOptions = builder.Configuration.GetSection(ArbolusApiOptions.SectionName);
builder.Services.Configure<ArbolusApiOptions>(arbolusApiOptions);

// Add services to the container.
builder.Services.AddHttpClient("PollyWaitAndRetry")
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(
            3, retryNumber => TimeSpan.FromMilliseconds(1000)));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
