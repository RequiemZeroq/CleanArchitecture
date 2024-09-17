using UseCases;
using DataAccess;
using DataAccess.Interfaces;
using DomainServices.Implementation;
using DomainServices.Interfaces;
using Email.Implementation;
using Email.Interfaces;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper;
using WebApp.Interfaces;
using WebApp.Services;
using UseCases.Order.Commands.CreateOrder;
using ApplicationServices.Interfaces;
using ApplicationServices.Implementation;
using Delivery.Interfaces;
using Delivery.Company;
using Hangfire;
using UseCases.Order.BackgroundJobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

//Domain
builder.Services.AddScoped<IOrderDomainService, OrderDomainService>();

//Infrastructure
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddDbContext<IDbContext, AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IBackgroundJobService, BackgroundJobService>();

//Application
builder.Services.AddScoped<ISecurityService, SecurityService>();

//Frameworks
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddControllers();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly);
});
builder.Services.AddHangfire(options =>
{
    options.UseSqlServerStorage(connectionString + ";Connect Timeout=60;");
});
builder.Services.AddHangfireServer();

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

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobManager.AddOrUpdate<UpdateDeliveryStatusJob>("UpdateDeliveryStatusJob", (job) => job.ExecuteAsync(), Cron.Minutely);
}

app.Run();
