using Abp.Application.Services.Dto;

namespace LMP.QuestionSystem.Services.Dtos
{
    public class GetQuestionInput : EntityRequestInput
    {
        public bool IncrementViewCount { get; set; }
    }
}