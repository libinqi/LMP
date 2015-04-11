using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using LMP.QuestionSystem.Services.Dtos;
using LMP.Core.Security.Users;

namespace LMP.QuestionSystem.Services
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;

        public UserAppService(IRepository<User, long> userRepository)
        {
            _userRepository = userRepository;
        }

        public ListResultOutput<UserDto> GetUsers()
        {
            return new ListResultOutput<UserDto>
                   {
                       Items = _userRepository
                           .GetAllList(u => u.TenantId == CurrentSession.TenantId)
                           .MapTo<List<UserDto>>()
                   };
        }
    }
}