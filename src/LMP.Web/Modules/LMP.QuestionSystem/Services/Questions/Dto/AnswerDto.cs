using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LMP.QuestionSystem.Domain.Questions;

namespace LMP.QuestionSystem.Services.Dtos
{
    [AutoMapFrom(typeof(Answer))]
    public class AnswerDto : CreationAuditedEntityDto
    {
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public bool IsAccepted { get; set; }

        public string CreatorUserName { get; set; }
    }
}