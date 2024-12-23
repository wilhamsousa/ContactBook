using Uex.ContactBook.Application.Base;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Notification;
using Microsoft.Extensions.Configuration;
using Uex.ContactBook.Domain.Interfaces.External;
using Uex.ContactBook.Domain.Model.DTOs.External;
using System;
using Newtonsoft.Json;

namespace Uex.ContactBook.Application.Services
{
    public class ExternalServiceAsync : ServiceBase, IExternalServiceAsync
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IConfiguration _configuration;

        public ExternalServiceAsync(
            NotificationContext notificationContext
        ) : base(notificationContext)
        {
        }

        public virtual async Task<ViaCepResponse> GetViaCep(string cep)
        {
            string url = $"https://viacep.com.br/ws/{cep}/json/";
            var client = new HttpClient();
            var responseStr = await client.GetStringAsync(url);

            if (string.IsNullOrEmpty(responseStr))
                return new ViaCepResponse();

            var response = JsonConvert.DeserializeObject<ViaCepResponse>(responseStr);
            return response;
        }
    }
}
