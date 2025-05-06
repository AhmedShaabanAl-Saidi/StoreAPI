using Store.API.Extensions;
using Store.API.Middlewares;

namespace Store.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastureServices(builder.Configuration);
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddPresentationServices();

            var app = builder.Build();

            await app.SeedDbAsync();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
