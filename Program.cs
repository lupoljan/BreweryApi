using BreweryApi.Repositories;
using BreweryApi.Services;
using BreweryApi.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IBreweryRepository, InMemoryBreweryRepository>();
builder.Services.AddScoped<IBreweryService, BreweryService>();
builder.Services.AddHttpClient<BreweryApiClient>();

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

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseApiExceptionMiddleware();
app.MapControllers();

app.Run();
