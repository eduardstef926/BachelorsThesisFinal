using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Helpers;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllUsersAsync();
            return users
                .Select(mapper.Map<UserEntity, UserDto>)
                .ToList();
        }
    }
}
