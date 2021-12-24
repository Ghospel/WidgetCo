using Azure.Data.Tables;
using DAL;
using WidgetAndCo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOptions();

builder.Services.AddScoped<IQueueService, QueueService>();

builder.Services.AddSingleton<TableClient>(new TableClient(builder.Configuration.GetConnectionString("CosmosTableApi"), "WeatherData"));

builder.Services.AddSingleton<TablesService>();

//builder.Services.AddDbContext<OrderContext>(opt =>
//                                   opt.UseCosmos("AccountEndpoint=https://clouddatabases.documents.azure.com:443/;AccountKey=Vl3li8iKwoRoI5kzgqIaC6NuV64BRBPgD8M99AHFImKbvvtqZkJQpgmutQzu2ODaiIFrWFtpMO8FvxHvNL5Wzg==", "WidgetCo"));
//builder.Services.AddDbContext<ProductContext>(opt =>
//                                   opt.UseCosmos("AccountEndpoint=https://clouddatabases.documents.azure.com:443/;AccountKey=Vl3li8iKwoRoI5kzgqIaC6NuV64BRBPgD8M99AHFImKbvvtqZkJQpgmutQzu2ODaiIFrWFtpMO8FvxHvNL5Wzg==", "WidgetCo"));


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
