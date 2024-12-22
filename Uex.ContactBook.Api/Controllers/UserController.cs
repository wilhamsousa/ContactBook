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
        /// Get User Data By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Get(Guid id)
        {
            var response = await _userService.GetAsync(id);
            return CreateResult(response, "Erro");
        }

        /// <summary>
        /// Get all users data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var response = await _userService.GetAsync();
                return CreateResult(response.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return CreateResult(null);
        }
    }
}
