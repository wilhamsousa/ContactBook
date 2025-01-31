using Uex.ContactBook.Api.Controllers.Base;
using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using Uex.ContactBook.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Uex.ContactBook.Domain.Model.DTOs.Contact;

namespace Uex.ContactBook.Api.Controllers
{
    public class ContactController : MyControllerBase
    {

        private readonly ILogger<ContactController> _logger;
        private readonly IContactServiceAsync _contactService;

        public ContactController(
            NotificationContext notificationContext,
            ILogger<ContactController> logger,
            IContactServiceAsync contactService)
            : base(notificationContext)
        {
            _logger = logger;
            _contactService = contactService;
        }

        /// <summary>
        /// Get Contact Data By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Get(Guid id)
        {
            var entity = await _contactService.GetAsync(Authentication.UserId, id);
            if (entity == null)
                return CreateResult(null, "Contato não encontrado");

            var response = entity.Adapt<ContactGetResponse>();
            return CreateResult(response);
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var entity = await _contactService.GetAsync(Authentication.UserId);
            if (entity == null)
                return CreateResult(null, "Contato não encontrado");

            var response = entity.Adapt<IEnumerable<ContactGetResponse>>();
            return CreateResult(response);
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(ContactCreateRequest param)
        {
            var entity = await _contactService.CreateAsync(Authentication.UserId, param);
            if (!entity.Valid)
                return CreateResult("Ao inserir contato", "Erro ao inserir contato");

            var response = entity.Adapt<ContactCreateResponse>();
            return CreateResult(response);
        }

        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Update(Guid id, ContactUpdateRequest param)
        {
            await _contactService.UpdateAsync(Authentication.UserId, id, param);
            if (HasNotifications)
                return CreateResult("Ao atualizar contato", "Erro ao atualizar contato");

            return CreateResult();
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _contactService.DeleteAsync(Authentication.UserId, id);
            return CreateResult();
        }        
    }
}
