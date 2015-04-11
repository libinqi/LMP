using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LMP.Core.Security.Users;

namespace LMP.QuestionSystem.Services.Dtos
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        public string UserName { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string EmailAddress { get; set; }
    }
}