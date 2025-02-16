
using MassTransit;
using Oder.API.Services;
using Order.Service;
using Order.Service.Services;
using ServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddHttpClient<StockService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("MicroservicesBaseUrl")["Stock"]!);
});
builder.Services.AddSingleton<ServiceBus.IBus, ServiceBus.Bus>();
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, cfg) =>
    {
        var busOptions = builder.Configuration.GetSection(nameof(BusOption)).Get<BusOption>();

        cfg.Host(new Uri(busOptions!.Url));



        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.Configure<BusOption>(builder.Configuration.GetSection(nameof(BusOption)));
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
