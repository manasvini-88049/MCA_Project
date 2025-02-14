
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sec_MasterTwo3681.Models;

namespace Sec_MasterTwo3681
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddDbContext<SecMasterTwo3681Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));

                options.LogTo(Console.WriteLine, LogLevel.Information);


              });

            builder.Services.AddCors(options =>
           options.AddPolicy("ivppolicy", builder => {
               builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
           }));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("ivppolicy");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
