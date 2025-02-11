using MassTransit;
using ServiceBus;
using Stock.Service.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ServiceBus.IBus, ServiceBus.Bus>();
//builder.Services.AddHostedService<OrderCreatedEventConsumerBGService>();
builder.Services.Configure<BusOption>(builder.Configuration.GetSection(nameof(BusOption)));

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderCreatedEventConsumer>();

    config.UsingRabbitMq((masstransitConfig, rabbitMqConfig) =>
    {
        rabbitMqConfig.UseMessageRetry(r => r.Interval(6, TimeSpan.FromSeconds(5)));

        rabbitMqConfig.UseInMemoryOutbox(masstransitConfig);

        var busOptions = builder.Configuration.GetSection(nameof(BusOption)).Get<BusOption>();

        rabbitMqConfig.Host(new Uri(busOptions!.Url));

        rabbitMqConfig.ReceiveEndpoint(BusConst.StockOrderCreatedEventQueueWithMassTransit, e =>
        {
            e.Consumer<OrderCreatedEventConsumer>(masstransitConfig);
        });

        //rabbitMqConfig.ConfigureEndpoints(masstransitConfig);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
