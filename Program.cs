using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Redis connection
var redisHost = Environment.GetEnvironmentVariable("REDIS_CLUSTER_URLS");
if (redisHost != null)
{
    EndPointCollection endpoints = new EndPointCollection();
    foreach (string host in redisHost.Split(','))
    {
        endpoints.Add(host);
    }
    ConfigurationOptions config = new ConfigurationOptions
    {
        EndPoints = endpoints,
    };
    IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config);
    builder.Services.AddScoped(s => redis.GetDatabase());
}


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

app.UseAuthorization();

app.MapControllers();

app.Run();
