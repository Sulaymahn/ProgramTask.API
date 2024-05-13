using Microsoft.AspNetCore.Mvc;
using ProgramTask.API.DataTransferObjects;
using ProgramTask.API.Interfaces;
using ProgramTask.API.Models;

namespace ProgramTask.API.Controllers
{
    [Route("api/program")]
    [ApiController]
    public class JobProgramController : ControllerBase
    {
        private readonly IJobProgramRepository _jobProgramRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly ICandidateApplicationRepository _applicationRepo;

        public JobProgramController(
            IJobProgramRepository jobProgramRepo,
            IQuestionRepository questionRepo,
            ICandidateApplicationRepository applicationRepo)
        {
            _jobProgramRepo = jobProgramRepo;
            _questionRepo = questionRepo;
            _applicationRepo = applicationRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobProgram(
            [FromBody] JobProgramForCreation jobForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var jobProgram = new JobProgram
            {
                Id = Guid.NewGuid(),
                Title = jobForCreation.Title,
                Description = jobForCreation.Description,
                IdNumber = new Field<string>
                {
                    Hide = jobForCreation.IdNumber.Hide,
                    Internal = jobForCreation.IdNumber.Internal,
                    Required = jobForCreation.IdNumber.Required
                },
                CurrentResidence = new Field<string>
                {
                    Hide = jobForCreation.CurrentResidence.Hide,
                    Internal = jobForCreation.CurrentResidence.Internal,
                    Required = jobForCreation.CurrentResidence.Required
                },
                DateOfBirth = new Field<DateTime?>
                {
                    Hide = jobForCreation.DateOfBirth.Hide,
                    Internal = jobForCreation.DateOfBirth.Internal,
                    Required = jobForCreation.DateOfBirth.Required
                },
                Nationality = new Field<string>
                {
                    Hide = jobForCreation.Nationality.Hide,
                    Internal = jobForCreation.Nationality.Internal,
                    Required = jobForCreation.Nationality.Required
                },
                Gender = new Field<Gender?>
                {
                    Hide = jobForCreation.Gender.Hide,
                    Internal = jobForCreation.Gender.Internal,
                    Required = jobForCreation.Gender.Required
                },
                Phone = new Field<string>
                {
                    Hide = jobForCreation.Phone.Hide,
                    Internal = jobForCreation.Phone.Internal,
                    Required = jobForCreation.Phone.Required
                }
            };

            try
            {
                var questions = new List<Question>();
                foreach (var question in jobForCreation.Questions)
                {
                    var questionToAdd = new Question()
                    {
                        Id = Guid.NewGuid(),
                        JobProgramId = jobProgram.Id,
                        Text = question.Text,
                        AllowedAnswers = question.AllowedAnswers ?? [],
                        MaxAnswerCount = question.MaxAnswerCount,
                        MinAnswerCount = question.MinAnswerCount,
                    };

                    if (Enum.TryParse(question.Type, true, out QuestionType type))
                    {
                        questionToAdd.Type = type;
                        questions.Add(questionToAdd);
                    }
                    else
                    {
                        throw new ArgumentException($"Contains an invalid question type");
                    }
                }

                await _questionRepo.CreateAllAsync(questions);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }

            await _jobProgramRepo.CreateAsync(jobProgram);

            return new ObjectResult(jobProgram.Id) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{programId:guid}/question/{questionId:guid}")]
        public async Task<IActionResult> UpdateQuestion(
            [FromRoute] Guid programId,
            [FromRoute] Guid questionId,
            [FromBody] QuestionForUpdate questionForUpdate)
        {
            var question = await _questionRepo.FindFromProgramId(questionId, programId);
            if (question == null)
            {
                return NotFound();
            }

            question.Text = questionForUpdate.Text;
            question.AllowedAnswers = questionForUpdate.AllowedAnswers ?? [];
            question.MinAnswerCount = questionForUpdate.MinAnswerCount;
            question.MaxAnswerCount = questionForUpdate.MaxAnswerCount;

            if (Enum.TryParse(questionForUpdate.Type, true, out QuestionType type))
            {
                question.Type = type;
            }
            else
            {
                return BadRequest("Invalid question type");
            }

            await _questionRepo.UpdateAsync(question);

            return Ok();
        }

        [HttpGet("{programId:guid}/question")]
        public async Task<IActionResult> GetQuestions(
            [FromRoute] Guid programId)
        {
            var program = await _jobProgramRepo.GetAsync(programId);

            if (program == null)
            {
                return NotFound("Program Not Found");
            }

            var questions = await _questionRepo.FindFromProgramId(programId);

            return Ok(questions.Select(question => new QuestionForClient
            {
                Text = question.Text,
                AllowedAnswers = question.AllowedAnswers,
                MaxAnswerCount = question.MaxAnswerCount,
                MinAnswerCount = question.MinAnswerCount,
                Type = Enum.GetName(question.Type)!.ToLowerInvariant()
            }));
        }

        [HttpPost("{programId:guid}/application")]
        public async Task<IActionResult> CreateApplication(
            [FromRoute] Guid programId,
            [FromBody] CandidateApplicationForCreation applicationForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var program = await _jobProgramRepo.GetAsync(programId);
            if (program == null)
            {
                return NotFound("Program not found");
            }

            var application = new CandidateApplication
            {
                Id = Guid.NewGuid(),
                JobProgramId = programId,
                CurrentResidence = applicationForCreation.CurrentResidence,
                DateOfBirth = applicationForCreation.DateOfBirth,
                Email = applicationForCreation.Email,
                FirstName = applicationForCreation.FirstName,
                LastName = applicationForCreation.LastName,
                IdNumber = applicationForCreation.IdNumber,
                Nationality = applicationForCreation.Nationality,
                Phone = applicationForCreation.Phone,
                Answers = applicationForCreation.Answers.Select(answer =>
                new Answer
                {
                    Id = Guid.NewGuid(),
                    QuestionId = answer.QuestionId,
                    Value = answer.Answer
                }).ToList()
            };

            if (Enum.TryParse(applicationForCreation.Gender, true, out Gender gender))
            {
                application.Gender = gender;
            }
            else
            {
                return BadRequest("Invalid gender");
            }

            await _applicationRepo.CreateAsync(application);
            return new ObjectResult(application.Id) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
