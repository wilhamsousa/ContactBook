using Uex.ContactBook.Api.Controllers.Base;
using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using Uex.ContactBook.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Uex.ContactBook.Domain.Model.DTOs.Login;

namespace Uex.ContactBook.Api.Controllers
{
    public class LoginController : MyControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly ILoginServiceAsync _loginServiceAsync;

        public LoginController(
            NotificationContext notificationContext,
            ILogger<UserController> logger,
            ILoginServiceAsync loginServiceAsync)
            : base(notificationContext)
        {
            _logger = logger;
            _loginServiceAsync = loginServiceAsync;
        }

        /// <summary>
        /// Login to receive credentials
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest param)
        {
            var response = await _loginServiceAsync.LoginAsync(param);
            if (HasNotifications)
                return Unauthorized(response);

             return CreateResult(response);
        }
    }
}
