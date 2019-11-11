using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using contact_book.services.Services;

namespace contact_book.Controllers
{
    public class EmailController : BaseController
    {
        private readonly EmailService _emailService;

        public EmailController()
        {
            _emailService = new EmailService();
        }
        //GET api/contacts/{id}/emails
        [HttpGet]
        [Route("api/contacts/{id}/emails")]
        public async Task<IHttpActionResult> GetEmailsByContactId(int id)
        {
            var result = await _emailService.GetEmailsByContactId(id);
            return Ok(ConvertToApiEmailList(result));
        }
        //DELETE api/
        [HttpDelete]
        [Route("api/email/{id}")]
        public async Task<IHttpActionResult> DeleteEmailById(int id)
        {
            var email = await _emailService.GetEmailById(id);
            if (email == null)
                return NotFound();

            await _emailService.DeleteEmailById(id);
            return Ok();
        }
    }
}
