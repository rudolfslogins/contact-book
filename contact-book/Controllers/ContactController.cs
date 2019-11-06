using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using contact_book.core.Models;
using contact_book.services;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace contact_book.Controllers
{
    public class ContactController : ApiController
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
            return Ok(contact);
        }
        // GET api/contacts/{id}
        [HttpGet]
        [Route("api/contacts/{id}")]
        public async Task<IHttpActionResult> GetContactById(int id)
        {
            var contact = await _contactService.GetContactById(id);
            if (contact == null)
                return NotFound();
            return Ok(contact);
        }

        // GET api/contacts/{search}
        [HttpGet]
        [Route("api/contacts")]
        public async Task<IHttpActionResult> SearchContacts (string search)
        {
            var result = await _contactService.SearchContacts(search.Trim().ToLowerInvariant());
            if (!result.Succeeded)
                return BadRequest(result.ValidationResult.ToString(" | "));
            return Ok(result.ContactCollection);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/contacts
        [HttpPut]
        [Route("api/contacts")]
        public async Task<IHttpActionResult> AddContact(Contact contact)
        {
            var result = await _contactService.AddContact(contact);
            if (!result.Succeeded)
                return BadRequest(result.ValidationResult.ToString(" | "));
            return Created("", result.Contact);
        }

        // DELETE api/contacts/{id}
        [HttpDelete]
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
        [HttpDelete]
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
