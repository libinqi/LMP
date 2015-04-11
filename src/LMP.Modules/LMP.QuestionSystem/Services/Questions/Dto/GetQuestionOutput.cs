using Abp.Application.Services.Dto;

namespace LMP.QuestionSystem.Services.Dtos
{
    public class GetQuestionOutput : IOutputDto
    {
        public QuestionWithAnswersDto Question { get; set; }
    }
}