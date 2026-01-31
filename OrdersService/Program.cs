using Microsoft.EntityFrameworkCore;
using OrdersService.Commands;
using OrdersService.Infrastructure;
using OrdersService.Queries;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OrdersDb"));
});


builder.Services.AddScoped<CreateOrderHandler>();
builder.Services.AddScoped<GetOrdersHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.Run();
