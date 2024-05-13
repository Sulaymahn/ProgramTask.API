using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramTask.API.Controllers;
using ProgramTask.API.Data;
using ProgramTask.API.DataTransferObjects;
using ProgramTask.API.Interfaces;
using ProgramTask.API.Services;

namespace ProgramTask.Test
{
    public class SimpleTest
    {
        JobProgramController controller;
        ApplicationDbContext dbContext;
        IJobProgramRepository jobRepository;
        ICandidateApplicationRepository applicationRepository;
        IQuestionRepository questionRepository;

        public SimpleTest()
        {
            string? connString = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User)["CosmosEmulatorConnectionString"] as string;
            var opt = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseCosmos(connString!, "ProgramTaskApp")
                .Options;
            
            dbContext = new ApplicationDbContext(opt);
            jobRepository = new JobProgramRepository(dbContext);
            applicationRepository = new CandidateApplicationRepository(dbContext);
            questionRepository = new QuestionRepository(dbContext);
            controller = new JobProgramController(jobRepository, questionRepository, applicationRepository);
        }

        [Fact]
        public async Task CanStoreProgramQuestions()
        {
            var result1 = await controller.CreateJobProgram(new JobProgramForCreation
            {
                Title = "Summer Internship Program",
                Description = "London is a perfect internship spot for keeping a cultural hub of diversity and opportunity as its central pedestal. Our associated companies span from large multinationals to start-ups working on cutting-edge technology. \n\nWhether you're looking to build your career or learn crucial skills to take away, our list of London firms have plenty to offer, and with so much under the belt for what this city has to offer, there's certainly no room for boredom.\n\n",
                CurrentResidence = new JobProgramFieldMetadata { Hide = true },
                Questions = new List<QuestionForCreation>
                {
                    new QuestionForCreation
                    {
                        Text = "Please tell me about yourself in less than 500 words",
                        Type = "paragraph"
                    }
                }
            }); 

            Assert.IsType<ObjectResult>(result1);
        }
    }
}