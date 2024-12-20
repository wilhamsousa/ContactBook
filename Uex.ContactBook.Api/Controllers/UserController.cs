using Uex.ContactBook.Api.Controllers.Base;
using Uex.ContactBook.Domain.Model.DTOs.User;
using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Model.Validators;

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

                var response = await _userService.CreateAsync(param);
                return CreateResult(response, "Erro ao inserir registro");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
