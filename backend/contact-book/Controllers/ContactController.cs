using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using contact_book.core.Models;
using contact_book.Models;
using contact_book.services;
using contact_book.services.Interfaces;
using contact_book.services.Services;
using MoreLinq.Experimental;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using Type = contact_book.core.Models.Type;

namespace contact_book.Controllers
{
    public class ContactController : BaseController
    {
        private readonly ContactService _contactService;

        public ContactController()
        {
            _contactService = new ContactService();
        }
        // GET api/contacts
        [HttpGet]
        [Route("api/contacts")]
        public async Task<IHttpActionResult> GetAllContacts()
        {
            var contact = await _contactService.GetAllContacts();
            return Ok(contact.Select((ConvertToApiContact)).ToList());
        }
        // GET api/contacts/{id}
        [HttpGet]
        [Route("api/contacts/{id}")]
        public async Task<IHttpActionResult> GetContactById(int id)
        {
            var contact = await _contactService.GetContactById(id);
            if (contact == null)
                return NotFound();
            return Ok(ConvertToApiContact(contact));
        }

        // GET api/contacts/{search}
        [HttpGet]
        [Route("api/contacts")]
        public async Task<IHttpActionResult> SearchContacts (string search)
        {
            var result = await _contactService.SearchContacts(search.Trim().ToLowerInvariant());
            if (!result.Succeeded)
                return BadRequest(result.ValidationResult.ToString(" | "));
            return Ok(result.ContactCollection.Select((ConvertToApiContact)).ToList());
        }
        //GET api/types
        [HttpGet]
        [Route("api/types")]
        public async Task<IHttpActionResult> GetAllTypes()
        {
            var result = await _contactService.GetAllTypes();
            return Ok(result.Select((ConvertToApiType)).ToList());
        }


        // PATCH api/contacts/{id}
        [HttpPatch]
        [Route("api/contacts/{id}")]
        public async Task<IHttpActionResult> UpdateContact(int id, ContactApiModel contactToUpdate)
        {
            var result = await _contactService.UpdateContact(id, ConvertToContact(contactToUpdate));
            if (!result.Succeeded && result.ValidationResult != null)
                return BadRequest(result.ValidationResult.ToString(" | "));
            if (!result.Succeeded && result.ErrorMessage != null)
                return BadRequest(result.ErrorMessage);
            return Ok(ConvertToApiContact(result.Contact));
        }


        // PUT api/contacts
        [HttpPut]
        [Route("api/contacts")]
        public async Task<IHttpActionResult> AddContact(ContactApiModel contact)
        {
            var result = await _contactService.AddContact(ConvertToContact(contact));
            if (!result.Succeeded)
                return BadRequest(result.ValidationResult.ToString(" | "));
            return Created("", ConvertToApiContact(result.Contact));
        }

        // DELETE api/contacts/{id}
        [System.Web.Http.HttpDelete]
        [Route("api/contacts/{id}")]
        public async Task<IHttpActionResult> DeleteById(int id)
        {
            var contact = await _contactService.GetContactById(id);
            if (contact == null)
                return NotFound();
            await _contactService.DeleteContactById(id);
            return Ok();
        }

        //DELETE api/contacts/clearall
        [System.Web.Http.HttpDelete]
        [Route("api/contacts/clearall")]
        public async Task<IHttpActionResult> DeleteAllContacts()
        {
            var contacts = await _contactService.GetAllContacts();
            if (contacts.Count == 0)
                return NotFound();
            await _contactService.DeleteAllContacts();
            return Ok($"{contacts.Count} contacts deleted!");
        }
    }
}
