using Uex.ContactBook.Api.Controllers.Base;
using Uex.ContactBook.Domain.Model.DTOs.User;
using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Validators;
using Mapster;

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

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var response = await _userService.GetAsync(id);
            return CreateResult(response, "Erro");
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var response = await _userService.GetAsync();
                return CreateResult(response.ToList(), "Erro ao consultar.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(UserCreateRequest param)
        {
            try
            {
                var requestValidator = new UserCreateRequestValidator();
                var requestValidatorResult = requestValidator.Validate(param);
                if (!requestValidatorResult.IsValid)
                {
                    AddNotifications(requestValidatorResult);
                    return CreateResult(null, "Erro ao inserir registro");
                }

                var entity = await _userService.CreateAsync(param);
                if (!requestValidatorResult.IsValid)
                {
                    AddNotifications(requestValidatorResult);
                    return CreateResult(null, "Erro ao inserir registro");
                }

                var response = entity.Adapt<UserCreateResponse>();
                return CreateResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userService.GetAsync(id);
            if (user == null)
            {
                AddValidationFailure("Usuário não encontrado para excluir.");
                return CreateResult(null, "Erro ao excluir registro");
            }

            await _userService.DeleteAsync(id);
            return CreateResult(null, "Erro ao excluir registro");
        }
    }
}
