using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LMP.QuestionSystem.Services.Dtos;

namespace LMP.QuestionSystem.Services
{
    public interface IUserAppService : IApplicationService
    {
        ListResultOutput<UserDto> GetUsers();
    }
}
