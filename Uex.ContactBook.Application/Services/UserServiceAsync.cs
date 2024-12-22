using Uex.ContactBook.Application.Base;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Domain.Notification;
using Mapster;
using Uex.ContactBook.Domain.Model.DTOs.User;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Uex.ContactBook.Application.Services
{
    public class UserServiceAsync : ServiceBase, IUserServiceAsync
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IConfiguration _configuration;

        public UserServiceAsync(
            NotificationContext notificationContext,
            IUserRepositoryAsync userRepository,
            IConfiguration configuration,
            ITokenGeneratorService tokenGeneratorService
        ) : base(notificationContext)
        {
            _userRepository = userRepository;
            _tokenGeneratorService = tokenGeneratorService;
            _configuration = configuration;
        }

        public async Task<User> CreateAsync(UserCreateRequest param)
        {
            var newUser = param.Adapt<User>();
            newUser.Validate();
            AddNotifications(newUser.ValidationResult);

            if (HasNotifications)
                return newUser;

            await UserNameAlreadyExistsValidation(param.UserName);

            if (HasNotifications)
                return newUser;

            await EmailAlreadyExistsValidation(param.Email);

            if (HasNotifications)
                return newUser;

            var result = await _userRepository.CreateAsync(newUser); ;
            return result;
        }

        private async Task UserNameAlreadyExistsValidation(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);

            if (user != null)
                AddValidationFailure(UserMessage.USER_USERNAME_ALREADY_EXISTS);
        }

        private async Task EmailAlreadyExistsValidation(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
                AddValidationFailure(UserMessage.USER_EMAIL_ALREADY_EXISTS);
        }

        public async Task<UserGetResponse> GetAsync(Guid id)
        {
            var UserEntity = await _userRepository.GetUserAsync(id);
            var model = UserEntity.Adapt<UserGetResponse>();
            return model;
        }

        public async Task<IEnumerable<UserGetAllResponse>> GetAsync()
        {
            var entity = await _userRepository.GetUserAsync();
            var model = entity.Adapt<IEnumerable<UserGetAllResponse>>();
            return model;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var entity = await _userRepository.GetByUserNameAsync(userName);
            return entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var user = await GetAsync(id);
            if (user == null)
            {
                AddValidationFailure("Usuário não encontrado para excluir.");
                return;
            }

            await _userRepository.DeleteAsync(id);
        }

        public virtual async Task<UserLoginResponse> LoginAsync(UserLoginRequest param)
        {
            var user = await GetByUserNameAsync(param.UserName);
            if (user == null)
            {
                AddValidationFailure("Usuário não encontrado.");
                return new UserLoginResponse();
            }

            var issuer = _configuration["OAuth:Issuer"];
            var audience = _configuration["OAuth:Audience"];
            var secret = _configuration["OAuth:Secret"];
            var response = await CreateAuthorization(param, issuer, audience, secret);
            return response;
        }

        private async Task<UserLoginResponse> CreateAuthorization(UserLoginRequest param, string issuer, string audience, string secret)
        {
            DateTime expiresOnRefreshToken = DateTime.UtcNow.AddDays(15);
            DateTime expiresOnAccessToken = DateTime.UtcNow.AddHours(1);

            var claims = new List<Claim>(){
                new Claim(ClaimTypes.NameIdentifier, param.UserName),
                new Claim(ClaimTypes.Email, param.Email)
            };

            var roles = Enumerable.Empty<string>();
            
            byte[] bytes = Encoding.UTF8.GetBytes(secret);
            var secretBase64 = Convert.ToBase64String(bytes);

            var generateRequest = new GenerateRequest(
                claims,
                expiresOnAccessToken,
                issuer,
                audience,
                secretBase64);

            string token = _tokenGeneratorService.Generate(generateRequest);

            var response = new UserLoginResponse()
            {
                UserName = param.UserName,
                Email = param.Email,
                AccessToken = token,
                CreatedUtcDateTime = DateTime.UtcNow,
                ExpiresOnAccessToken = expiresOnAccessToken,
                RefreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                ExpiresOnRefreshToken = expiresOnRefreshToken
            };

            return response;
        }
    }
}
