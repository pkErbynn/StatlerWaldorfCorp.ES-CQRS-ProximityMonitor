using StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Queues;
using StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.Realtime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

builder.Services.Configure<PubnubOptionSettings>(builder.Configuration.GetSection("PubnubOptionSettings"));
builder.Services.Configure<QueueOptionSettings>(builder.Configuration.GetSection("QueueOptionSettings"));
builder.Services.Configure<AMQPOptionSettings>(builder.Configuration.GetSection("AMQPOptionSettings"));

builder.Services.AddSingleton<AMQPConnectionFactory>();

// builder.Services.AddRealtimeService();
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

app.Run();
