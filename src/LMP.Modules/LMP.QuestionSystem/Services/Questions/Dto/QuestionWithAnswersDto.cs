using System.Collections.Generic;
using Abp.AutoMapper;
using LMP.QuestionSystem.Domain.Questions;

namespace LMP.QuestionSystem.Services.Dtos
{
    [AutoMapFrom(typeof(Question))]
    public class QuestionWithAnswersDto : QuestionDto
    {
        public List<AnswerDto> Answers { get; set; }
    }
}