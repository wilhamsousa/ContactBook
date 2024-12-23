using Uex.ContactBook.Api.Controllers.Base;
using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Uex.ContactBook.Domain.Interfaces.External;

namespace Uex.ContactBook.Api.Controllers
{
    public class ExternalController : MyControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IExternalServiceAsync _externalServiceAsync;

        public ExternalController(
            NotificationContext notificationContext,
            ILogger<UserController> logger,
            IExternalServiceAsync externalServiceAsync)
            : base(notificationContext)
        {
            _logger = logger;
            _externalServiceAsync = externalServiceAsync;
        }

        /// <summary>
        /// Get Via Cep data
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("viacep/{cep}")]
        [Authorize]
        public async Task<IActionResult> GetViaCep(string cep)
        {
            var response = await _externalServiceAsync.GetViaCep(cep);
            return CreateResult(response);
        }
    }
}
