using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LMP.QuestionSystem.Services.Dtos;

namespace LMP.QuestionSystem.Services
{
    public interface IQuestionAppService : IApplicationService
    {
        PagedResultOutput<QuestionDto> GetQuestions(GetQuestionsInput input);

        Task CreateQuestion(CreateQuestionInput input);

        GetQuestionOutput GetQuestion(GetQuestionInput input);

        VoteChangeOutput VoteUp(EntityRequestInput input);

        VoteChangeOutput VoteDown(EntityRequestInput input);

        SubmitAnswerOutput SubmitAnswer(SubmitAnswerInput input);

        void AcceptAnswer(EntityRequestInput input);
    }
}
