using ChatServer.Hubs;
using ChatServer.Security;
using ChatServer.Services;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<ChatHubContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");
app.Run();
