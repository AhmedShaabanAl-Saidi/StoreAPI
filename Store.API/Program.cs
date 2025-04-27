using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.Abstractions;
using Services.MappingProfiles;
using StackExchange.Redis;
using Store.API.Factories;
using Store.API.Middlewares;

namespace Store.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(
                _=> ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"))
            );

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Services.ServiceManager).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidtionErrorRespose;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await SeedDbAsync(app);

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        // SeedDb
        static async Task SeedDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            await dbInitializer.InitializeAsync();
        }
    }
}
