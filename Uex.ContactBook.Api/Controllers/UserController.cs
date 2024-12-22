using Uex.ContactBook.Api.Controllers.Base;
using Uex.ContactBook.Domain.Model.DTOs.User;
using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using Uex.ContactBook.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace Uex.ContactBook.Api.Controllers
{
    public class UserController : MyControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserServiceAsync _userService;

        public UserController(
            NotificationContext notificationContext,
            ILogger<UserController> logger,
            IUserServiceAsync userService)
            : base(notificationContext)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get Loged User Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {

            var response = await _userService.GetAsync(Authentication.UserId);
            return CreateResult(response);
        }

        /// <summary>
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(UserCreateRequest param)
        {
            try
            {
                var entity = await _userService.CreateAsync(param);
                if (!entity.Valid)
                    return CreateResult("Ao inserir usuário", "Erro ao inserir usuário");

                var response = entity.Adapt<UserCreateResponse>();
                return CreateResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete logged user
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public virtual async Task<ActionResult> Delete()
        {
            await _userService.DeleteAsync(Authentication.UserId);
            return CreateResult(null);
        }
    }
}
