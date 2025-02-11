
using Broker;
using Caching;
using Microsoft.EntityFrameworkCore;
using Order.Application;
using Order.Application.Order;
using Order.Repository;
using ServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IBusService,BusService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<ICacheService,CacheService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("OrderDb");
});
builder.Services.AddMemoryCache();

//builder.Services.AddMassTransit(config => 
//{
//    config.UsingRabbitMq((context, cfg) =>
//    {
//        var busOptions = builder.Configuration.GetSection(nameof(BusOption)).Get<BusOption>();

//        cfg.Host(new Uri(busOptions!.Url));



//        cfg.ConfigureEndpoints(context);
//    });
//});

builder.Services.AddScoped<IOrderService, OrderService>();

//builder.Services.Configure<BusOption>(builder.Configuration.GetSection(nameof(BusOption)));
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
