using Uex.ContactBook.Application.Base;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Notification;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Uex.ContactBook.Domain.Model.DTOs.Login;
using Uex.ContactBook.Domain.Interfaces.External;
using Uex.ContactBook.Domain.Model.Consts;

namespace Uex.ContactBook.Application.Services
{
    public class LoginServiceAsync : ServiceBase, ILoginServiceAsync
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IExternalServiceAsync _externalService;

        public LoginServiceAsync(
            NotificationContext notificationContext,            
            IUserRepositoryAsync userRepository,
            IConfiguration configuration,
            IExternalServiceAsync externalService
        ) : base(notificationContext)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _externalService = externalService;            
        }

        public virtual async Task<LoginResponse> LoginAsync(LoginRequest param)
        {
            var user = await _userRepository.GetByEmailAsync(param.Email);
            if (user == null)
            {
                AddValidationFailure("Usuário ou senha inválidos.");
                return new LoginResponse();
            }

            if (!user.Password.Equals(param.Password))
            {
                AddValidationFailure("Usuário ou senha inválidos.");
                return new LoginResponse();
            }

            var issuer = _configuration["OAuth:Issuer"];
            var audience = _configuration["OAuth:Audience"];
            var secret = _configuration["OAuth:Secret"];
            var response = await CreateAuthorization(param, user.Id, user.UserName, issuer, audience, secret);
            return response;
        }

        private async Task<LoginResponse> CreateAuthorization(LoginRequest param, Guid userId, string userName, string issuer, string audience, string secret)
        {
            DateTime expiresOnRefreshToken = DateTime.UtcNow.AddDays(15);
            DateTime expiresOnAccessToken = DateTime.UtcNow.AddHours(1);

            var claims = new List<Claim>(){
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, param.Email)
            };

            var roles = Enumerable.Empty<string>();

            byte[] bytes = Encoding.UTF8.GetBytes(secret);
            var secretBase64 = Convert.ToBase64String(bytes);

            var generateRequest = new GenerateLoginRequest(
                claims,
                expiresOnAccessToken,
                issuer,
                audience,
                secretBase64);

            string token = GenerateToken(generateRequest);

            var response = new LoginResponse()
            {
                UserName = userName,
                Email = param.Email,
                AccessToken = token,
                CreatedUtcDateTime = DateTime.UtcNow,
                ExpiresOnAccessToken = expiresOnAccessToken,
                RefreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                ExpiresOnRefreshToken = expiresOnRefreshToken
            };

            return response;
        }

        private string GenerateToken(GenerateLoginRequest param)
        {
            var identityClaims = new ClaimsIdentity(param.claims);
            var tokenHandler = new JwtSecurityTokenHandler();

            var symmetricKey = Convert.FromBase64String(param.base64Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = param.issuer,
                Audience = param.audiance,
                Expires = param.expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task ResetEmail(ResetEmailRequest param)
        {            
            await _externalService.RabbitMqProduce(RabbitmqConstants.ResetEmailQueue, param.Email);
        }
    }
}
