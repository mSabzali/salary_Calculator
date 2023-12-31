﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SalaryCalculator.WebApi.Infrastructure;

namespace SalaryCalculator.FunctionalTests;

public class SalaryCalculatorScenarioBase
{
    private const string ApiUrlBase = "http://localhost:49837";

    public TestServer CreateServer()
    {
        var factory = new SalaryCalculatorApplication();
        return factory.Server;
    }

    private class SalaryCalculatorApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // services.RemoveAll<DbContextOptions<EmployeeContext>>(); 
                // services.AddDbContext<EmployeeContext>(
                //     opt => opt.UseInMemoryDatabase("InMemoryEmployeeContext")); 
                //for use In Memory EF but in get use dapper not to use
                
            });

            builder.ConfigureAppConfiguration(c => { });

            return base.CreateHost(builder);
        }
    }


    public static class Get
    {
        public static string GetUrl(string firstName, string lastName, string Date)
        {
            return $"{ApiUrlBase}/SalaryEmployee?FirstName={firstName}&LastName={lastName}&Date={Date}";
        }

        public static string GetRangeUrl(string firstName, string lastName, string fromDate, string toDate)
        {
            return $"{ApiUrlBase}/SalaryEmployee/Range?FirstName={firstName}" +
                   $"&LastName={lastName}&FromDate={fromDate}&ToDate={toDate}";
        }
    }

    public static class Put
    {
        public static string PutUrl = $"{ApiUrlBase}/SalaryEmployee";
    }

    public static class Post
    {
        public static string PostSalaryEmployee(string datatype)
        {
            return $"{ApiUrlBase}/SalaryEmployee/{datatype}/SalaryEmployee";
        }
    }

    public static class Delete
    {
        public static string DeleteUrl = $@"{ApiUrlBase}/SalaryEmployee/{{0}}";
    }
}