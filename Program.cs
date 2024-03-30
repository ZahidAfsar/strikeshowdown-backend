using Microsoft.EntityFrameworkCore;
using strikeshowdown_backend.Services;
using strikeshowdown_backend.Services.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();

var connectionString = builder.Configuration.GetConnectionString("StrikeShowdown");

builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

builder.Services.AddCors(options => options.AddPolicy("StrikePolicy", 
builder => {
    builder.WithOrigins("http://localhost:5039", "https://strikeshowdownbackend.azurewebsites.net", "http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod();
}));

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

app.Run();
