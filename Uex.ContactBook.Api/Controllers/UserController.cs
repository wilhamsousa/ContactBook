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
        private readonly IUserServiceAsync _userServiceAsync;

        public UserController(
            NotificationContext notificationContext,
            ILogger<UserController> logger,
            IUserServiceAsync userServiceAsync)
            : base(notificationContext)
        {
            _logger = logger;
            _userServiceAsync = userServiceAsync;
        }

        /// <summary>
        /// Get Loged User Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var entity = await _userServiceAsync.GetAsync(Authentication.UserId);
            if (entity == null)
                return CreateResult(null, "Usuário não encontrado");

            var response = entity.Adapt<UserGetResponse>();
            return CreateResult(response);
        }

        /// <summary>
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(UserCreateRequest param)
        {
            var entity = await _userServiceAsync.CreateAsync(param);
            if (!entity.Valid)
                return CreateResult("Ao inserir usuário", "Erro ao inserir usuário");

            var response = entity.Adapt<UserCreateResponse>();
            return CreateResult(response);
        }

        /// <summary>
        /// Delete logged user
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public virtual async Task<ActionResult> Delete()
        {
            await _userServiceAsync.DeleteAsync(Authentication.UserId);
            return CreateResult();
        }
    }
}
