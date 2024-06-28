using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Queues;
using StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime;
using StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.TeamService;
using StatlerWaldorfCorp.ProximityMonitor.Events;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOptions();

builder.Services.Configure<PubnubOptionSettings>(builder.Configuration.GetSection("PubnubOptionSettings"));
builder.Services.Configure<QueueOptionSettings>(builder.Configuration.GetSection("QueueOptionSettings"));
builder.Services.Configure<AMQPOptionSettings>(builder.Configuration.GetSection("AMQPOptionSettings"));
builder.Services.Configure<TeamServiceOptionSettings>(builder.Configuration.GetSection("TeamServiceOptionSettings"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AMQPConnectionFactory>();
builder.Services.AddTransient<EventingBasicConsumer, RabbitMQEventingConsumer>();
builder.Services.AddTransient<IEventSubscriber, RabbitMQEventSubscriber>();
builder.Services.AddTransient<IEventProcessor, ProximityDetectedEventProcessor>();
builder.Services.AddTransient<ITeamServiceClient, HttpTeamServiceClient>();
builder.Services.AddRealtimeService();
builder.Services.AddSingleton<IRealtimePublisher, PubnubRealtimePublisher>();

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

var eventProcessor = app.Services.GetRequiredService<IEventProcessor>();
var pubnubOptions = app.Services.GetRequiredService<IOptions<PubnubOptionSettings>>();
var realtimePublisher = app.Services.GetRequiredService<IRealtimePublisher>();

await realtimePublisher.ValidateAsync();
await realtimePublisher.PublishAsync(pubnubOptions.Value.StartupChannel, "{'hello': 'world'}");
eventProcessor.Start();

// app.Run();   // used since no controllers needed
