using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using pabio;
using pabio.Data;
using pabio.Models;
using pabio.Services;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "pabio App", Version = "v1" }));

builder.Services.AddLocalization();

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("Endpoint=https://pabio-config.azconfig.io;Id=I8Y6;Secret=6H1lD9Q2KMIW7kl8EtrfNy17wUSHoH5IoBKE5ZLnIp50VvgMJf3OJQQJ99ALAC5RqLJuukI8AAACAZAC4dos")
           .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential())); // Optional: If using Key Vault
});

string defaultConnection = builder.Configuration["DefaultConnection"]!;
builder.Services.AddDbContext<PabioDbContext>(options => options.UseSqlServer(defaultConnection));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<PabioDbContext>();

builder.Services.AddScoped<EventService>();

// Policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(
        Constants.Policies.GLOBAL_ADMIN,
        policyBuilder => policyBuilder
            .RequireClaim("IsGlobalAdmin"));


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
