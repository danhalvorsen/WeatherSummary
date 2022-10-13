using System.Reflection;
using WeatherWebAPI.StartUp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(new List<Assembly> { Assembly.GetExecutingAssembly() });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register extension methods
builder.Services.RegisterValidators();
builder.Services.RegisterHttpClients();
builder.Services.RegisterConfigurations();
builder.Services.RegisterStrategies();
builder.Services.RegisterServices();
builder.Services.RegisterBackgroundServices();

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

//try
//{
//    app.Run();
//}
//catch (Exception e)
//{
//    var servicecollection = new ServiceCollection();
//    var serviceProvider = servicecollection.BuildServiceProvider();
//    var logger = serviceProvider.GetService<ILogger<Program>>();

//    //app.Logger.LogError("{Error}", e.Message);

//    logger?.LogError("{Error}", e);
//}

