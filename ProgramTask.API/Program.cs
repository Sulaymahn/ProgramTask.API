using Microsoft.EntityFrameworkCore;
using ProgramTask.API.Data;
using ProgramTask.API.Interfaces;
using ProgramTask.API.Services;

namespace ProgramTask.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.EnableSensitiveDataLogging();
                opt.UseCosmos(
                    connectionString: builder.Configuration.GetConnectionString("CosmosDb")!,
                    databaseName: "ProgramTaskApp");
            });
            builder.Services.AddScoped<IJobProgramRepository, JobProgramRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<ICandidateApplicationRepository, CandidateApplicationRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            var d = app.Services.CreateScope()
                .ServiceProvider
                .GetRequiredService<ApplicationDbContext>()
                .Database;

            d.EnsureDeleted();
            d.EnsureCreated();

            app.Run();
        }
    }
}
