using employeeManagementSystem.Common;
using employeeManagementSystem.CosmosDb;
using employeeManagementSystem.Interfaces;
using employeeManagementSystem.ServiceFilters;
using employeeManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeBasicDetailService, EmployeeBasicDetailService>();
builder.Services.AddScoped<IEmployeeAdditionalDetailService, EmployeeAdditionalDetailService>();
builder.Services.AddSingleton<ICosmosDBService, CosmosDBService>();
builder.Services.AddScoped<BuildEmployeeFilter>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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
