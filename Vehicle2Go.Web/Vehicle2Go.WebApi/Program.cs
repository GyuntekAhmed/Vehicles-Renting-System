namespace Vehicle2Go.WebApi
{
    using Microsoft.EntityFrameworkCore;
    using Services.Data.Interfaces;
    using Web.Infrastructure.Extensions;

    using Services.Data;
    using Data;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<Vehicle2GoDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddApplicationServices(typeof(ICarService));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("Vehicle2Go", policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins("https://localhost:7107")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

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

            app.UseCors("Vehicle2Go");

            app.Run();
        }
    }
}