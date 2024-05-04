using BurgerRoyale.Payment.BackgroundService.Services;
using BurgerRoyale.Payment.Domain.BackgroundMessage;
using BurgerRoyale.Payment.Infrastructure.Database.Models;
using BurgerRoyale.Payment.IOC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.Configure<AWSConfiguration>
(
    options => builder.Configuration.GetSection("AWS").Bind(options)
);

builder.Services.Configure<MessageQueuesConfiguration>
(
    options => builder.Configuration.GetSection("MessageQueues").Bind(options)
);

builder.Services.AddHostedService<OrderCompletedBackgroundService>();

builder.Services.AddDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();