using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Core;
using SalaryCalculator.WebApi.Infrastructure;
using SalaryCalculator.WebApi.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer(); 
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ISalaryEmployee, SalaryEmployee>();
        builder.Services.AddScoped<IOverTimeCalculator, OverTimeCalculatorA>();
        builder.Services.AddScoped<IOverTimeCalculator, OverTimeCalculatorB>();
        builder.Services.AddScoped<OverTimeServiceFactory>();
        builder.Services.AddScoped<IOverTimeCalculator, OverTimeCalculatorC>();
        builder.Services.AddDbContext<EmployeeContext>(opt => opt
            .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        
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
    }
}