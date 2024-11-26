using AspNetCoreRateLimit;
using JbhifiTechTest.Server.Services;
using JbhifiTechTest.Server.Middleware;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions();

builder.Services.AddMemoryCache();

builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("RateLimit"));

builder.Services.Configure<ClientRateLimitPolicies>(builder.Configuration.GetSection("RateLimitPolicies"));

builder.Services.AddInMemoryRateLimiting();

builder.Services.AddCors();

builder.Services.AddMvc();

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddControllers();

builder.Services.AddHttpClient("OpenWeatherMap", httpClient =>
{
    var baseAddress = builder.Configuration.GetValue<string>("OpenWeatherMap:BaseAddress");
    httpClient.BaseAddress = new Uri(baseAddress ?? throw new InvalidOperationException("BaseAddress configuration is missing"));
});

builder.Services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

app.UseCors(i => i.WithOrigins("http://localhost:7265").AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseClientRateLimiting();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
