
using LiveStock.MiddleWare;
using LiveStock.Scheduler;
using LiveStockBL.Hubs;
using LiveStockBL.MiddleWare;
using LiveStockBL.ServiceContract;
using LiveStockBL.Services;

namespace LiveStock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontEndCors", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IStockService,StockService>();
            builder.Services.AddHostedService<StockUpdateScheduler>();

            var app = builder.Build();
            app.UseExceptionHandlingMiddleware();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("FrontEndCors");
            app.UseFailureHandlingMiddleware();
            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHub<StockHub>("/stockhub");
            app.Run();
        }
    }
}
