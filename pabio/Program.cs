using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.OpenApi.Models;
using pabio;
using pabio.Models;
using pabio.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "pabio App", Version = "v1" }));

// Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("Endpoint=https://pabio-config.azconfig.io;Id=I8Y6;Secret=6H1lD9Q2KMIW7kl8EtrfNy17wUSHoH5IoBKE5ZLnIp50VvgMJf3OJQQJ99ALAC5RqLJuukI8AAACAZAC4dos")
           .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential())); // Optional: If using Key Vault
});

// Database
string defaultConnection = builder.Configuration["DefaultConnection"]!;
builder.Services.AddDbContext<PabioDbContext>(options => options.UseSqlServer(defaultConnection));

// Application Insights
builder.Logging.ClearProviders(); // Optional: Clear default logging providers
string applicationInsightsConnection = builder.Configuration["PabioApplicationInsightsConnection"]!;
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = applicationInsightsConnection;
});
builder.Logging.AddApplicationInsights();
builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information); // Default log level
builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning); // Microsoft logs
builder.Logging.AddConsole();

// Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<PabioDbContext>();

// Application services
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<ChatGptService>();

// Policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(
        Constants.Policies.GLOBAL_ADMIN,
        policyBuilder => policyBuilder
            .RequireClaim("IsGlobalAdmin"));

// Build Application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
