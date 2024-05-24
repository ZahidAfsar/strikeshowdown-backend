using Microsoft.EntityFrameworkCore;
using strikeshowdown_backend.Hubs;
using strikeshowdown_backend.Services;
using strikeshowdown_backend.Services.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<RecentWinnerService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddSignalR();
builder.Services.AddScoped<ChatroomService>();


var connectionString = builder.Configuration.GetConnectionString("StrikeShowdown");

builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

builder.Services.AddCors(options => options.AddPolicy("StrikePolicy", 
builder => {
    builder.WithOrigins("http://localhost:5039", "https://strikeshowdownbackend.azurewebsites.net", "http://localhost:3000", "https://full-stack-strike-showdown.vercel.app", "https://full-stack-strike-showdown-git-jayvons-branch-jayvons-projects.vercel.app/")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
}));

builder.Services.AddSingleton<SharedDB>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("StrikePolicy");

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/Chat");
});


app.MapHub<ChatHub>("/Chat");


app.Run();

