using Microsoft.EntityFrameworkCore;
using PaymentsService.Infrastructure;
using PaymentsService.Commands;
using PaymentsService.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaymentsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("PaymentsDb")));

builder.Services.AddScoped<CreatePaymentHandler>();
builder.Services.AddScoped<GetPaymentStatusHandler>();

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
app.Run();
